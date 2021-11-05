using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Moderation.AutomodSettings;
using TwitchLib.Api.Helix.Models.Moderation.BanAndTimeoutUsers;
using TwitchLib.Api.Helix.Models.Moderation.BanUsers;
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

        public Task ManageHeldAutoModMessages(string userId, string msgId, ManageHeldAutoModMessageActionEnum action, string accessToken = null)
        {
            if(String.IsNullOrEmpty(userId) || String.IsNullOrEmpty(msgId))
                throw new BadParameterException("userId and msgId cannot be null and must be greater than 0 length");

            JObject json = new JObject();
            json["user_id"] = userId;
            json["msg_id"] = msgId;
            json["action"] = action.ToString().ToUpper();

            return TwitchPostAsync("/moderation/automod/message", ApiVersion.Helix, json.ToString(), accessToken: accessToken);
        }

        #region CheckAutoModeStatus

        public Task<CheckAutoModStatusResponse> CheckAutoModStatusAsync(List<Message> messages, string broadcasterId, string accessToken = null)
        {
            if (messages == null || messages.Count == 0)
                throw new BadParameterException("messages cannot be null and must be greater than 0 length");

            if (broadcasterId == null || broadcasterId.Length == 0)
                throw new BadParameterException("broadcasterId cannot be null and must be greater than 0 length");

            var getParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("broacaster_id", broadcasterId)
            };

            MessageRequest request = new MessageRequest()
            {
                Messages = messages.ToArray()
            };

            return TwitchPostGenericAsync<CheckAutoModStatusResponse>("/moderation/enforcements/status", ApiVersion.Helix, JsonConvert.SerializeObject(request), getParams, accessToken);
        }

        #endregion

        #region GetBannedEvents

        public Task<GetBannedEventsResponse> GetBannedEventsAsync(string broadcasterId, List<string> userIds = null, string after = null, string first = null, string accessToken = null)
        {
            if (broadcasterId == null || broadcasterId.Length == 0)
                throw new BadParameterException("broadcasterId cannot be null and must be greater than 0 length");

            var getParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            if (userIds != null && userIds.Count > 0)
                foreach (var userId in userIds)
                    getParams.Add(new KeyValuePair<string, string>("user_id", userId));

            if (after != null)
                getParams.Add(new KeyValuePair<string, string>("after", after));

            if (first != null)
                getParams.Add(new KeyValuePair<string, string>("first", first));

            return TwitchGetGenericAsync<GetBannedEventsResponse>("/moderation/banned/events", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region GetBannedUsers

        public Task<GetBannedUsersResponse> GetBannedUsersAsync(string broadcasterId, List<string> userIds = null, string after = null, string before = null, string accessToken = null)
        {
            if (broadcasterId == null || broadcasterId.Length == 0)
                throw new BadParameterException("broadcasterId cannot be null and must be greater than 0 length");

            var getParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            if (userIds != null && userIds.Count > 0)
                foreach (var userId in userIds)
                    getParams.Add(new KeyValuePair<string, string>("user_id", userId));

            if (after != null)
                getParams.Add(new KeyValuePair<string, string>("after", after));

            if (before != null)
                getParams.Add(new KeyValuePair<string, string>("before", before));

            return TwitchGetGenericAsync<GetBannedUsersResponse>("/moderation/banned", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region GetModerators

        public Task<GetModeratorsResponse> GetModeratorsAsync(string broadcasterId, List<string> userIds = null, string after = null, string accessToken = null)
        {
            if (broadcasterId == null || broadcasterId.Length == 0)
                throw new BadParameterException("broadcasterId cannot be null and must be greater than 0 length");

            var getParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            if (userIds != null && userIds.Count > 0)
                foreach (var userId in userIds)
                    getParams.Add(new KeyValuePair<string, string>("user_id", userId));

            if (after != null)
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetModeratorsResponse>("/moderation/moderators", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region GetModeratorEvents

        public Task<GetModeratorEventsResponse> GetModeratorEventsAsync(string broadcasterId, List<string> userIds = null, string accessToken = null)
        {
            if (broadcasterId == null || broadcasterId.Length == 0)
                throw new BadParameterException("broadcasterId cannot be null and must be greater than 0 length");

            var getParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            if (userIds != null && userIds.Count > 0)
                foreach (var userId in userIds)
                    getParams.Add(new KeyValuePair<string, string>("user_id", userId));

            return TwitchGetGenericAsync<GetModeratorEventsResponse>("/moderation/moderators/events", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region BanUsers

        public Task<BanAndTimeoutUsersResponse> BanAndTimeoutUsersAsync(string broadcasterId, string moderatorId, List<TimeoutUser> usersToTimeout = null, List<BanUser> usersToBan = null, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");
            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId must be set");
            if ((usersToTimeout == null || usersToTimeout.Count == 0) && (usersToBan == null || usersToBan.Count == 0))
                throw new BadParameterException("at least usersToTimeout or usersToBan must be set");

            var bans = new JArray();
            if(usersToTimeout != null && usersToTimeout.Count > 0)
            {
                foreach(var user in usersToTimeout)
                {
                    if (string.IsNullOrEmpty(user.UserId))
                        throw new BadParameterException($"timeout target's userid must be set");
                    if (user.Reason == null)
                        throw new BadParameterException($"timeout target's reason must not be null");
                    JObject timeout = new JObject();
                    timeout["user_id"] = user.UserId;
                    timeout["reason"] = user.Reason;
                    timeout["duration"] = user.Duration.TotalSeconds;
                    bans.Add(timeout);
                }
            }
            if(usersToBan != null && usersToBan.Count > 0)
            {
                foreach(var user in usersToBan)
                {
                    if (string.IsNullOrEmpty(user.UserId))
                        throw new BadParameterException($"ban target's userid must be set");
                    if (user.Reason == null)
                        throw new BadParameterException($"ban target's reason must not be null");
                    JObject ban = new JObject();
                    ban["user_id"] = user.UserId;
                    ban["reason"] = user.Reason;
                    bans.Add(ban);
                }
            }
            JObject req = new JObject();
            req["data"] = bans;

            var getParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId)
            };

            return TwitchPostGenericAsync<BanAndTimeoutUsersResponse>("/moderation/bans", ApiVersion.Helix, req.ToString(), getParams, accessToken);
        }

        #endregion

        #region UnbanUsers

        public Task UnbanUserAsync(string broadcasterId, string moderatorId, string userId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");
            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId must be set");
            if (string.IsNullOrEmpty(userId))
                throw new BadParameterException("userId must be set");

            var getParams = new List<KeyValuePair<string, string>>()
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
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");
            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId must be set");

            var getParams = new List<KeyValuePair<string, string>>()
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
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");
            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId must be set");

            // you can set the overall level, OR you can set individual levels, but not both

            var getParams = new List<KeyValuePair<string, string>>()
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
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");
            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId must be set");
            if (first < 1 || first > 100)
                throw new BadParameterException("first must be greater than 0 and less than 101");

            var getParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if (!string.IsNullOrEmpty(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetBlockedTermsResponse>("/moderation/blocked_terms", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region AddBlockedTerm

        public Task<AddBlockedTermResponse> AddBlockedTermAsync(string broadcasterId, string moderatorId, string term, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");
            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId must be set");
            if (string.IsNullOrEmpty(term))
                throw new BadParameterException("term must be set");

            var getParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
            };

            JObject body = new JObject();
            body["text"] = term;

            return TwitchPostGenericAsync<AddBlockedTermResponse>("/moderation/blocked_terms", ApiVersion.Helix, body.ToString(), getParams, accessToken);
        }

        #endregion

        #region DeleteBlockedTerm

        public Task DeleteBlockedTermAsync(string broadcasterId, string moderatorId, string termId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");
            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId must be set");
            if (string.IsNullOrEmpty(termId))
                throw new BadParameterException("termId must be set");

            var getParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
                new KeyValuePair<string, string>("id", termId)
            };

            return TwitchDeleteAsync("/moderation/blocked_terms", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion
    }
}
