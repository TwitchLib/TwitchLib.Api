using Newtonsoft.Json;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class ActivationConfig
    {
        [JsonProperty(PropertyName = "slot")]
        public string Slot { get; protected set; }
        [JsonProperty(PropertyName = "anchor")]
        public string Anchor { get; protected set; }
    }
}
