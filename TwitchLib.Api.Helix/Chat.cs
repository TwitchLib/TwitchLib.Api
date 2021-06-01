﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Chat.GetChannelChatBadges;
using TwitchLib.Api.Helix.Models.Chat.GetGlobalChatBadges;

namespace TwitchLib.Api.Helix
{
    public class Chat : ApiBase
    {
        public Chat(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        { }

        public Task<GetChannelChatBadgesResponse> GetChannelChatBadges(string broadcasterId, string authToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };
            return TwitchGetGenericAsync<GetChannelChatBadgesResponse>("/chat/badges", ApiVersion.Helix, getParams, authToken);
        }

        public Task<GetGlobalChatBadgesResponse> GetGlobalChatBadges(string authToken = null)
        {
            return TwitchGetGenericAsync<GetGlobalChatBadgesResponse>("/chat/badges/global", ApiVersion.Helix, accessToken: authToken);
        }
    }
}
