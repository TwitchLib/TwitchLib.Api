using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Badges.GetChannelChatBadges
{
    public class GetChannelChatBadgesResponse
    {
        [JsonProperty(PropertyName = "data")]
        public BadgeEmoteSet[] EmoteSet { get; protected set; }
    }
}
