using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;
using TwitchLib.Api.Models.Helix.Games.GetGames;
using TwitchLib.Api.Models.Helix.Games.GetTopGames;
using TwitchLib.Api.Models.v5.Games;

namespace TwitchLib.Api.Sections
{
    public class Games
    {
        public Games(TwitchAPI api)
        {
            v5 = new V5Api(api);
            helix = new HelixApi(api);
        }

        public V5Api v5 { get; }
        public HelixApi helix { get; }

        public class V5Api : ApiSection
        {
            public V5Api(TwitchAPI api) : base(api)
            {
            }

            #region GetTopGames

            public Task<TopGames> GetTopGamesAsync(int? limit = null, int? offset = null)
            {
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));

                return Api.TwitchGetGenericAsync<TopGames>("/games/top", ApiVersion.v5, getParams);
            }

            #endregion
        }

        public class HelixApi : ApiSection
        {
            public HelixApi(TwitchAPI api) : base(api)
            {
            }

            #region GetGames

            public Task<GetGamesResponse> GetGamesAsync(List<string> gameIds = null, List<string> gameNames = null)
            {
                if (gameIds == null && gameNames == null || gameIds != null && gameIds.Count == 0 && gameNames == null || gameNames != null && gameNames.Count == 0 && gameIds == null)
                    throw new BadParameterException("Either gameIds or gameNames must have at least one value");

                if (gameIds != null && gameIds.Count > 100)
                    throw new BadParameterException("gameIds list cannot exceed 100 items");

                if (gameNames != null && gameNames.Count > 100)
                    throw new BadParameterException("gameNames list cannot exceed 100 items");

                var getParams = new List<KeyValuePair<string, string>>();
                if (gameIds != null && gameIds.Count > 0)
                {
                    foreach (var gameId in gameIds)
                        getParams.Add(new KeyValuePair<string, string>("id", gameId));
                }

                if (gameNames != null && gameNames.Count > 0)
                {
                    foreach (var gameName in gameNames)
                        getParams.Add(new KeyValuePair<string, string>("name", gameName));
                }

                return Api.TwitchGetGenericAsync<GetGamesResponse>("/games", ApiVersion.Helix, getParams);
            }

            #endregion

            #region GetTopGames

            public Task<GetTopGamesResponse> GetTopGamesAsync(string before = null, string after = null, int first = 20)
            {
                if (first < 0 || first > 100)
                    throw new BadParameterException("'first' parameter must be between 1 (inclusive) and 100 (inclusive).");

                var getParams = new List<KeyValuePair<string, string>>
                {
                        new KeyValuePair<string, string>("first", first.ToString())
                };

                if (before != null)
                    getParams.Add(new KeyValuePair<string, string>("before", before));
                if (after != null)
                    getParams.Add(new KeyValuePair<string, string>("after", after));

                return Api.TwitchGetGenericAsync<GetTopGamesResponse>("/games/top", ApiVersion.Helix, getParams);
            }

            #endregion
        }
    }
}