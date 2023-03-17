using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
    /// <summary>
    /// Chat related APIs
    /// </summary>
    public class Chat : ApiBase
    {
        public Chat(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        { }

        #region Badges
        /// <summary>
        /// Gets a list of custom chat badges that can be used in chat for the specified channel. This includes subscriber badges and Bit badges.
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster whose chat badges you want to get.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetChannelChatBadgesResponse"></returns>
        public Task<GetChannelChatBadgesResponse> GetChannelChatBadgesAsync(string broadcasterId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetGenericAsync<GetChannelChatBadgesResponse>("/chat/badges", ApiVersion.Helix, getParams, accessToken);
        }

        /// <summary>
        /// Gets a list of chat badges that can be used in chat for any channel.
        /// </summary>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetGlobalChatBadgesResponse"></returns>
        public Task<GetGlobalChatBadgesResponse> GetGlobalChatBadgesAsync(string accessToken = null)
        {
            return TwitchGetGenericAsync<GetGlobalChatBadgesResponse>("/chat/badges/global", ApiVersion.Helix, accessToken: accessToken);
        }
        #endregion

        #region Chatters
        /// <summary>
        /// Gets the list of users that are connected to the specified broadcaster’s chat session.
        /// <para>Note that there is a delay between when users join and leave a chat and when the list is updated accordingly.</para>
        /// <para>Requires a user access token that includes the moderator:read:chatters scope.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster whose list of chatters you want to get.</param>
        /// <param name="moderatorId">
        /// The ID of the moderator or the specified broadcaster that’s requesting the list of chatters.
        /// <para>This ID must match the user ID associated with the user access token.</para>
        /// </param>
        /// <param name="first">
        /// The maximum number of items to return per page in the response.
        /// <para>The minimum page size is 1 item per page and the maximum is 1,000. The default is 100.</para>
        /// </param>
        /// <param name="after">The cursor used to get the next page of results. The Pagination object in the response contains the cursor’s value. </param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetChattersResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
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
        /// <summary>
        /// Gets all emotes that the specified Twitch channel created. 
        /// </summary>
        /// <param name="broadcasterId">ID of the broadcaster to get the emotes from.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetChannelEmotesResponse"></returns>
        public Task<GetChannelEmotesResponse> GetChannelEmotesAsync(string broadcasterId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetGenericAsync<GetChannelEmotesResponse>("/chat/emotes", ApiVersion.Helix, getParams, accessToken);
        }

        /// <summary>
        /// Gets emotes for one or more specified emote sets.
        /// <para>An emote set groups emotes that have a similar context.</para>
        /// <para>For example, Twitch places all the subscriber emotes that a broadcaster uploads for their channel in the same emote set.</para>
        /// </summary>
        /// <param name="emoteSetIds">
        /// An ID that identifies the emote set
        /// <para>You may specify a maximum of 25 IDs.</para>
        /// </param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetEmoteSetsResponse"></returns>
        public Task<GetEmoteSetsResponse> GetEmoteSetsAsync(List<string> emoteSetIds, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();

            getParams.AddRange(emoteSetIds.Select(emoteSetId => new KeyValuePair<string, string>("emote_set_id", emoteSetId)));

            return TwitchGetGenericAsync<GetEmoteSetsResponse>("/chat/emotes/set", ApiVersion.Helix, getParams, accessToken);
        }

        /// <summary>
        /// Gets all global emotes. Global emotes are Twitch-created emoticons that users can use in any Twitch chat.
        /// </summary>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetGlobalEmotesResponse"></returns>
        public Task<GetGlobalEmotesResponse> GetGlobalEmotesAsync(string accessToken = null)
        {
            return TwitchGetGenericAsync<GetGlobalEmotesResponse>("/chat/emotes/global", ApiVersion.Helix, accessToken: accessToken);
        }
        #endregion

        #region GetChatSettings
        /// <summary>
        /// Gets the broadcaster’s chat settings.
        /// <para>To include the non_moderator_chat_delay or non_moderator_chat_delay_duration settings in the response, you must specify a User access token with scope set to moderator:read:chat_settings.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster whose chat settings you want to get.</param>
        /// <param name="moderatorId">
        /// Required only to access the non_moderator_chat_delay or non_moderator_chat_delay_duration settings.
        /// <para>The ID of a user that has permission to moderate the broadcaster’s chat room. This ID must match the user ID associated with the user OAuth token.</para>
        /// <para>If the broadcaster wants to get their own settings (instead of having the moderator do it), set this parameter to the broadcaster’s ID, too.</para>
        /// </param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetChatSettingsResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
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
        /// <summary>
        /// Updates the broadcaster’s chat settings.
        /// <para>Requires a User access token with scope set to moderator:manage:chat_settings.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster whose chat settings you want to update.</param>
        /// <param name="moderatorId">
        /// The ID of a user that has permission to moderate the broadcaster’s chat room.
        /// <para>This ID must match the user ID associated with the user OAuth token.</para>
        /// <para>If the broadcaster is making the update, also set this parameter to the broadcaster’s ID.</para>
        /// </param>
        /// <param name="settings" cref="ChatSettings"></param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="UpdateChatSettingsResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
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
        /// Sends an announcement to the broadcaster’s chat room.
        /// Requires a user access token that includes the moderator:manage:announcements scope.
        /// The ID in the moderator_id query parameter must match the user ID in the access token.
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster that owns the chat room to send the announcement to.</param>
        /// <param name="moderatorId">The ID of a user who has permission to moderate the broadcaster’s chat room. This ID must match the user ID in the OAuth token, which can be a moderator or the broadcaster.</param>
        /// <param name="message">The announcement to make in the broadcaster’s chat room.</param>
        /// <param name="color">The color used to highlight the announcement. Possible case-sensitive values are: blue/green/orange/purple/primary(default)</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
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

        #region Shoutouts

        /// <summary>
        /// Sends a Shoutout to the specified broadcaster.
        /// </summary>
        /// <param name="fromBroadcasterId">The ID of the broadcaster that’s sending the Shoutout.</param>
        /// <param name="toBroadcasterId"> 	The ID of the broadcaster that’s receiving the Shoutout.</param>
        /// <param name="moderatorId">The ID of the broadcaster or a user that is one of the broadcaster’s moderators. This ID must match the user ID in the access token.</param>
        /// <param name="accessToken"></param>
        /// <exception cref="BadParameterException"></exception>
        public Task SendShoutoutAsync(string fromBroadcasterId, string toBroadcasterId, string moderatorId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(fromBroadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            if (string.IsNullOrEmpty(toBroadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("from_broadcaster_id", fromBroadcasterId),
                new KeyValuePair<string, string>("to_broadcaster_id", toBroadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
            };

            return TwitchPostAsync("/chat/shoutouts", ApiVersion.Helix, null, getParams, accessToken);
        }

        #endregion

        #region Update User Chat Color
        /// <summary>
        /// Updates the color used for the user’s name in chat from a selection of available colors.
        /// </summary>
        /// <param name="userId">The ID of the user whose chat color you want to update.</param>
        /// <param name="color">The color to use for the user’s name in chat from UserColors selection.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
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

            return TwitchPutAsync("/chat/color", ApiVersion.Helix, null, getParams, accessToken);
        }

        /// <summary>
        /// Updates the color used for the user’s name in chat from a HEX Code.
        /// <para>Turbo or Prime Required</para>
        /// </summary>
        /// <param name="userId">The ID of the user whose chat color you want to update.</param>
        /// <param name="colorHex">The color to use for the user’s name in chat in hex format.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
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

            return TwitchPutAsync("/chat/color", ApiVersion.Helix, null, getParams, accessToken);
        }

        #endregion

        #region Get User Chat Color
        /// <summary>
        /// Gets the color used for the user(s)’s name in chat.
        /// </summary>
        /// <param name="userIds">The ID of the users whose color you want to get.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetUserChatColorResponse"></returns>
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
