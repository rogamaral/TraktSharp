﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TraktSharp.Response;

namespace TraktSharp.Request.Shows {

	public class TraktShowsProgressWatchedRequest : TraktRequest<TraktShowProgress> {

		public TraktShowsProgressWatchedRequest(TraktClient client) : base(client) { }

		protected override HttpMethod Method { get { return HttpMethod.Get; } }

		protected override string PathTemplate { get { return "shows/{id}/progress/watched"; } }

		protected override OAuthRequirementOptions OAuthRequirement { get { return OAuthRequirementOptions.Required; } }

		protected override bool SupportsPagination { get { return false; } }

		public string Id { get; set; }

		protected override IEnumerable<KeyValuePair<string, string>> GetPathParameters(IEnumerable<KeyValuePair<string, string>> pathParameters) {
			return new Dictionary<string, string> {
				{ "id", Id }
			};
		}

		protected override void ValidateParameters() {
			if (string.IsNullOrEmpty(Id)) {
				throw new ArgumentException("Id not set.");
			}
		}

	}

}