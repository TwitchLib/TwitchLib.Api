using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Models.v5.Search;

namespace TwitchLib.Api.Sections
{
    public class Search
    {
        public Search(TwitchAPI api)
        {
            v5 = new V5Api(api);
        }

        public V5Api v5 { get; }

        public class V5Api : ApiSection
        {
            public V5Api(TwitchAPI api) : base(api)
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

                return Api.TwitchGetGenericAsync<SearchChannels>("/search/channels", ApiVersion.v5, getParams);
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

                return Api.TwitchGetGenericAsync<SearchGames>("/search/games", ApiVersion.v5, getParams);
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

                return Api.TwitchGetGenericAsync<SearchStreams>("/search/streams", ApiVersion.v5, getParams);
            }

            #endregion
        }
    }
}