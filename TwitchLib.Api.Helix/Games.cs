using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Games;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Games related APIs
    /// </summary>
    public class Games : ApiBase
    {
        public Games(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetGames
        /// <summary>
        /// Gets game information by game ID or name.
        /// <para>For a query to be valid, name and/or id must be specified.</para>
        /// </summary>
        /// <param name="gameIds">List of Game IDs. At most 100 id values can be specified.</param>
        /// <param name="gameNames">
        /// List of Game names.
        /// <para>The name must be an exact match. For example, “Pokemon” will not return a list of Pokemon games; instead, query any specific Pokemon games in which you are interested.</para>
        /// <para>At most 100 name values can be specified.</para>
        /// </param>
        /// <param name="igdbIds">List of IGDB Game Ids. At most 100 id values can be specified.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetGamesResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetGamesResponse> GetGamesAsync(List<string> gameIds = null, List<string> gameNames = null, List<string> igdbIds = null, string accessToken = null)
        {
            if (gameIds == null && gameNames == null && igdbIds == null 
                || gameIds != null && gameIds.Count == 0 && gameNames == null && igdbIds == null 
                || gameNames != null && gameNames.Count == 0 && gameIds == null && igdbIds == null
                || igdbIds != null && igdbIds.Count == 0 && gameIds == null && gameNames == null)
                throw new BadParameterException("Either gameIds, gameNames or igdbIds must have at least one value");

            if (gameIds != null && gameIds.Count > 100)
                throw new BadParameterException("gameIds list cannot exceed 100 items");

            if (gameNames != null && gameNames.Count > 100)
                throw new BadParameterException("gameNames list cannot exceed 100 items");

            if (igdbIds != null && igdbIds.Count > 100)
                throw new BadParameterException("igdbIds list cannot exceed 100 items");

            if (gameIds?.Count + gameNames?.Count + igdbIds?.Count > 100)
                throw new BadParameterException("The combined amount of items of gameIds, gameNames and igdbIds cannot exceed 100 items");

            var getParams = new List<KeyValuePair<string, string>>();

            if (gameIds != null && gameIds.Count > 0)
            {
                getParams.AddRange(gameIds.Select(gameId => new KeyValuePair<string, string>("id", gameId)));
            }

            if (gameNames != null && gameNames.Count > 0)
            {
                getParams.AddRange(gameNames.Select(gameName => new KeyValuePair<string, string>("name", gameName)));
            }

            if (igdbIds != null && igdbIds.Count > 0)
            {
                getParams.AddRange(igdbIds.Select(igdbId => new KeyValuePair<string, string>("igdb_id", igdbId)));
            }

            return TwitchGetGenericAsync<GetGamesResponse>("/games", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region GetTopGames
        /// <summary>
        /// Gets games sorted by number of current viewers on Twitch, most popular first.
        /// </summary>
        /// <param name="before">Cursor for backward pagination: tells the server where to start fetching the next set of results, in a multi-page response. </param>
        /// <param name="after">Cursor for forward pagination: tells the server where to start fetching the next set of results, in a multi-page response. </param>
        /// <param name="first">Maximum number of objects to return. Maximum: 100. Default: 20.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetTopGamesResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetTopGamesResponse> GetTopGamesAsync(string before = null, string after = null, int first = 20, string accessToken = null)
        {
            if (first < 0 || first > 100)
                throw new BadParameterException("'first' parameter must be between 1 (inclusive) and 100 (inclusive).");

            var getParams = new List<KeyValuePair<string, string>>
                {
                        new KeyValuePair<string, string>("first", first.ToString())
                };

            if (!string.IsNullOrWhiteSpace(before))
                getParams.Add(new KeyValuePair<string, string>("before", before));

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetTopGamesResponse>("/games/top", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

    }
}