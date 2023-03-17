using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Moderation.AutomodSettings;
using TwitchLib.Api.Helix.Models.Moderation.BanUser;
using TwitchLib.Api.Helix.Models.Moderation.BlockedTerms;
using TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus;
using TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus.Request;
using TwitchLib.Api.Helix.Models.Moderation.GetBannedEvents;
using TwitchLib.Api.Helix.Models.Moderation.GetBannedUsers;
using TwitchLib.Api.Helix.Models.Moderation.GetModeratorEvents;
using TwitchLib.Api.Helix.Models.Moderation.GetModerators;
using TwitchLib.Api.Helix.Models.Moderation.ShieldModeStatus;
using TwitchLib.Api.Helix.Models.Moderation.ShieldModeStatus.GetShieldModeStatus;
using TwitchLib.Api.Helix.Models.Moderation.ShieldModeStatus.UpdateShieldModeStatus;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Moderation related APIs
    /// </summary>
    public class Moderation : ApiBase
    {
        public Moderation(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }
        #region ManageHeldAutoModMessage
        /// <summary>
        /// Allow or deny a message that was held for review by AutoMod.
        /// <para>Required Scope: moderator:manage:automod</para>
        /// </summary>
        /// <param name="userId">The moderator who is approving or rejecting the held message. Must match the user_id in the user OAuth token.</param>
        /// <param name="msgId">ID of the message to be allowed or denied.</param>
        /// <param name="action">
        /// The action to take for the message.
        /// <para>Must be "ALLOW" or "DENY".</para>
        /// </param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task ManageHeldAutoModMessagesAsync(string userId, string msgId, ManageHeldAutoModMessageActionEnum action, string accessToken = null)
        {
            if(string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(msgId))
                throw new BadParameterException("userId and msgId cannot be null and must be greater than 0 length");

            var json = new JObject
            {
                ["user_id"] = userId,
                ["msg_id"] = msgId,
                ["action"] = action.ToString().ToUpper()
            };

            return TwitchPostAsync("/moderation/automod/message", ApiVersion.Helix, json.ToString(), accessToken: accessToken);
        }
        #endregion

        #region CheckAutoModeStatus
        /// <summary>
        /// Determines whether a string message meets the channel’s AutoMod requirements.
        /// <para>Rate Limits: Rates are limited per channel based on the account type rather than per access token.</para>
        /// <para>Normal: 5 per Minute / 50 per hour</para>
        /// <para>Affiliate: 10 per Minute / 100 per hour</para>
        /// <para>Partner: 30 per Minute / 300 per hour</para>
        /// <para>Required Scope: moderation:read</para>
        /// </summary>
        /// <param name="messages" cref="Message">List of messages to check</param>
        /// <param name="broadcasterId">BroadcasterId to test against. Provided broadcasterId must match the userId in the auth token.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="CheckAutoModStatusResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<CheckAutoModStatusResponse> CheckAutoModStatusAsync(List<Message> messages, string broadcasterId, string accessToken = null)
        {
            if (messages == null || messages.Count == 0)
                throw new BadParameterException("messages cannot be null and must be greater than 0 length");

            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null/empty/whitespace");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            var request = new MessageRequest
            {
                Messages = messages.ToArray()
            };

            return TwitchPostGenericAsync<CheckAutoModStatusResponse>("/moderation/enforcements/status", ApiVersion.Helix, JsonConvert.SerializeObject(request), getParams, accessToken);
        }

        #endregion

        #region GetBannedEvents
        public Task<GetBannedEventsResponse> GetBannedEventsAsync(string broadcasterId, List<string> userIds = null, string after = null, int first = 20, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null/empty/whitespace");

            if (first < 1 || first > 100)
                throw new BadParameterException("first cannot be less than 1 or greater than 100");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            if (userIds != null && userIds.Count > 0) 
                getParams.AddRange(userIds.Select(userId => new KeyValuePair<string, string>("user_id", userId)));

            if (string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            getParams.Add(new KeyValuePair<string, string>("first", first.ToString()));

            return TwitchGetGenericAsync<GetBannedEventsResponse>("/moderation/banned/events", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region GetBannedUsers
        /// <summary>
        /// Returns all banned and timed-out users for a channel.
        /// <para>Required scope: moderation:read</para>
        /// </summary>
        /// <param name="broadcasterId">BroadcasterId to get banned users for. Provided broadcaster_id must match the user_id in the OAuth token.</param>
        /// <param name="userIds">Filters the results and only returns a status object for users who are banned in the channel and have a matching user_id.</param>
        /// <param name="first">Maximum number of objects to return. 1 - 100. Default 1</param>
        /// <param name="after">Cursor for forward pagination.</param>
        /// <param name="before">Cursor for backward pagination.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetBannedUsersResponse"></returns>
        public Task<GetBannedUsersResponse> GetBannedUsersAsync(string broadcasterId, List<string> userIds = null, int first = 20, string after = null, string before = null, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null/empty/whitespace");

            if (first < 1 || first > 100)
                throw new BadParameterException("first cannot be less than 1 or greater than 100");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if (userIds != null && userIds.Count > 0) 
                getParams.AddRange(userIds.Select(userId => new KeyValuePair<string, string>("user_id", userId)));

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            if (!string.IsNullOrWhiteSpace(before))
                getParams.Add(new KeyValuePair<string, string>("before", before));

            return TwitchGetGenericAsync<GetBannedUsersResponse>("/moderation/banned", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region GetModerators
        /// <summary>
        /// Returns all moderators in a channel.
        /// <para>Note: This endpoint does not return the broadcaster in the response, as broadcasters are channel owners and have all permissions of moderators implicitly.</para>
        /// <para>Requires a user access token that includes the moderation:read scope.</para>
        /// <para>The ID in the broadcaster_id query parameter must match the user ID in the access token.</para>
        /// </summary>
        /// <param name="broadcasterId">Broadcaster to get the moderators for</param>
        /// <param name="userIds">
        /// Filters the results and only returns a status object for users who are moderators in this channel and have a matching user_id.
        /// <para>Maximum: 100</para>
        /// </param>
        /// <param name="first">Maximum number of objects to return. Maximum: 100. Default: 20.</param>
        /// <param name="after">Cursor for forward pagination: tells the server where to start fetching the next set of results in a multi-page response.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetModeratorsResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetModeratorsResponse> GetModeratorsAsync(string broadcasterId, List<string> userIds = null, int first = 20, string after = null, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null/empty/whitespace");
            if (first > 100 || first < 1)
                throw new BadParameterException("first must be greater than 0 and less than 101");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if (userIds != null && userIds.Count > 0) 
                getParams.AddRange(userIds.Select(userId => new KeyValuePair<string, string>("user_id", userId)));

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetModeratorsResponse>("/moderation/moderators", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region GetModeratorEvents

        public Task<GetModeratorEventsResponse> GetModeratorEventsAsync(string broadcasterId, List<string> userIds = null, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null/empty/whitespace");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            if (userIds != null && userIds.Count > 0) 
                getParams.AddRange(userIds.Select(userId => new KeyValuePair<string, string>("user_id", userId)));

            return TwitchGetGenericAsync<GetModeratorEventsResponse>("/moderation/moderators/events", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region BanUsers
        /// <summary>
        /// Ban or Timeout an user from chat. If a duration is specified it is treated as a timeout, if you omit a duration is a permanent ban.
        /// <para>Requires a User access token with scope set to moderator:manage:banned_users.</para>
        /// </summary>
        /// <param name="broadcasterId">Id of the broadcaster channel from which you want to ban/timeout somebody</param>
        /// <param name="moderatorId">Id of the moderator that wants to ban/timeout somebody (if you use the broadcaster account this has to be the broadcasterId)</param>
        /// <param name="banUserRequest">request object containing the information about the ban like the userId of the user to ban, the reason and optional duration</param>
        /// <param name="accessToken">optional access token to override the one used while creating the TwitchAPI object</param>
        /// <returns cref="BanUserResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<BanUserResponse> BanUserAsync(string broadcasterId, string moderatorId, BanUserRequest banUserRequest, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId must be set");

            if (banUserRequest == null)
                throw new BadParameterException("banUserRequest cannot be null");

            if (string.IsNullOrWhiteSpace(banUserRequest.UserId))
                throw new BadParameterException("banUserRequest.UserId must be set");

            if (banUserRequest.Duration.HasValue)
                if(banUserRequest.Duration.Value <= 0 || banUserRequest.Duration.Value > 1209600)
                    throw new BadParameterException("banUserRequest.Duration has to be between including 1 and including 1209600");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId)
            };

            var body = new
            {
                data = banUserRequest
            };

            return TwitchPostGenericAsync<BanUserResponse>("/moderation/bans", ApiVersion.Helix, JsonConvert.SerializeObject(body), getParams, accessToken);
        }

        #endregion

        #region UnbanUsers
        /// <summary>
        /// Removes the ban or timeout that was placed on the specified user
        /// <para>Requires a User access token with scope set to moderator:manage:banned_users.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster whose chat room the user is banned from chatting in.</param>
        /// <param name="moderatorId">
        /// The ID of a user that has permission to moderate the broadcaster’s chat room.
        /// <para>This ID must match the user ID associated with the user OAuth token.</para>
        /// </param>
        /// <param name="userId">The ID of the user to remove the ban or timeout from.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task UnbanUserAsync(string broadcasterId, string moderatorId, string userId, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            if (string.IsNullOrWhiteSpace(moderatorId))
                throw new BadParameterException("moderatorId must be set");

            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("userId must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
                new KeyValuePair<string, string>("user_id", userId)
            };

            return TwitchDeleteAsync("/moderation/bans", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region GetAutomodSettings
        /// <summary>
        /// Gets the broadcaster’s AutoMod settings, which are used to automatically block inappropriate or harassing messages from appearing in the broadcaster’s chat room.
        /// <para>Requires a User access token with scope set to moderator:read:automod_settings.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster whose AutoMod settings you want to get.</param>
        /// <param name="moderatorId">
        /// The ID of a user that has permission to moderate the broadcaster’s chat room.
        /// <para>This ID must match the user ID associated with the user OAuth token.</para>
        /// <para>If the broadcaster wants to get their own AutoMod settings (instead of having the moderator do it), set this parameter to the broadcaster’s ID, too.</para>
        /// </param>
        /// <param name="accessToken">optional access token to override the one used while creating the TwitchAPI object</param>
        /// <returns cref="GetAutomodSettingsResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetAutomodSettingsResponse> GetAutomodSettingsAsync(string broadcasterId, string moderatorId, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            if (string.IsNullOrWhiteSpace(moderatorId))
                throw new BadParameterException("moderatorId must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
            };

            return TwitchGetGenericAsync<GetAutomodSettingsResponse>("/moderation/automod/settings", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region UpdateAutomodSettings
        /// <summary>
        /// Updates the broadcaster’s AutoMod settings, which are used to automatically block inappropriate or harassing messages from appearing in the broadcaster’s chat room.
        /// <para>Requires a User access token with scope set to moderator:manage:automod_settings.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster whose AutoMod settings you want to update.</param>
        /// <param name="moderatorId">
        /// The ID of a user that has permission to moderate the broadcaster’s chat room.
        /// <para>This ID must match the user ID associated with the user OAuth token.</para>
        /// <para>If the broadcaster wants to update their own AutoMod settings (instead of having the moderator do it), set this parameter to the broadcaster’s ID, too.</para>
        /// </param>
        /// <param name="settings" cref="AutomodSettings"></param>
        /// <param name="accessToken">optional access token to override the one used while creating the TwitchAPI object</param>
        /// <returns cref="UpdateAutomodSettingsResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<UpdateAutomodSettingsResponse> UpdateAutomodSettingsAsync(string broadcasterId, string moderatorId, AutomodSettings settings, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            if (string.IsNullOrWhiteSpace(moderatorId))
                throw new BadParameterException("moderatorId must be set");

            // you can set the overall level, OR you can set individual levels, but not both

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
            };

            return TwitchPutGenericAsync<UpdateAutomodSettingsResponse>("/moderation/automod/settings", ApiVersion.Helix, JsonConvert.SerializeObject(settings), getParams, accessToken);
        }

        #endregion

        #region GetBlockedTerms
        /// <summary>
        /// Gets the broadcaster’s list of non-private, blocked words or phrases.
        /// <para>These are the terms that the broadcaster or moderator added manually, or that were denied by AutoMod.</para>
        /// <para>Requires a User access token with scope set to moderator:read:blocked_terms.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster whose blocked terms you’re getting.</param>
        /// <param name="moderatorId">
        /// The ID of a user that has permission to moderate the broadcaster’s chat room.
        /// <para>This ID must match the user ID associated with the user OAuth token.</para>
        /// <para>If the broadcaster wants to get their own block terms (instead of having the moderator do it), set this parameter to the broadcaster’s ID, too.</para>
        /// </param>
        /// <param name="after">The cursor used to get the next page of results. </param>
        /// <param name="first">
        /// The maximum number of blocked terms to return per page in the response.
        /// <para>The minimum page size is 1 blocked term per page and the maximum is 100. The default is 20.</para>
        /// </param>
        /// <param name="accessToken">optional access token to override the one used while creating the TwitchAPI object</param>
        /// <returns cref="GetBlockedTermsResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetBlockedTermsResponse> GetBlockedTermsAsync(string broadcasterId, string moderatorId, string after = null, int first = 20, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            if (string.IsNullOrWhiteSpace(moderatorId))
                throw new BadParameterException("moderatorId must be set");

            if (first < 1 || first > 100)
                throw new BadParameterException("first must be greater than 0 and less than 101");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetBlockedTermsResponse>("/moderation/blocked_terms", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region AddBlockedTerm
        /// <summary>
        /// Adds a word or phrase to the broadcaster’s list of blocked terms.
        /// <para>Requires a User access token with scope set to moderator:manage:blocked_terms.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster that owns the list of blocked terms.</param>
        /// <param name="moderatorId">
        /// The ID of a user that has permission to moderate the broadcaster’s chat room.
        /// <para>This ID must match the user ID associated with the user OAuth token.</para>
        /// <para>If the broadcaster wants to add the blocked term (instead of having the moderator do it), set this parameter to the broadcaster’s ID, too.</para>
        /// </param>
        /// <param name="term">
        /// The word or phrase to block from being used in the broadcaster’s chat room.
        /// <para>The term must contain a minimum of 2 characters and may contain up to a maximum of 500 characters.</para>
        /// <para>Terms can use a wildcard character (*).</para>
        /// <para>The wildcard character must appear at the beginning or end of a word, or set of characters. For example, *foo or foo*.</para>
        /// </param>
        /// <param name="accessToken">optional access token to override the one used while creating the TwitchAPI object</param>
        /// <returns cref="AddBlockedTermResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<AddBlockedTermResponse> AddBlockedTermAsync(string broadcasterId, string moderatorId, string term, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            if (string.IsNullOrWhiteSpace(moderatorId))
                throw new BadParameterException("moderatorId must be set");

            if (string.IsNullOrWhiteSpace(term))
                throw new BadParameterException("term must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
            };

            var body = new JObject
            {
                ["text"] = term
            };

            return TwitchPostGenericAsync<AddBlockedTermResponse>("/moderation/blocked_terms", ApiVersion.Helix, body.ToString(), getParams, accessToken);
        }

        #endregion

        #region DeleteBlockedTerm
        /// <summary>
        /// Removes the word or phrase that the broadcaster is blocking users from using in their chat room.
        /// <para>Requires a User access token with scope set to moderator:manage:blocked_terms.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster that owns the list of blocked terms.</param>
        /// <param name="moderatorId">
        /// The ID of the broadcaster or a user that has permission to moderate the broadcaster’s chat room.
        /// <para>This ID must match the user ID associated with the user OAuth token.</para>
        /// </param>
        /// <param name="termId">The ID of the blocked term you want to delete.</param>
        /// <param name="accessToken">optional access token to override the one used while creating the TwitchAPI object</param>
        /// <returns></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task DeleteBlockedTermAsync(string broadcasterId, string moderatorId, string termId, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            if (string.IsNullOrWhiteSpace(moderatorId))
                throw new BadParameterException("moderatorId must be set");

            if (string.IsNullOrWhiteSpace(termId))
                throw new BadParameterException("termId must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
                new KeyValuePair<string, string>("id", termId)
            };

            return TwitchDeleteAsync("/moderation/blocked_terms", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region Delete Chat Messages

        /// <summary>
        /// Removes a single chat message or all chat messages from the broadcaster’s chat room.
        /// <para>!!! If no messageId is specified, the request removes all messages in the broadcaster’s chat room. !!!</para>
        /// <para>The message must have been created within the last 6 hours.</para>
        /// <para>The message must not belong to the broadcaster.</para>
        /// <para>The message must not belong to another moderator.</para>
        /// <para>Requires a user access token that includes the moderator:manage:chat_messages scope. </para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster that owns the chat room to remove messages from.</param>
        /// <param name="moderatorId">The ID of a user that has permission to moderate the broadcaster’s chat room. This ID must match the user ID in the OAuth token.</param>
        /// <param name="messageId">
        /// The ID of the message to remove.
        /// <para>!!! If not specified, the request removes all messages in the broadcaster’s chat room. !!!</para>
        /// </param>
        /// <param name="accessToken">optional access token to override the one used while creating the TwitchAPI object</param>
        /// <returns></returns>
        public Task DeleteChatMessagesAsync(string broadcasterId, string moderatorId, string messageId = null, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            if (string.IsNullOrWhiteSpace(moderatorId))
                throw new BadParameterException("moderatorId must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
            };

            if (!string.IsNullOrWhiteSpace(messageId))
            {
                getParams.Add(new KeyValuePair<string, string>("message_id", messageId));
            }

            return TwitchDeleteAsync("/moderation/chat", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region AddChannelModerator

        /// <summary>
        /// Adds a moderator to the broadcaster’s chat room.
        /// Rate Limits: The channel may add a maximum of 10 moderators within a 10 seconds period.
        /// Requires a user access token that includes the channel:manage:moderators scope.
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster that owns the chat room.</param>
        /// <param name="userId">The ID of the user to add as a moderator in the broadcaster’s chat room.</param>
        /// <param name="accessToken">optional access token to override the one used while creating the TwitchAPI object</param>
        /// <returns></returns>
        public Task AddChannelModeratorAsync(string broadcasterId, string userId, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("userId must be set");

            var getParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("user_id", userId),
            };

            return TwitchPostAsync("/moderation/moderators", ApiVersion.Helix, null, getParams, accessToken);
        }

        #endregion

        #region DeleteChannelModerator

        /// <summary>
        /// Removes a moderator from the broadcaster’s chat room.
        /// Rate Limits: The channel may remove a maximum of 10 moderators within a 10 seconds period.
        /// Requires a user access token that includes the channel:manage:moderators scope.
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster that owns the chat room.</param>
        /// <param name="userId">The ID of the user to remove as a moderator from the broadcaster’s chat room.</param>
        /// <param name="accessToken">optional access token to override the one used while creating the TwitchAPI object</param>
        /// <returns></returns>
        public Task DeleteChannelModeratorAsync(string broadcasterId, string userId, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("userId must be set");

            var getParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("user_id", userId),
            };

            return TwitchDeleteAsync("/moderation/moderators", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region ShieldModeStatus

        #region GetShieldModeStatus

        /// <summary>
        /// Gets the broadcaster’s Shield Mode activation status.
        /// Requires a user access token that includes the moderator:read:shield_mode or moderator:manage:shield_mode scope.
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster whose Shield Mode activation status you want to get.</param>
        /// <param name="moderatorId">The ID of the broadcaster or a user that is one of the broadcaster’s moderators. This ID must match the user ID in the access token.</param>
        /// <param name="accessToken">optional access token to override the one used while creating the TwitchAPI object</param>
        /// <returns cref="GetShieldModeStatusResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetShieldModeStatusResponse> GetShieldModeStatusAsync(string broadcasterId, string moderatorId, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            if (string.IsNullOrWhiteSpace(moderatorId))
                throw new BadParameterException("moderatorId must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId)
            };

            return TwitchGetGenericAsync<GetShieldModeStatusResponse>("/moderation/shield_mode", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region UpdateShieldModeStatus

        /// <summary>
        /// Activates or deactivates the broadcaster’s Shield Mode.
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster whose Shield Mode activation status you want to get.</param>
        /// <param name="moderatorId">The ID of the broadcaster or a user that is one of the broadcaster’s moderators. This ID must match the user ID in the access token.</param>
        /// <param name="request">ShieldModeStatusRequest Model to request</param>
        /// <param name="accessToken">optional access token to override the one used while creating the TwitchAPI object</param>
        /// <returns cref="ShieldModeStatus"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<ShieldModeStatus> UpdateShieldModeStatusAsync(string broadcasterId, string moderatorId, ShieldModeStatusRequest request, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            if (string.IsNullOrWhiteSpace(moderatorId))
                throw new BadParameterException("moderatorId must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId)
            };

            return TwitchPutGenericAsync<ShieldModeStatus>("/moderation/shield_mode", ApiVersion.Helix, JsonConvert.SerializeObject(request), getParams, accessToken);
        }

        #endregion

        #endregion
    }
}
