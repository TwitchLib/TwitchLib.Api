using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.StreamsMetadata
{
    public class PlayerOverwatch
    {
        [JsonProperty(PropertyName = "hero")]
        public HeroOverwatch Hero { get; protected set; }
    }
}
