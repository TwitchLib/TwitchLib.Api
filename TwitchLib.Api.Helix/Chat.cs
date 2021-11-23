using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Chat.Badges.GetChannelChatBadges;
using TwitchLib.Api.Helix.Models.Chat.Badges.GetGlobalChatBadges;
using TwitchLib.Api.Helix.Models.Chat.ChatSettings;
using TwitchLib.Api.Helix.Models.Chat.Emotes.GetChannelEmotes;
using TwitchLib.Api.Helix.Models.Chat.Emotes.GetEmoteSets;
using TwitchLib.Api.Helix.Models.Chat.Emotes.GetGlobalEmotes;

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

            var getParams = new List<KeyValuePair<string, string>>()
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

            var getParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId)
            };

            return TwitchPatchGenericAsync<UpdateChatSettingsResponse>("/chat/settings", ApiVersion.Helix, JsonConvert.SerializeObject(settings), getParams, accessToken);
        }

        #endregion
    }
}
