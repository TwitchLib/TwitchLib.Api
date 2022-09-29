using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Chat;
using TwitchLib.Api.Helix.Models.Chat.Badges.GetChannelChatBadges;
using TwitchLib.Api.Helix.Models.Chat.Badges.GetGlobalChatBadges;
using TwitchLib.Api.Helix.Models.Chat.ChatSettings;
using TwitchLib.Api.Helix.Models.Chat.Emotes.GetChannelEmotes;
using TwitchLib.Api.Helix.Models.Chat.Emotes.GetEmoteSets;
using TwitchLib.Api.Helix.Models.Chat.Emotes.GetGlobalEmotes;
using TwitchLib.Api.Helix.Models.Chat.GetChatters;
using TwitchLib.Api.Helix.Models.Chat.GetUserChatColor;

namespace TwitchLib.Api.Helix
{
    public class Chat : ApiBase
    {
        public Chat(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        { }

        #region Badges
        public Task<GetChannelChatBadgesResponse> GetChannelChatBadgesAsync(string broadcasterId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetGenericAsync<GetChannelChatBadgesResponse>("/chat/badges", ApiVersion.Helix, getParams, accessToken);
        }

        public Task<GetGlobalChatBadgesResponse> GetGlobalChatBadgesAsync(string accessToken = null)
        {
            return TwitchGetGenericAsync<GetGlobalChatBadgesResponse>("/chat/badges/global", ApiVersion.Helix, accessToken: accessToken);
        }
        #endregion

        #region Chatters

        public Task<GetChattersResponse> GetChattersAsync(string broadcasterId, string moderatorId, int first = 100, string after = null, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null/empty/whitespace");

            if (string.IsNullOrWhiteSpace(moderatorId))
                throw new BadParameterException("moderatorId cannot be null/empty/whitespace");

            if (first < 1 || first > 1000)
                throw new BadParameterException("first cannot be less than 1 or greater than 1000");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
                new KeyValuePair<string, string>("first", first.ToString()),
            };

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetChattersResponse>("/chat/chatters", ApiVersion.Helix, getParams, accessToken: accessToken);
        }

        #endregion

        #region Emotes

        public Task<GetChannelEmotesResponse> GetChannelEmotesAsync(string broadcasterId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetGenericAsync<GetChannelEmotesResponse>("/chat/emotes", ApiVersion.Helix, getParams, accessToken);
        }

        public Task<GetEmoteSetsResponse> GetEmoteSetsAsync(string emoteSetId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("emote_set_id", emoteSetId)
            };

            return TwitchGetGenericAsync<GetEmoteSetsResponse>("/chat/emotes/set", ApiVersion.Helix, getParams, accessToken);
        }

        public Task<GetGlobalEmotesResponse> GetGlobalEmotesAsync(string accessToken = null)
        {
            return TwitchGetGenericAsync<GetGlobalEmotesResponse>("/chat/emotes/global", ApiVersion.Helix, accessToken: accessToken);
        }
        #endregion

        #region GetChatSettings

        public Task<GetChatSettingsResponse> GetChatSettingsAsync(string broadcasterId, string moderatorId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId)
            };

            return TwitchGetGenericAsync<GetChatSettingsResponse>("/chat/settings", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region UpdateChatSettings

        public Task<UpdateChatSettingsResponse> UpdateChatSettingsAsync(string broadcasterId, string moderatorId, ChatSettings settings, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId must be set");

            if (settings == null)
                throw new BadParameterException("settings must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId)
            };

            return TwitchPatchGenericAsync<UpdateChatSettingsResponse>("/chat/settings", ApiVersion.Helix, JsonConvert.SerializeObject(settings), getParams, accessToken);
        }

        #endregion

        #region Announcements

        /// <summary>
        /// BETA : Sends an announcement to the broadcaster’s chat room.
        /// Requires a user access token that includes the moderator:manage:announcements scope.
        /// The ID in the moderator_id query parameter must match the user ID in the access token.
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster that owns the chat room to send the announcement to.</param>
        /// <param name="moderatorId">The ID of a user who has permission to moderate the broadcaster’s chat room. This ID must match the user ID in the OAuth token, which can be a moderator or the broadcaster.</param>
        /// <param name="message">The announcement to make in the broadcaster’s chat room.</param>
        /// <param name="color">The color used to highlight the announcement. Possible case-sensitive values are: blue/green/orange/purple/primary(default)</param>
        /// <param name="accessToken"></param>
        public Task SendChatAnnouncementAsync(string broadcasterId, string moderatorId, string message, AnnouncementColors color = null, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId must be set");

            if (message == null)
                throw new BadParameterException("message must be set");

            if (message.Length > 500)
                throw new BadParameterException("message length must be less than or equal to 500 characters");

            if (color == null)
                color = AnnouncementColors.Primary;

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
            };

            // This should be updated to have a Request Class in the future.
            var json = new JObject
            {
                ["message"] = message,
                ["color"] = color.Value
            };

            return TwitchPostAsync("/chat/announcements", ApiVersion.Helix, json.ToString(), getParams, accessToken);
        }

        #endregion

        #region Update User Chat Color

        /// <summary>
        /// BETA - Updates the color used for the user’s name in chat from a selection of available colors.
        /// </summary>
        /// <param name="userId">The ID of the user whose chat color you want to update.</param>
        /// <param name="color">The color to use for the user’s name in chat from UserColors selection.</param>
        /// <param name="accessToken"></param>
        public Task UpdateUserChatColorAsync(string userId, UserColors color, string accessToken = null)
        {
            if (string.IsNullOrEmpty(userId))
                throw new BadParameterException("userId must be set");

            if (string.IsNullOrEmpty(color.Value))
                throw new BadParameterException("color must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("user_id", userId),
                new KeyValuePair<string, string>("color", color.Value),
            };

            return TwitchPostAsync("/chat/color", ApiVersion.Helix, null, getParams, accessToken);
        }

        /// <summary>
        /// BETA - Updates the color used for the user’s name in chat from a HEX Code.
        /// Turbo or Prime Required
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="colorHex"></param>
        /// <param name="accessToken"></param>
        public Task UpdateUserChatColorAsync(string userId, string colorHex, string accessToken = null)
        {
            if (string.IsNullOrEmpty(userId))
                throw new BadParameterException("userId must be set");

            if (string.IsNullOrEmpty(colorHex))
                throw new BadParameterException("colorHex must be set");

            if (colorHex.Length != 6)
                throw new BadParameterException("colorHex length must be equal to 6 characters \"######\"");

            var colorEncoded = HttpUtility.UrlEncode("#" + colorHex);

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("user_id", userId),
                new KeyValuePair<string, string>("color", colorEncoded),
            };

            return TwitchPostAsync("/chat/color", ApiVersion.Helix, null, getParams, accessToken);
        }

        #endregion

        #region Get User Chat Color

        /// <summary>
        /// BETA - Gets the color used for the user(s)’s name in chat.
        /// </summary>
        /// <param name="userIds">The ID of the users whose color you want to get.</param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public Task<GetUserChatColorResponse> GetUserChatColorAsync(List<string> userIds, string accessToken = null)
        {
            if (userIds.Count == 0)
                throw new BadParameterException("userIds must contain at least 1 userId");

            var getParams = new List<KeyValuePair<string, string>>();

            foreach (var userId in userIds)
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new BadParameterException("userId must be set");
                }

                getParams.Add(new KeyValuePair<string, string>("user_id", userId));
            }

            return TwitchGetGenericAsync<GetUserChatColorResponse>("/chat/color", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion
    }
}
