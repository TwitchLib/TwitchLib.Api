﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Models.V5.Search;

namespace TwitchLib.Api.Sections
{
    public class Search
    {
        public Search(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
        {
            V5 = new V5Api(settings, rateLimiter, http);
        }

        public V5Api V5 { get; }

        public class V5Api : ApiBase
        {
            public V5Api(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
            {
            }

            #region SearchChannels

            public Task<SearchChannels> SearchChannelsAsync(string encodedSearchQuery, int? limit = null, int? offset = null)
            {
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("query", encodedSearchQuery)
                };

                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));

                return TwitchGetGenericAsync<SearchChannels>("/search/channels", ApiVersion.v5, getParams);
            }

            #endregion

            #region SearchGames

            public Task<SearchGames> SearchGamesAsync(string encodedSearchQuery, bool? live = null)
            {
                var getParams = new List<KeyValuePair<string, string>>
                {
                        new KeyValuePair<string, string>("query", encodedSearchQuery)
                };

                if (live.HasValue) getParams.Add(live.Value ? new KeyValuePair<string, string>("live", "true") : new KeyValuePair<string, string>("live", "false"));

                return TwitchGetGenericAsync<SearchGames>("/search/games", ApiVersion.v5, getParams);
            }

            #endregion

            #region SearchStreams

            public Task<SearchStreams> SearchStreamsAsync(string encodedSearchQuery, int? limit = null, int? offset = null, bool? hls = null)
            {
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("query", encodedSearchQuery)
                };

                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));
                if (hls.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("hls", hls.Value.ToString()));

                return TwitchGetGenericAsync<SearchStreams>("/search/streams", ApiVersion.v5, getParams);
            }

            #endregion
        }
    }
}