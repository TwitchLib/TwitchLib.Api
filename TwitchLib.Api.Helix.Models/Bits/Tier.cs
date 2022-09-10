using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits
{
    public class Tier
    {
        [JsonProperty(PropertyName = "min_bits")]
        public int MinBits { get; protected set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "color")]
        public string Color { get; protected set; }
        [JsonProperty(PropertyName = "images")]
        public Images Images { get; protected set; }
        [JsonProperty(PropertyName = "can_cheer")]
        public bool CanCheer { get; protected set; }
        [JsonProperty(PropertyName = "show_in_bits_card")]
        public bool ShowInBitsCard { get; protected set; }
    }
}
