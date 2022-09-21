using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace TwitchLib.Api.Helix
{
    public class Moderation : ApiBase
    {
        public Moderation(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

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

        #region CheckAutoModeStatus

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
        /// </summary>
        /// <param name="broadcasterId">Provided broadcaster_id must match the user_id in the OAuth token.</param>
        /// <param name="userIds">Filters the results and only returns a status object for users who are banned in the channel and have a matching user_id.</param>
        /// <param name="first">Maximum number of objects to return. 1 - 100. Default 1</param>
        /// <param name="after">Cursor for forward pagination.</param>
        /// <param name="before">Cursor for backward pagination.</param>
        /// <param name="accessToken">Access Token</param>
        /// <returns></returns>
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

            if (banUserRequest.Reason == null)
                throw new BadParameterException("banUserRequest.Reason cannot be null and must be set to at least an empty string");

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
        /// BETA - Removes a single chat message or all chat messages from the broadcaster’s chat room.
        /// The message must have been created within the last 6 hours.
        /// The message must not belong to the broadcaster.
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster that owns the chat room to remove messages from.</param>
        /// <param name="moderatorId">The ID of a user that has permission to moderate the broadcaster’s chat room. This ID must match the user ID in the OAuth token.</param>
        /// <param name="messageId">The ID of the message to remove. If not specified, the request removes all messages in the broadcaster’s chat room.</param>
        /// <param name="accessToken"></param>
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
        /// BETA - Adds a moderator to the broadcaster’s chat room.
        /// Rate Limits: The channel may add a maximum of 10 moderators within a 10 seconds period.
        /// Requires a user access token that includes the channel:manage:moderators scope.
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster that owns the chat room.</param>
        /// <param name="userId">The ID of the user to add as a moderator in the broadcaster’s chat room.</param>
        /// <param name="accessToken"></param>
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
        /// BETA - Removes a moderator from the broadcaster’s chat room.
        /// Rate Limits: The channel may remove a maximum of 10 moderators within a 10 seconds period.
        /// Requires a user access token that includes the channel:manage:moderators scope.
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster that owns the chat room.</param>
        /// <param name="userId">The ID of the user to remove as a moderator from the broadcaster’s chat room.</param>
        /// <param name="accessToken"></param>
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
    }
}
