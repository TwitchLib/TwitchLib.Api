using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;

namespace TwitchLib.Api.Sections
{
    public class Channels
    {
        public Channels(TwitchAPI api)
        {
            v5 = new V5Api(api);
        }

        public V5Api v5 { get; }

        public class V5Api : ApiSection
        {
            public V5Api(TwitchAPI api) : base(api)
            {
            }

            #region GetChannel

            /// <summary>
            /// [ASYNC] Gets a channel object based on a specified OAuth token.<para/>
            /// Get Channel returns more data than Get Channel by ID because Get Channel is privileged.<para/>
            /// <para>Required Authentication Scope: channel_read</para>
            /// </summary>
            /// <returns>A ChannelPrivileged object including all Channel object info plus email and streamkey.</returns>
            public Task<Models.v5.Channels.ChannelAuthed> GetChannelAsync(string authToken = null)
            {
                return Api.TwitchGetGenericAsync<Models.v5.Channels.ChannelAuthed>("/channel", ApiVersion.v5, accessToken: authToken);
            }

            #endregion

            #region GetChannelById

            /// <summary>
            /// [ASYNC] Gets a speicified channel object.<para/>
            /// </summary>
            /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
            /// <returns>A Channel object from the response of the Twitch API.</returns>
            public Task<Models.v5.Channels.Channel> GetChannelByIDAsync(string channelId)
            {
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");
                }

                return Api.TwitchGetGenericAsync<Models.v5.Channels.Channel>($"/channels/{channelId}", ApiVersion.v5);
            }

            #endregion

            #region UpdateChannel

            /// <summary>
            /// [ASYNC] Updates specified properties of a specified channel.<para/>
            /// In the request, the new properties are specified as a JSON object representation.<para/>
            /// <para>Required Authentication Scopes: To update delay or channel_feed_enabled parameter: a channel_editor token from the channel owner. To update other parameters: channel_editor.</para>
            /// </summary>
            /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
            /// <param name="status">Description of the broadcaster’s status, displayed as a title on the channel page.</param>
            /// <param name="game">Name of the game.</param>
            /// <param name="delay">Channel delay, in seconds. This inserts a delay in the live feed. Requires the channel owner’s OAuth token.</param>
            /// <param name="channelFeedEnabled">If true, the channel’s feed is turned on. Requires the channel owner’s OAuth token. Default: false.</param>
            /// <param name="authToken"></param>
            /// <returns>A Channel object with the newly changed properties.</returns>
            public Task<Models.v5.Channels.Channel> UpdateChannelAsync(string channelId, string status = null, string game = null, string delay = null, bool? channelFeedEnabled = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Helix_User_Edit_Broadcast, authToken);
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");
                }

                var datas = new List<KeyValuePair<string, string>>();
                if (!string.IsNullOrEmpty(status))
                    datas.Add(new KeyValuePair<string, string>("status", "\"" + status + "\""));
                if (!string.IsNullOrEmpty(game))
                    datas.Add(new KeyValuePair<string, string>("game", "\"" + game + "\""));
                if (!string.IsNullOrEmpty(delay))
                    datas.Add(new KeyValuePair<string, string>("delay", "\"" + delay + "\""));
                if (channelFeedEnabled.HasValue)
                    datas.Add(new KeyValuePair<string, string>("channel_feed_enabled", channelFeedEnabled == true ? "true" : "false"));

                var payload = "";
                switch (datas.Count)
                {
                    case 0:
                        throw new BadParameterException("At least one parameter must be specified: status, game, delay, channel_feed_enabled.");
                    case 1:
                        payload = $"\"{datas[0].Key}\": {datas[0].Value}";
                        break;
                    default:
                        for (var i = 0; i < datas.Count; i++)
                        {
                            payload = datas.Count - i > 1 ? $"{payload}\"{datas[i].Key}\": {datas[i].Value}," : $"{payload}\"{datas[i].Key}\": {datas[i].Value}";
                        }

                        break;
                }

                payload = "{ \"channel\": {" + payload + "} }";

                return Api.TwitchPutGenericAsync<Models.v5.Channels.Channel>($"/channels/{channelId}", ApiVersion.v5, payload, accessToken: authToken);
            }

            #endregion

            #region GetChannelEditors

            /// <summary>
            /// <para>[ASYNC] Gets a list of users who are editors for a specified channel.</para>
            /// <para>Required Authentication Scope: channel_read</para>
            /// </summary>
            /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
            /// <param name="authToken"></param>
            /// <returns>A ChannelEditors object that contains an array of the Users which are Editor of the channel.</returns>
            public Task<Models.v5.Channels.ChannelEditors> GetChannelEditorsAsync(string channelId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Read, authToken);
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");
                }

                return Api.TwitchGetGenericAsync<Models.v5.Channels.ChannelEditors>($"/channels/{channelId}/editors", ApiVersion.v5, accessToken: authToken);
            }

            #endregion

            #region GetChannelFollowers

            /// <summary>
            /// <para>[ASYNC] Gets a list of users who follow a specified channel, sorted by the date when they started following the channel (newest first, unless specified otherwise).</para>
            /// </summary>
            /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
            /// <param name="limit">Maximum number of objects to return. Default: 25. Maximum: 100.</param>
            /// <param name="offset">Object offset for pagination of results. Default: 0.</param>
            /// <param name="cursor">Tells the server where to start fetching the next set of results, in a multi-page response.</param>
            /// <param name="direction">Sorting direction. Valid values: "asc", "desc" (newest first). Default: "desc".</param>
            /// <returns>A ChannelFollowers object that represents the response from the Twitch API.</returns>
            public Task<Models.v5.Channels.ChannelFollowers> GetChannelFollowersAsync(string channelId, int? limit = null, int? offset = null, string cursor = null, string direction = null)
            {
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");
                }

                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    getParams.Add(new KeyValuePair<string, string>("cursor", cursor));
                if (!string.IsNullOrEmpty(direction) && (direction == "asc" || direction == "desc"))
                    getParams.Add(new KeyValuePair<string, string>("direction", direction));

                return Api.TwitchGetGenericAsync<Models.v5.Channels.ChannelFollowers>($"/channels/{channelId}/follows", ApiVersion.v5, getParams);
            }

            #endregion

            #region GetAllChannelFollowers

            /// <summary>
            /// [ASYNC] Gets all of the followers a channel has. THIS IS A VERY EXPENSIVE CALL AND CAN TAKE A LONG TIME IF THE CHANNEL HAS A LOT OF FOLLOWERS. NOT RECOMMENDED.
            /// </summary>
            /// <param name="channelId">THe specified channelId of the channel to get the information from.</param>
            /// <returns></returns>
            public async Task<List<Models.v5.Channels.ChannelFollow>> GetAllFollowersAsync(string channelId)
            {
                var followers = new List<Models.v5.Channels.ChannelFollow>();
                var firstBatch = await GetChannelFollowersAsync(channelId, 100).ConfigureAwait(false);
                var totalFollowers = firstBatch.Total;
                var cursor = firstBatch.Cursor;
                followers.AddRange(firstBatch.Follows.OfType<Models.v5.Channels.ChannelFollow>().ToList());

                // math stuff
                var amount = firstBatch.Follows.Length;
                var leftOverFollowers = (totalFollowers - amount) % 100;
                var requiredRequests = (totalFollowers - amount - leftOverFollowers) / 100;

                await Task.Delay(1000);
                for (var i = 0; i < requiredRequests; i++)
                {
                    var requestedFollowers = await GetChannelFollowersAsync(channelId, 100, cursor: cursor).ConfigureAwait(false);
                    cursor = requestedFollowers.Cursor;
                    followers.AddRange(requestedFollowers.Follows.OfType<Models.v5.Channels.ChannelFollow>().ToList());

                    // we should wait a second before performing another request per Twitch requirements
                    await Task.Delay(1000);
                }

                // get leftover subs
                var leftOverFollowersRequest = await GetChannelFollowersAsync(channelId, limit: leftOverFollowers, cursor: cursor).ConfigureAwait(false);
                followers.AddRange(leftOverFollowersRequest.Follows.OfType<Models.v5.Channels.ChannelFollow>().ToList());

                return followers;
            }

            #endregion

            #region GetChannelTeams

            /// <summary>
            /// <para>[ASYNC] Gets a list of teams to which a specified channel belongs.</para>
            /// </summary>
            /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
            /// <returns>An Array of the Teams the Channel belongs to.</returns>
            public Task<Models.v5.Channels.ChannelTeams> GetChannelTeamsAsync(string channelId)
            {
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");
                }

                return Api.TwitchGetGenericAsync<Models.v5.Channels.ChannelTeams>($"/channels/{channelId}/teams", ApiVersion.v5);
            }

            #endregion

            #region GetChannelSubscribers

            /// <summary>
            /// <para>[ASYNC] Gets a list of users subscribed to a specified channel, sorted by the date when they subscribed.</para>
            /// <para>Required Authentication Scope: channel_subscriptions</para>
            /// </summary>
            /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
            /// <param name="limit">Maximum number of objects to return. Default: 25. Maximum: 100.</param>
            /// <param name="offset">Object offset for pagination of results. Default: 0.</param>
            /// <param name="direction">Sorting direction. Valid values: "asc", "desc" (newest first). Default: "desc".</param>
            /// <param name="authToken">The associated auth token for this request.</param>
            /// <returns></returns>
            public Task<Models.v5.Channels.ChannelSubscribers> GetChannelSubscribersAsync(string channelId, int? limit = null, int? offset = null, string direction = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Subscriptions, authToken);
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");
                }

                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
                if (!string.IsNullOrEmpty(direction) && (direction == "asc" || direction == "desc"))
                    getParams.Add(new KeyValuePair<string, string>("direction", direction));

                return Api.TwitchGetGenericAsync<Models.v5.Channels.ChannelSubscribers>($"/channels/{channelId}/subscriptions", ApiVersion.v5, getParams, authToken);
            }

            #endregion

            #region GetAllSubscribers

            /// <summary>
            /// [ASYNC] Makes a number of calls to get all subscriber objects belonging to a channel. THIS IS AN EXPENSIVE OPERATION.
            /// </summary>
            /// <param name="channelId">ChannelId indicating channel to get subs from.</param>
            /// <param name="accessToken">The associated auth token for this request.</param>
            /// <returns></returns>
            public async Task<List<Models.v5.Subscriptions.Subscription>> GetAllSubscribersAsync(string channelId, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Subscriptions, accessToken);
                // initial stuffs
                var allSubs = new List<Models.v5.Subscriptions.Subscription>();
                var firstBatch = await GetChannelSubscribersAsync(channelId, 100, 0, "asc", accessToken);
                var totalSubs = firstBatch.Total;
                allSubs.AddRange(firstBatch.Subscriptions);

                // math stuff to determine left over and number of requests
                var amount = firstBatch.Subscriptions.Length;
                var leftOverSubs = (totalSubs - amount) % 100;
                var requiredRequests = (totalSubs - amount - leftOverSubs) / 100;

                // perform required requests after initial delay
                var currentOffset = amount;
                await Task.Delay(1000);
                for (var i = 0; i < requiredRequests; i++)
                {
                    var requestedSubs = await GetChannelSubscribersAsync(channelId, 100, currentOffset, "asc", accessToken).ConfigureAwait(false);
                    allSubs.AddRange(requestedSubs.Subscriptions);
                    currentOffset += requestedSubs.Subscriptions.Length;

                    // We should wait a second before performing another request per Twitch requirements
                    await Task.Delay(1000);
                }

                // get leftover subs
                var leftOverSubsRequest = await GetChannelSubscribersAsync(channelId, leftOverSubs, currentOffset, "asc", accessToken).ConfigureAwait(false);
                allSubs.AddRange(leftOverSubsRequest.Subscriptions);

                return allSubs;
            }

            #endregion

            #region CheckChannelSubscriptionByUser

            /// <summary>
            /// <para>[ASYNC] Checks if a specified channel has a specified user subscribed to it. Intended for use by channel owners.</para>
            /// <para>Returns a subscription object which includes the user if that user is subscribed. Requires authentication for the channel.</para>
            /// <para>Required Authentication Scope: channel_check_subscription</para>
            /// </summary>
            /// <param name="channelId">The specified channel to check the subscription on.</param>
            /// <param name="userId">The specified user to check for.</param>
            /// <param name="authToken"></param>
            /// <returns>Returns a subscription object or null if not subscribed.</returns>
            public Task<Models.v5.Subscriptions.Subscription> CheckChannelSubscriptionByUserAsync(string channelId, string userId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Check_Subscription, authToken);
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(userId))
                {
                    throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");
                }

                return Api.TwitchGetGenericAsync<Models.v5.Subscriptions.Subscription>($"/channels/{channelId}/subscriptions/{userId}", ApiVersion.v5, accessToken: authToken);
            }

            #endregion

            #region GetChannelVideos

            public Task<Models.v5.Channels.ChannelVideos> GetChannelVideosAsync(string channelId, int? limit = null, int? offset = null, List<string> broadcastType = null, List<string> language = null, string sort = null)
            {
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");
                }

                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
                if (broadcastType != null && broadcastType.Count > 0)
                {
                    var isCorrect = false;
                    foreach (var entry in broadcastType)
                    {
                        if (entry == "archive" || entry == "highlight" || entry == "upload")
                        {
                            isCorrect = true;
                        }
                        else
                        {
                            isCorrect = false;
                            break;
                        }
                    }

                    if (isCorrect)
                        getParams.Add(new KeyValuePair<string, string>("broadcast_type", string.Join(",", broadcastType)));
                }

                if (language != null && language.Count > 0)
                    getParams.Add(new KeyValuePair<string, string>("language", string.Join(",", language)));
                if (!string.IsNullOrWhiteSpace(sort) && (sort == "views" || sort == "time"))
                    getParams.Add(new KeyValuePair<string, string>("sort", sort));

                return Api.TwitchGetGenericAsync<Models.v5.Channels.ChannelVideos>($"/channels/{channelId}/videos", ApiVersion.v5, getParams);
            }

            #endregion

            #region StartChannelCommercial

            public Task<Models.v5.Channels.ChannelCommercial> StartChannelCommercialAsync(string channelId, CommercialLength duration, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Commercial, authToken);
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");
                }

                string payload = "{\"duration\": " + (int) duration + "}";
                return Api.TwitchPostGenericAsync<Models.v5.Channels.ChannelCommercial>($"/channels/{channelId}/commercial", ApiVersion.v5, payload, accessToken: authToken);
            }

            #endregion

            #region ResetChannelStreamKey

            /// <summary>
            /// <para>[ASYNC] Deletes the stream key for a specified channel. Once it is deleted, the stream key is automatically reset.</para>
            /// <para>A stream key (also known as authorization key) uniquely identifies a stream. Each broadcast uses an RTMP URL that includes the stream key. Stream keys are assigned by Twitch.</para>
            /// <para>Required Authentication Scope: channel_stream</para>
            /// </summary>
            /// <param name="channelId">The specified channel to reset the StreamKey on.</param>
            /// <param name="authToken"></param>
            /// <returns>A ChannelPrivileged object that also contains the email and stream key of the channel aside from the normal channel values.</returns>
            public Task<Models.v5.Channels.ChannelAuthed> ResetChannelStreamKeyAsync(string channelId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Stream, authToken);
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");
                }

                return Api.TwitchDeleteGenericAsync<Models.v5.Channels.ChannelAuthed>($"/channels/{channelId}/stream_key", ApiVersion.v5, authToken);
            }

            #endregion

            #region Communities

            #region GetChannelCommunity

            /// <summary>
            /// <para>[ASYNC] Gets the community for a specified channel.</para>
            /// <para>Required Authentication Scope: channel_editor</para>
            /// </summary>
            /// <param name="channelId">The specified channel ID to get the community from.</param>
            /// <param name="authToken"></param>
            /// <returns>A Community object that represents the community the channel is in.</returns>
            public Task<Models.v5.Communities.Community> GetChannelCommunityAsync(string channelId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");
                }

                return Api.TwitchGetGenericAsync<Models.v5.Communities.Community>($"/channels/{channelId}/community", ApiVersion.v5, accessToken: authToken);
            }

            #endregion

            #region GetChannelCommunities

            public Task<Models.v5.Communities.CommunitiesResponse> GetChannelCommunitiesAsync(string channelId, string authToken = null)
            {
                if (string.IsNullOrEmpty(channelId))
                {
                    throw new BadParameterException("The channel id is not valid. It is now allowed to be null, empty or filled with whitespaces.");
                }

                return Api.TwitchGetGenericAsync<Models.v5.Communities.CommunitiesResponse>($"/channels/{channelId}/communities", ApiVersion.v5, accessToken: authToken);
            }

            #endregion

            #region SetChannelCommunities

            /// <summary>
            /// <para>[ASYNC]Sets a specified channel to be in a specified communities.</para>
            /// <para>Required Authentication Scope: channel_editor</para>
            /// </summary>
            /// <param name="channelId">The specified channel to set the community for.</param>
            /// <param name="communityIds">The specified communities to set the channel to be a part of.</param>
            /// <param name="authToken"></param>
            public Task SetChannelCommunitiesAsync(string channelId, List<string> communityIds, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Editor, authToken);
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (communityIds == null || communityIds.Count == 0)
                {
                    throw new BadParameterException("The no community ids where specified");
                }

                if (communityIds != null && communityIds.Count > 3)
                {
                    throw new BadParameterException("You can only set up to 3 communities");
                }

                if (communityIds.Any(string.IsNullOrWhiteSpace))
                {
                    throw new BadParameterException("One or more of the community ids is not valid. It is not allowed to be null, empty or filled with whitespaces.");
                }

                string payload = "";
                foreach (var communityId in communityIds)
                {
                    if (payload == "")
                        payload = $"\"{communityId}\"";
                    else
                        payload += $", \"{communityId}\"";
                }

                payload = $"{{\"community_ids\": [{payload}]}}";
                return Api.TwitchPutAsync($"/channels/{channelId}/communities", ApiVersion.v5, payload, accessToken: authToken);
            }

            #endregion

            #region DeleteChannelFromCommunity

            /// <summary>
            /// [ASYNC] Deletes a specified channel from its community.
            /// </summary>
            /// <param name="channelId">The specified channel to be removed.</param>
            /// <param name="authToken"></param>
            public Task DeleteChannelFromCommunityAsync(string channelId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");
                }

                return Api.TwitchDeleteAsync($"/channels/{channelId}/community", ApiVersion.v5, accessToken: authToken);
            }

            #endregion

            #endregion
        }
    }
}