using Newtonsoft.Json;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class ViewerUrls
    {
        [JsonProperty(PropertyName = "video_overlay")]
        public string VideoOverlay { get; protected set; }
    }
}
