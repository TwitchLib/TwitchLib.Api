using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions
{
    public class Views
    {
        [JsonProperty(PropertyName = "mobile")]
        public Mobile Mobile { get; protected set; }
        [JsonProperty(PropertyName = "panel")]
        public Panel Panel { get; protected set; }
        [JsonProperty(PropertyName = "video_overlay")]
        public VideoOverlay VideoOverlay { get; protected set; }
        [JsonProperty(PropertyName = "component")]
        public Component Component { get; protected set; }
    }
}
