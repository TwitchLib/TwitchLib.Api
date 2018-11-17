using Newtonsoft.Json;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class Views
    {
        [JsonProperty(PropertyName = "video_overlay")]
        public View VideoOverlay { get; protected set; }
        [JsonProperty(PropertyName = "config")]
        public View Config { get; protected set; }
    }
}
