﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TraktSharp.Exceptions;
using TraktSharp.Helpers;
using TraktSharp.Response;

namespace TraktSharp.Request {

	public abstract class TraktRequest<TResponse> {

		private readonly TraktClient _client;

		protected TraktRequest(TraktClient client) {
			_client = client;
			Pagination = new PaginationOptions();
		}

		public ExtendedOptions Extended { get; set; }

		public PaginationOptions Pagination { get; set; }

		private bool _authenticate;

		public bool Authenticate {
			get {
				if (_client.Configuration.ForceAuthentication && OAuthRequirement != OAuthRequirementOptions.Forbidden) {
					return true;
				}
				if (OAuthRequirement == OAuthRequirementOptions.Required) {
					return true;
				}
				if (OAuthRequirement == OAuthRequirementOptions.Forbidden) {
					return false;
				}
				return _authenticate;
			}
			set {
				if (!value && OAuthRequirement == OAuthRequirementOptions.Required) {
					throw new InvalidOperationException("This request type requires authentication");
				}
				if (!value && OAuthRequirement == OAuthRequirementOptions.Forbidden) {
					throw new InvalidOperationException("This request type does not allow authentication");
				}
				_authenticate = value;
			}
		}

		protected abstract HttpMethod Method { get; }

		protected abstract string PathTemplate { get; }

		protected abstract OAuthRequirementOptions OAuthRequirement { get; }

		protected abstract bool SupportsPagination { get; }

		protected virtual void ValidateParameters() { }

		protected virtual IEnumerable<KeyValuePair<string, string>> GetPathParameters(IEnumerable<KeyValuePair<string, string>> pathParameters) { return pathParameters; }

		private string Path {
			get {
				return GetPathParameters(new Dictionary<string, string>())
					.Aggregate(PathTemplate.ToLower(), (current, parameter) => current.Replace("{" + parameter.Key.ToLower() + "}", parameter.Value.ToLower()))
					.TrimEnd(new[] {'/'});
			}
		}

		protected virtual IEnumerable<KeyValuePair<string, string>> GetQueryStringParameters(Dictionary<string, string> queryStringParameters) {
			if (Extended != ExtendedOptions.Unspecified) {
				queryStringParameters["extended"] = EnumsHelper.GetDescription(Extended);
			}
			if (SupportsPagination) {
				if (Pagination.Page != null) {
					queryStringParameters["page"] = Pagination.Page.ToString();
				}
				if (Pagination.Limit != null) {
					queryStringParameters["limit"] = Pagination.Limit.ToString();
				}
			}
			return queryStringParameters;
		}

		private string QueryString {
			get {
				using (var content = new FormUrlEncodedContent(GetQueryStringParameters(new Dictionary<string, string>()))) {
					var ret = content.ReadAsStringAsync().Result;
					if (!string.IsNullOrEmpty(ret)) {
						ret = string.Format("?{0}", ret);
					}
					return ret;
				}
			}
		}

		public string Url {
			get { return string.Format("{0}{1}{2}", _client.Configuration.BaseUrl, Path, QueryString); }
		}

		public object RequestBody { get; set; }

		protected HttpContent RequestBodyContent {
			get {
				var json = RequestBodyJson;
				return string.IsNullOrEmpty(json) ? null : new StringContent(json, Encoding.UTF8, "application/json");
			}
		}

		protected string RequestBodyJson {
			get { return RequestBody == null ? null : JsonConvert.SerializeObject(RequestBody, Formatting.Indented); }
		}

		protected virtual void SetRequestHeaders(HttpRequestMessage request) {
			request.Headers.Add("trakt-api-key", _client.Authentication.ClientId);
			request.Headers.Add("trakt-api-version", _client.Configuration.ApiVersion.ToString(CultureInfo.InvariantCulture));
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			if (Authenticate) {
				if (_client.Authentication.CurrentAccessToken == null || string.IsNullOrEmpty(_client.Authentication.CurrentAccessToken.AccessToken)) {
					throw new InvalidOperationException("Authentication is required for this request type, but the current access token is not set");
				}
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _client.Authentication.CurrentAccessToken.AccessToken);
			}
		}

		public async Task<TResponse> SendAsync() {
			ValidateParameters(); //Expected to throw an exception on invalid parameters.

			using (var cl = new HttpClient()) {
				var request = new HttpRequestMessage(Method, Url) {Content = RequestBodyContent};
				SetRequestHeaders(request);
				var response = await cl.SendAsync(request).ConfigureAwait(false);
				var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

				if (!response.IsSuccessStatusCode) {
					TraktErrorResponse traktError = null;
					try {
						traktError = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<TraktErrorResponse>(responseText)).ConfigureAwait(false);
					} catch {}
					traktError = traktError ?? new TraktErrorResponse();
					var message = string.IsNullOrEmpty(traktError.Description)
						? "The Trakt API threw an error with no content. Refer to the StatusCode for an indication of the problem."
						: traktError.Description;
					switch (response.StatusCode) {
						case HttpStatusCode.NotFound:
							throw new TraktNotFoundException(traktError, Url, RequestBodyJson);
						case HttpStatusCode.BadRequest:
							throw new TraktBadRequestException(traktError, Url, RequestBodyJson);
						case HttpStatusCode.Unauthorized:
							throw new TraktUnauthorizedException(traktError, Url, RequestBodyJson);
						case HttpStatusCode.Forbidden:
							throw new TraktForbiddenException(traktError, Url, RequestBodyJson);
						case HttpStatusCode.MethodNotAllowed:
							throw new TraktMethodNotFoundException(traktError, Url, RequestBodyJson);
						case HttpStatusCode.Conflict:
							throw new TraktConflictException(traktError, Url, RequestBodyJson);
						//case HttpStatusCode.UnprocessableEntity: //TODO: No such enumeration member. Must decide what to do about this
						//	throw new TraktUnprocessableEntityException(traktError, Url, RequestBodyJson);
						//case HttpStatusCode.RateLimitExceeded: //TODO: No such enumeration member. Must decide what to do about this
						//	throw new TraktRateLimitExceededException(traktError, Url, RequestBodyJson);
						case HttpStatusCode.InternalServerError:
							throw new TraktServerErrorException(traktError, Url, RequestBodyJson);
						case HttpStatusCode.ServiceUnavailable:
							throw new TraktServiceUnavailableException(traktError, Url, RequestBodyJson);
					}
					throw new TraktException(message, response.StatusCode, traktError, Url, RequestBodyJson);
				}

				return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<TResponse>(responseText)).ConfigureAwait(false);
			}
		}

	}

}