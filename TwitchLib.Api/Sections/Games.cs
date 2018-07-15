﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Models.Helix.Games.GetGames;
using TwitchLib.Api.Models.Helix.Games.GetTopGames;
using TwitchLib.Api.Models.V5.Games;

namespace TwitchLib.Api.Sections
{
    public class Games
    {
        public Games(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
        {
            V5 = new V5Api(settings, rateLimiter, http);
            Helix = new HelixApi(settings, rateLimiter, http);
        }

        public V5Api V5 { get; }
        public HelixApi Helix { get; }

        public class V5Api : ApiBase
        {
            public V5Api(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
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

                return TwitchGetGenericAsync<TopGames>("/games/top", ApiVersion.v5, getParams);
            }

            #endregion
        }

        public class HelixApi : ApiBase
        {
            public HelixApi(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
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

                return TwitchGetGenericAsync<GetGamesResponse>("/games", ApiVersion.Helix, getParams);
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

                return TwitchGetGenericAsync<GetTopGamesResponse>("/games/top", ApiVersion.Helix, getParams);
            }

            #endregion
        }
    }
}