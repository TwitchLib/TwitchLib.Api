using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;

namespace TwitchLib.Api.Sections
{
    public class Games
    {
        public Games(TwitchAPI api)
        {
            v5 = new V5(api);
            helix = new Helix(api);
        }
        
        public V5 v5 { get; }
        public Helix helix { get; }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetTopGames
            public async Task<Models.v5.Games.TopGames> GetTopGamesAsync(int? limit = null, int? offset = null)
            {
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));
                
                return await Api.TwitchGetGenericAsync<Models.v5.Games.TopGames>("/games/top", ApiVersion.v5, getParams).ConfigureAwait(false);
            }
            #endregion
        }

        public class Helix : ApiSection
        {
            public Helix(TwitchAPI api) : base(api)
            {
            }
            #region GetGames
            public async Task<Models.Helix.Games.GetGames.GetGamesResponse> GetGamesAsync(List<string> gameIds = null, List<string> gameNames = null)
            {
                if (gameIds == null && gameNames == null ||
                    gameIds != null && gameIds.Count == 0 && gameNames == null ||
                    gameNames != null && gameNames.Count == 0 && gameIds == null)
                    throw new BadParameterException("Either gameIds or gameNames must have at least one value");
                if (gameIds != null && gameIds.Count > 100)
                    throw new BadParameterException("gameIds list cannot exceed 100 items");
                if (gameNames != null && gameNames.Count > 100)
                    throw new BadParameterException("gameNames list cannot exceed 100 items");

                var getParams = new List<KeyValuePair<string, string>>();
                if (gameIds != null && gameIds.Count > 0)
                    foreach (var gameId in gameIds)
                        getParams.Add(new KeyValuePair<string, string>("id", gameId));
                if (gameNames != null && gameNames.Count > 0)
                    foreach (var gameName in gameNames)
                        getParams.Add(new KeyValuePair<string, string>("name", gameName));
                
                return await Api.TwitchGetGenericAsync<Models.Helix.Games.GetGames.GetGamesResponse>("/games", ApiVersion.Helix, getParams).ConfigureAwait(false);
            }
            #endregion
        }

    }
}
