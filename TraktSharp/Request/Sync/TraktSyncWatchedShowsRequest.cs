﻿using System;
using System.Collections.Generic;
using System.Linq;
using TraktSharp.Entities.Response;
using TraktSharp.Enums;

namespace TraktSharp.Request.Sync {

	internal class TraktSyncWatchedShowsRequest : TraktGetRequest<IEnumerable<TraktWatchedShowsResponseItem>> {

		internal TraktSyncWatchedShowsRequest(TraktClient client) : base(client) { }

		protected override string PathTemplate { get { return "sync/watched/shows"; } }

		protected override TraktAuthenticationRequirement AuthenticationRequirement { get { return TraktAuthenticationRequirement.Required; } }

	}

}