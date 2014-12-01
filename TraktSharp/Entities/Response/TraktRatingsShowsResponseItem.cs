﻿using System;
using System.Linq;
using Newtonsoft.Json;
using TraktSharp.Enums;

namespace TraktSharp.Entities.Response {

	[Serializable]
	public class TraktRatingsShowsResponseItem {

		[JsonProperty(PropertyName = "rating")]
		public TraktRating Rating { get; set; }

		[JsonProperty(PropertyName = "rated_at")]
		public DateTime? RatedAt { get; set; }

		[JsonProperty(PropertyName = "show")]
		public TraktShow Show { get; set; }

	}

}