using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions
{
    public class Component
    {
        [JsonProperty(PropertyName = "viewer_url")]
        public string ViewerUrl { get; protected set; }
        [JsonProperty(PropertyName = "aspect_width")]
        public int AspectWidth { get; protected set; }
        [JsonProperty(PropertyName = "aspect_height")]
        public int AspectHeight { get; protected set; }
        [JsonProperty(PropertyName = "aspect_ratio_x")]
        public int AspectRatioX { get; protected set; }
        [JsonProperty(PropertyName = "aspect_ratio_y")]
        public int AspectRatioY { get; protected set; }
        [JsonProperty(PropertyName = "autoscale")]
        public bool Autoscale { get; protected set; }
        [JsonProperty(PropertyName = "scale_pixels")]
        public int ScalePixels { get; protected set; }
        [JsonProperty(PropertyName = "target_height")]
        public int TargetHeight { get; protected set; }
        [JsonProperty(PropertyName = "size")]
        public int Size { get; protected set; }
        [JsonProperty(PropertyName = "zoom")]
        public bool Zoom { get; protected set; }
        [JsonProperty(PropertyName = "zoom_pixels")]
        public int ZoomPixels { get; protected set; }
        [JsonProperty(PropertyName = "can_link_external_content")]
        public string CanLinkExternalContent { get; protected set; }
    }
}
