using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Models.V5.Badges;

namespace TwitchLib.Api.Sections
{
    public class Badges
    {
        public Badges(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
        {
            V5 = new V5Api(settings, rateLimiter, http);
        }

        public V5Api V5 { get; }

        public class V5Api : ApiBase
        {
            public V5Api(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
            {
            }

            #region GetSubscriberBadgesForChannel

            public Task<ChannelDisplayBadges> GetSubscriberBadgesForChannelAsync(string channelId)
            {
                if (string.IsNullOrWhiteSpace(channelId)) throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                return TwitchGetGenericAsync<ChannelDisplayBadges>($"/v1/badges/channels/{channelId}/display", ApiVersion.v5, customBase: "https://badges.twitch.tv");
            }

            #endregion

            #region GetGlobalBadges

            public Task<GlobalBadgesResponse> GetGlobalBadgesAsync()
            {
                return TwitchGetGenericAsync<GlobalBadgesResponse>("/v1/badges/global/display", ApiVersion.v5, customBase: "https://badges.twitch.tv");
            }

            #endregion
        }
    }
}