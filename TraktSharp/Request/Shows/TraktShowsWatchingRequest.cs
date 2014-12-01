﻿using System;
using System.Collections.Generic;
using System.Linq;
using TraktSharp.Entities;
using TraktSharp.Enums;

namespace TraktSharp.Request.Shows {

	public class TraktShowsWatchingRequest : TraktGetByIdRequest<IEnumerable<TraktUser>> {

		public TraktShowsWatchingRequest(TraktClient client) : base(client) { }

		protected override string PathTemplate { get { return "shows/{id}/watching"; } }

		protected override TraktOAuthRequirement OAuthRequirement { get { return TraktOAuthRequirement.NotRequired; } }

	}

}