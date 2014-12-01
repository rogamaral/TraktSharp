﻿using System;
using System.Collections.Generic;
using System.Linq;
using TraktSharp.Entities.Response;
using TraktSharp.Enums;

namespace TraktSharp.Request.Sync {

	public class TraktSyncWatchlistShowsRequest : TraktGetRequest<IEnumerable<TraktWatchlistShowsResponseItem>> {

		public TraktSyncWatchlistShowsRequest(TraktClient client) : base(client) { }

		protected override string PathTemplate { get { return "sync/watchlist/shows"; } }

		protected override TraktOAuthRequirement OAuthRequirement { get { return TraktOAuthRequirement.Required; } }

	}

}