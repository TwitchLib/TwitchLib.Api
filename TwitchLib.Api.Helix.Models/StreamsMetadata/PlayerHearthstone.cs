using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.StreamsMetadata
{
    public class PlayerHearthstone
    {
        [JsonProperty(PropertyName = "hero")]
        public HeroHearthstone Hero { get; protected set; }
    }
}
