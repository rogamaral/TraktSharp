﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraktSharp.Entities.Response;
using TraktSharp.Entities.Response.Sync;
using TraktSharp.Enums;
using TraktSharp.Request.Sync;

namespace TraktSharp.Modules {

	/// <summary>Provides API methods in the Sync namespace</summary>
	public partial class TraktSyncModule {

		/// <summary>Default constructor for the module. Used internally by <see cref="TraktClient"/>.</summary>
		/// <param name="client">The owning instance of <see cref="TraktClient"/></param>
		public TraktSyncModule(TraktClient client) : base(client) { }

		/// <summary>
		/// This method is a useful first step in the syncing process. We recommended caching these dates locally, then you can compare to know exactly what data has
		/// changed recently. This can greatly optimize your syncs so you don't pull down a ton of data only to see nothing has actually changed.
		/// </summary>
		/// <returns>See summary</returns>
		public async Task<TraktSyncLastActivitiesResponse> GetLastActivitiesAsync() {
			return await SendAsync(new TraktSyncLastActivitiesRequest(Client));
		}

		/// <summary>
		/// Whenever a scrobble is paused, the playback progress is saved. Use this progress to sync up playback across different media centers or apps. For example, you can start
		/// watching a movie in a media center, stop it, then resume on your tablet from the same spot. Each item will have the progress percentage between 0 and 100.
		/// </summary>
		/// <param name="extended">Changes which properties are populated for standard media objects. By default, minimal data is returned. Change this if additional fields are required in the returned data.</param>
		/// <returns>See summary</returns>
		public async Task<TraktSyncPlaybackResponse> GetPlaybackStateAsync(ExtendedOption extended = ExtendedOption.Unspecified) {
			return await SendAsync(new TraktSyncPlaybackRequest(Client) { Extended = extended });
		}

		/// <summary>Get all collected items in a user's collection. A collected item indicates availability to watch digitally or on physical media.</summary>
		/// <param name="extended">Changes which properties are populated for standard media objects. By default, minimal data is returned. Change this if additional fields are required in the returned data.</param>
		/// <returns>See summary</returns>
		public async Task<IEnumerable<TraktCollectionMoviesResponseItem>> GetMoviesCollectionAsync(ExtendedOption extended = ExtendedOption.Unspecified) {
			return await SendAsync(new TraktSyncCollectionMoviesRequest(Client) { Extended = extended });
		}

		/// <summary>Get all collected items in a user's collection. A collected item indicates availability to watch digitally or on physical media.</summary>
		/// <param name="extended">Changes which properties are populated for standard media objects. By default, minimal data is returned. Change this if additional fields are required in the returned data.</param>
		/// <returns>See summary</returns>
		public async Task<IEnumerable<TraktCollectionShowsResponseItem>> GetShowsCollectionAsync(ExtendedOption extended = ExtendedOption.Unspecified) {
			return await SendAsync(new TraktSyncCollectionShowsRequest(Client) { Extended = extended });
		}

		/// <summary>Returns all movies a user has watched</summary>
		/// <param name="extended">Changes which properties are populated for standard media objects. By default, minimal data is returned. Change this if additional fields are required in the returned data.</param>
		/// <returns>See summary</returns>
		public async Task<IEnumerable<TraktWatchedMoviesResponseItem>> GetWatchedMoviesAsync(ExtendedOption extended = ExtendedOption.Unspecified) {
			return await SendAsync(new TraktSyncWatchedMoviesRequest(Client) { Extended = extended });
		}

		/// <summary>Returns all shows a user has watched</summary>
		/// <param name="extended">Changes which properties are populated for standard media objects. By default, minimal data is returned. Change this if additional fields are required in the returned data.</param>
		/// <returns>See summary</returns>
		public async Task<IEnumerable<TraktWatchedShowsResponseItem>> GetWatchedShowsAsync(ExtendedOption extended = ExtendedOption.Unspecified) {
			return await SendAsync(new TraktSyncWatchedShowsRequest(Client) { Extended = extended });
		}

		/// <summary>Get a user's ratings filtered by type. You can optionally filter for a specific rating between 1 and 10.</summary>
		/// <param name="rating">The rating</param>
		/// <param name="extended">Changes which properties are populated for standard media objects. By default, minimal data is returned. Change this if additional fields are required in the returned data.</param>
		/// <returns>See summary</returns>
		public async Task<IEnumerable<TraktRatingsMoviesResponseItem>> GetMovieRatingsAsync(Rating rating = Rating.RatingUnspecified, ExtendedOption extended = ExtendedOption.Unspecified) {
			return await SendAsync(new TraktSyncRatingsMoviesRequest(Client) { Rating = rating, Extended = extended });
		}

		/// <summary>Get a user's ratings filtered by type. You can optionally filter for a specific rating between 1 and 10.</summary>
		/// <param name="rating">The rating</param>
		/// <param name="extended">Changes which properties are populated for standard media objects. By default, minimal data is returned. Change this if additional fields are required in the returned data.</param>
		/// <returns>See summary</returns>
		public async Task<IEnumerable<TraktRatingsShowsResponseItem>> GetShowRatingsAsync(Rating rating = Rating.RatingUnspecified, ExtendedOption extended = ExtendedOption.Unspecified) {
			return await SendAsync(new TraktSyncRatingsShowsRequest(Client) { Rating = rating, Extended = extended });
		}

		/// <summary>Get a user's ratings filtered by type. You can optionally filter for a specific rating between 1 and 10.</summary>
		/// <param name="rating">The rating</param>
		/// <param name="extended">Changes which properties are populated for standard media objects. By default, minimal data is returned. Change this if additional fields are required in the returned data.</param>
		/// <returns>See summary</returns>
		public async Task<IEnumerable<TraktRatingsSeasonsResponseItem>> GetSeasonRatingsAsync(Rating rating = Rating.RatingUnspecified, ExtendedOption extended = ExtendedOption.Unspecified) {
			return await SendAsync(new TraktSyncRatingsSeasonsRequest(Client) { Rating = rating, Extended = extended });
		}

		/// <summary>Get a user's ratings filtered by type. You can optionally filter for a specific rating between 1 and 10.</summary>
		/// <param name="rating">The rating</param>
		/// <param name="extended">Changes which properties are populated for standard media objects. By default, minimal data is returned. Change this if additional fields are required in the returned data.</param>
		/// <returns>See summary</returns>
		public async Task<IEnumerable<TraktRatingsEpisodesResponseItem>> GetEpisodeRatingsAsync(Rating rating = Rating.RatingUnspecified, ExtendedOption extended = ExtendedOption.Unspecified) {
			return await SendAsync(new TraktSyncRatingsEpisodesRequest(Client) { Rating = rating, Extended = extended });
		}

		/// <summary>Returns all items in a user's watchlist filtered by type. When an item is watched, it will be automatically removed from the watchlist. To track what the user is actively watching, use the progress APIs.</summary>
		/// <param name="extended">Changes which properties are populated for standard media objects. By default, minimal data is returned. Change this if additional fields are required in the returned data.</param>
		/// <returns>See summary</returns>
		public async Task<IEnumerable<TraktWatchlistMoviesResponseItem>> GetWatchlistMoviesAsync(ExtendedOption extended = ExtendedOption.Unspecified) {
			return await SendAsync(new TraktSyncWatchlistMoviesRequest(Client) { Extended = extended });
		}

		/// <summary>Returns all items in a user's watchlist filtered by type. When an item is watched, it will be automatically removed from the watchlist. To track what the user is actively watching, use the progress APIs.</summary>
		/// <param name="extended">Changes which properties are populated for standard media objects. By default, minimal data is returned. Change this if additional fields are required in the returned data.</param>
		/// <returns>See summary</returns>
		public async Task<IEnumerable<TraktWatchlistShowsResponseItem>> GetWatchlistShowsAsync(ExtendedOption extended = ExtendedOption.Unspecified) {
			return await SendAsync(new TraktSyncWatchlistShowsRequest(Client) { Extended = extended });
		}

		/// <summary>Returns all items in a user's watchlist filtered by type. When an item is watched, it will be automatically removed from the watchlist. To track what the user is actively watching, use the progress APIs.</summary>
		/// <param name="extended">Changes which properties are populated for standard media objects. By default, minimal data is returned. Change this if additional fields are required in the returned data.</param>
		/// <returns>See summary</returns>
		public async Task<IEnumerable<TraktWatchlistSeasonsResponseItem>> GetWatchlistSeasonsAsync(ExtendedOption extended = ExtendedOption.Unspecified) {
			return await SendAsync(new TraktSyncWatchlistSeasonsRequest(Client) { Extended = extended });
		}

		/// <summary>Returns all items in a user's watchlist filtered by type. When an item is watched, it will be automatically removed from the watchlist. To track what the user is actively watching, use the progress APIs.</summary>
		/// <param name="extended">Changes which properties are populated for standard media objects. By default, minimal data is returned. Change this if additional fields are required in the returned data.</param>
		/// <returns>See summary</returns>
		public async Task<IEnumerable<TraktWatchlistEpisodesResponseItem>> GetWatchlistEpisodesAsync(ExtendedOption extended = ExtendedOption.Unspecified) {
			return await SendAsync(new TraktSyncWatchlistEpisodesRequest(Client) { Extended = extended });
		}

	}

}