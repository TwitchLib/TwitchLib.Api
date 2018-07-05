using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;
using TwitchLib.Api.Models.v5.Badges;

namespace TwitchLib.Api.Sections
{
    public class Badges
    {
        public Badges(TwitchAPI api)
        {
            v5 = new V5Api(api);
        }

        public V5Api v5 { get; }

        public class V5Api : ApiSection
        {
            public V5Api(TwitchAPI api) : base(api)
            {
            }

            #region GetSubscriberBadgesForChannel

            public Task<ChannelDisplayBadges> GetSubscriberBadgesForChannelAsync(string channelId)
            {
                if (string.IsNullOrWhiteSpace(channelId)) throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                return Api.TwitchGetGenericAsync<ChannelDisplayBadges>($"/v1/badges/channels/{channelId}/display", ApiVersion.v5, customBase: "https://badges.twitch.tv");
            }

            #endregion

            #region GetGlobalBadges

            public Task<GlobalBadgesResponse> GetGlobalBadgesAsync()
            {
                return Api.TwitchGetGenericAsync<GlobalBadgesResponse>("/v1/badges/global/display", ApiVersion.v5, customBase: "https://badges.twitch.tv");
            }

            #endregion
        }
    }
}