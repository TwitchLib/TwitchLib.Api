using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Badges.GetGlobalChatBadges
{
    public class GetGlobalChatBadgesResponse
    {
        [JsonProperty(PropertyName = "data")]
        public EmoteSet[] EmoteSet { get; protected set; }
    }
}
