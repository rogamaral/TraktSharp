﻿using System;
using System.Linq;
using System.Net.Http;
using TraktSharp.Enums;

namespace TraktSharp.Request {

	public abstract class TraktBodylessPostRequest<TResponse> : TraktRequest<TResponse, object> {

		protected TraktBodylessPostRequest(TraktClient client) : base(client) { }

		protected override HttpMethod Method { get { return HttpMethod.Post; } }

		protected override TraktOAuthRequirement OAuthRequirement { get { return TraktOAuthRequirement.Required; } }

	}

}