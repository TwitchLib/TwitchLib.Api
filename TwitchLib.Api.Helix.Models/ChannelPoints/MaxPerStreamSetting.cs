using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
    public class MaxPerStreamSetting
    {
        [JsonProperty(PropertyName = "is_enabled")]
        public bool IsEnabled { get; protected set; }
        [JsonProperty(PropertyName = "max_per_stream")]
        public int MaxPerStream { get; protected set; }
    }
}
