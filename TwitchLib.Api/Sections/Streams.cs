using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Models.Helix.Streams;
using TwitchLib.Api.Models.Helix.StreamsMetadata;
using TwitchLib.Api.Models.v5.Streams;
using LiveStreams = TwitchLib.Api.Models.v5.Streams.LiveStreams;

namespace TwitchLib.Api.Sections
{
    public class Streams
    {
        public Streams(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
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

            #region GetStreamByUser

            public Task<StreamByUser> GetStreamByUserAsync(string channelId, string streamType = null)
            {
                if (string.IsNullOrWhiteSpace(channelId))
                    throw new BadParameterException("The channel id is not valid for fetching streams. It is not allowed to be null, empty or filled with whitespaces.");

                var getParams = new List<KeyValuePair<string, string>>();
                if (!string.IsNullOrWhiteSpace(streamType) && (streamType == "live" || streamType == "playlist" || streamType == "all" || streamType == "watch_party"))
                    getParams.Add(new KeyValuePair<string, string>("stream_type", streamType));

                return TwitchGetGenericAsync<StreamByUser>($"/streams/{channelId}", ApiVersion.v5, getParams);
            }

            #endregion

            #region GetLiveStreams

            public Task<LiveStreams> GetLiveStreamsAsync(List<string> channelList = null, string game = null, string language = null, string streamType = null, int? limit = null, int? offset = null)
            {
                var getParams = new List<KeyValuePair<string, string>>();
                if (channelList != null && channelList.Count > 0)
                    getParams.Add(new KeyValuePair<string, string>("channel", string.Join(",", channelList)));
                if (!string.IsNullOrWhiteSpace(game))
                    getParams.Add(new KeyValuePair<string, string>("game", game));
                if (!string.IsNullOrWhiteSpace(language))
                    getParams.Add(new KeyValuePair<string, string>("language", language));
                if (!string.IsNullOrWhiteSpace(streamType) && (streamType == "live" || streamType == "playlist" || streamType == "all" || streamType == "watch_party"))
                    getParams.Add(new KeyValuePair<string, string>("stream_type", streamType));
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));

                return TwitchGetGenericAsync<LiveStreams>("/streams", ApiVersion.v5, getParams);
            }

            #endregion

            #region GetStreamsSummary

            public Task<StreamsSummary> GetStreamsSummaryAsync(string game = null)
            {
                var getParams = new List<KeyValuePair<string, string>>();
                if (game != null)
                    getParams.Add(new KeyValuePair<string, string>("game", game));

                return TwitchGetGenericAsync<StreamsSummary>("/streams/summary", ApiVersion.v5, getParams);
            }

            #endregion

            #region GetFeaturedStreams

            public Task<FeaturedStreams> GetFeaturedStreamAsync(int? limit = null, int? offset = null)
            {
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));

                return TwitchGetGenericAsync<FeaturedStreams>("/streams/featured", ApiVersion.v5, getParams);
            }

            #endregion

            #region GetFollowedStreams

            public Task<FollowedStreams> GetFollowedStreamsAsync(string streamType = null, int? limit = null, int? offset = null, string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.User_Read, authToken);
                var getParams = new List<KeyValuePair<string, string>>();
                if (!string.IsNullOrWhiteSpace(streamType) && (streamType == "live" || streamType == "playlist" || streamType == "all" || streamType == "watch_party"))
                    getParams.Add(new KeyValuePair<string, string>("stream_type", streamType));
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.ToString()));

                return TwitchGetGenericAsync<FollowedStreams>("/streams/followed", ApiVersion.v5, getParams, authToken);
            }

            #endregion

            #region GetUptime

            public async Task<TimeSpan?> GetUptimeAsync(string channelId)
            {
                try
                {
                    var stream = await GetStreamByUserAsync(channelId).ConfigureAwait(false);
                    return DateTime.UtcNow - stream.Stream.CreatedAt;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            #endregion

            #region BroadcasterOnline

            public async Task<bool> BroadcasterOnlineAsync(string channelId)
            {
                var res = await GetStreamByUserAsync(channelId).ConfigureAwait(false);
                return res.Stream != null;
            }

            #endregion
        }

        public class HelixApi : ApiBase
        {
            public HelixApi(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
            {
            }

            public Task<GetStreamsResponse> GetStreamsAsync(string after = null, List<string> communityIds = null, int first = 20, List<string> gameIds = null, List<string> languages = null, string type = "all", List<string> userIds = null, List<string> userLogins = null)
            {
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("first", first.ToString()),
                    new KeyValuePair<string, string>("type", type)
                };
                if (after != null)
                    getParams.Add(new KeyValuePair<string, string>("after", after));
                if (communityIds != null && communityIds.Count > 0)
                {
                    foreach (var communityId in communityIds)
                        getParams.Add(new KeyValuePair<string, string>("community_id", communityId));
                }

                if (gameIds != null && gameIds.Count > 0)
                {
                    foreach (var gameId in gameIds)
                        getParams.Add(new KeyValuePair<string, string>("game_id", gameId));
                }

                if (languages != null && languages.Count > 0)
                {
                    foreach (var language in languages)
                        getParams.Add(new KeyValuePair<string, string>("language", language));
                }

                if (userIds != null && userIds.Count > 0)
                {
                    foreach (var userId in userIds)
                        getParams.Add(new KeyValuePair<string, string>("user_id", userId));
                }

                if (userLogins != null && userLogins.Count > 0)
                {
                    foreach (var userLogin in userLogins)
                        getParams.Add(new KeyValuePair<string, string>("user_login", userLogin));
                }

                return TwitchGetGenericAsync<GetStreamsResponse>($"/streams", ApiVersion.Helix, getParams);
            }

            public Task<GetStreamsMetadataResponse> GetStreamsMetadataAsync(string after = null, List<string> communityIds = null, int first = 20, List<string> gameIds = null, List<string> languages = null, string type = "all", List<string> userIds = null, List<string> userLogins = null)
            {
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("first", first.ToString()),
                    new KeyValuePair<string, string>("type", type)
                };
                if (after != null)
                    getParams.Add(new KeyValuePair<string, string>("after", after));
                if (communityIds != null && communityIds.Count > 0)
                {
                    foreach (var communityId in communityIds)
                        getParams.Add(new KeyValuePair<string, string>("community_id", communityId));
                }

                if (gameIds != null && gameIds.Count > 0)
                {
                    foreach (var gameId in gameIds)
                        getParams.Add(new KeyValuePair<string, string>("game_id", gameId));
                }

                if (languages != null && languages.Count > 0)
                {
                    foreach (var language in languages)
                        getParams.Add(new KeyValuePair<string, string>("language", language));
                }

                if (userIds != null && userIds.Count > 0)
                {
                    foreach (var userId in userIds)
                        getParams.Add(new KeyValuePair<string, string>("user_id", userId));
                }

                if (userLogins != null && userLogins.Count > 0)
                {
                    foreach (var userLogin in userLogins)
                        getParams.Add(new KeyValuePair<string, string>("user_login", userLogin));
                }

                return TwitchGetGenericAsync<GetStreamsMetadataResponse>("/streams/metadata", ApiVersion.Helix, getParams);
            }
        }
    }
}