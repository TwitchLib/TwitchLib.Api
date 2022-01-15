using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.V5.Models.Search;

namespace TwitchLib.Api.V5
{
    [Obsolete("This is a v5 class, please use a Helix class. All v5 calls will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
    public class Search : ApiBase
    {
        public Search(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region SearchChannels
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
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

            return TwitchGetGenericAsync<SearchChannels>("/search/channels", ApiVersion.V5, getParams);
        }

        #endregion

        #region SearchGames
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task<SearchGames> SearchGamesAsync(string encodedSearchQuery, bool? live = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
                {
                        new KeyValuePair<string, string>("query", encodedSearchQuery)
                };

            if (live.HasValue) getParams.Add(live.Value ? new KeyValuePair<string, string>("live", "true") : new KeyValuePair<string, string>("live", "false"));

            return TwitchGetGenericAsync<SearchGames>("/search/games", ApiVersion.V5, getParams);
        }

        #endregion

        #region SearchStreams
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
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

            return TwitchGetGenericAsync<SearchStreams>("/search/streams", ApiVersion.V5, getParams);
        }

        #endregion
    }

}