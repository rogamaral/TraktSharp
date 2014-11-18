﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TraktSharp.Response {

	[Serializable]
	public class TraktEpisode {

		public TraktEpisode() {
			Title = string.Empty;
			Ids = new TraktEpisodeIds();
		}

		[JsonProperty(PropertyName = "available_translations")]
		public IEnumerable<string> AvailableTranslations { get; set; }

		[JsonProperty(PropertyName = "first_aired")]
		public DateTime? FirstAired { get; set; }

		[JsonProperty(PropertyName = "ids")]
		public TraktEpisodeIds Ids { get; set; }

		[JsonProperty(PropertyName = "images")]
		public TraktEpisodeImages Images { get; set; }

		[JsonProperty(PropertyName = "number")]
		public int? Number { get; set; }

		[JsonProperty(PropertyName = "number_abs")]
		public int? NumberAbsolute { get; set; }

		[JsonProperty(PropertyName = "overview")]
		public string Overview { get; set; }

		[JsonProperty(PropertyName = "rating")]
		public decimal? Rating { get; set; }

		[JsonProperty(PropertyName = "season")]
		public int? Season { get; set; }

		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "updated_at")]
		public DateTime? UpdatedAt { get; set; }

	}

}