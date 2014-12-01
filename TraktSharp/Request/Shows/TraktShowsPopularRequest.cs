﻿using System;
using System.Collections.Generic;
using System.Linq;
using TraktSharp.Entities;
using TraktSharp.Enums;

namespace TraktSharp.Request.Shows {

	public class TraktShowsPopularRequest : TraktGetRequest<IEnumerable<TraktShow>> {

		public TraktShowsPopularRequest(TraktClient client) : base(client) { }

		protected override string PathTemplate { get { return "shows/popular"; } }

		protected override TraktOAuthRequirement OAuthRequirement { get { return TraktOAuthRequirement.NotRequired; } }

		protected override bool SupportsPagination { get { return true; } }

	}

}