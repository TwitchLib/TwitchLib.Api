using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Badges
{
    public class EmoteSet
    {
        [JsonProperty(PropertyName = "set_id")]
        public string SetId { get; protected set; }
        [JsonProperty(PropertyName = "versions")]
        public Version[] Versions { get; protected set; }
    }
}
