using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Search;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Search related APIs
    /// </summary>
    public class Search : ApiBase
    {
        public Search(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region SearchCategories

        /// <summary>
        /// Returns a list of games or categories that match the query via name either entirely or partially.
        /// </summary>
        /// <param name="encodedSearchQuery">URl encoded search query</param>
        /// <param name="after">Cursor for forward pagination: tells the server where to start fetching the next set of results, in a multi-page response.</param>
        /// <param name="first">
        /// Maximum number of objects to return.
        /// <para>Default: 20. Maximum: 100.</para>
        /// </param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="SearchCategoriesResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<SearchCategoriesResponse> SearchCategoriesAsync(string encodedSearchQuery, string after = null, int first = 20, string accessToken = null)
        {
            if (first < 0 || first > 100)
                throw new BadParameterException("'first' parameter must be between 1 (inclusive) and 100 (inclusive).");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("query", encodedSearchQuery)
            };

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            getParams.Add(new KeyValuePair<string, string>("first", first.ToString()));

            return TwitchGetGenericAsync<SearchCategoriesResponse>("/search/categories", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region SearchChannels

        /// <summary>
        /// Returns a list of channels (users who have streamed within the past 6 months) that match the query via channel name or description either entirely or partially.
        /// <para>Results include both live and offline channels.</para>
        /// <para> Online channels will have additional metadata (e.g. started_at, tag_ids).</para>
        /// </summary>
        /// <param name="encodedSearchQuery">URl encoded search query</param>
        /// <param name="liveOnly">	Filter results for live streams only. Default: false</param>
        /// <param name="after">Cursor for forward pagination: tells the server where to start fetching the next set of results, in a multi-page response.</param>
        /// <param name="first">
        /// Maximum number of objects to return.
        /// <para>Default: 20. Maximum: 100.</para>
        /// </param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<SearchChannelsResponse> SearchChannelsAsync(string encodedSearchQuery, bool liveOnly = false, string after = null, int first = 20, string accessToken = null)
        {
            if (first < 0 || first > 100)
                throw new BadParameterException("'first' parameter must be between 1 (inclusive) and 100 (inclusive).");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("query", encodedSearchQuery),
                new KeyValuePair<string, string>("live_only", liveOnly.ToString())
            };

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            getParams.Add(new KeyValuePair<string, string>("first", first.ToString()));

            return TwitchGetGenericAsync<SearchChannelsResponse>("/search/channels", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion
    }
}
