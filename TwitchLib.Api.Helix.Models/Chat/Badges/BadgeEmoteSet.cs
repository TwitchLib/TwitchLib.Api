using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Badges
{
    public class BadgeEmoteSet
    {
        [JsonProperty(PropertyName = "set_id")]
        public string SetId { get; protected set; }
        [JsonProperty(PropertyName = "versions")]
        public BadgeVersion[] Versions { get; protected set; }
    }
}
