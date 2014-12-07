﻿using System;
using System.Collections.Generic;
using System.Linq;
using TraktSharp.Entities.Response;
using TraktSharp.Enums;

namespace TraktSharp.Request.Sync {

	internal class TraktSyncCollectionShowsRequest : TraktGetRequest<IEnumerable<TraktCollectionShowsResponseItem>> {

		internal TraktSyncCollectionShowsRequest(TraktClient client) : base(client) { }

		protected override string PathTemplate { get { return "sync/collection/shows"; } }

		protected override TraktAuthenticationRequirement AuthenticationRequirement { get { return TraktAuthenticationRequirement.Required; } }

	}

}