using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Chat.Badges.GetChannelChatBadges;
using TwitchLib.Api.Helix.Models.Chat.Badges.GetGlobalChatBadges;
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
    }
}
