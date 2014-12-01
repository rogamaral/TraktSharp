﻿using System;
using System.Linq;
using TraktSharp.Entities.Response.Sync;
using TraktSharp.Enums;

namespace TraktSharp.Request.Sync {

	public class TraktSyncLastActivitiesRequest : TraktGetRequest<TraktSyncLastActivitiesResponse> {

		public TraktSyncLastActivitiesRequest(TraktClient client) : base(client) { }

		protected override string PathTemplate { get { return "sync/last_activities"; } }

		protected override TraktOAuthRequirement OAuthRequirement { get { return TraktOAuthRequirement.Required; } }

	}

}