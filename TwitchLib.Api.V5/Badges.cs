using System;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.V5.Models.Badges;

namespace TwitchLib.Api.V5
{
    [Obsolete("This is a v5 class, please use a Helix class. All v5 calls will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
    public class Badges : ApiBase
    {
        public Badges(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetSubscriberBadgesForChannel

        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task<ChannelDisplayBadges> GetSubscriberBadgesForChannelAsync(string channelId)
        {
            if (string.IsNullOrWhiteSpace(channelId)) throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            return TwitchGetGenericAsync<ChannelDisplayBadges>($"/v1/badges/channels/{channelId}/display", ApiVersion.V5, customBase: "https://badges.twitch.tv");
        }

        #endregion

        #region GetGlobalBadges

        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task<GlobalBadgesResponse> GetGlobalBadgesAsync()
        {
            return TwitchGetGenericAsync<GlobalBadgesResponse>("/v1/badges/global/display", ApiVersion.V5, customBase: "https://badges.twitch.tv");
        }

        #endregion
    }

}