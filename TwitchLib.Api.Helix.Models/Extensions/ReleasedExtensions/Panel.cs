using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions
{
    public class Panel
    {
        [JsonProperty(PropertyName = "viewer_url")]
        public string ViewerUrl { get; protected set; }
        [JsonProperty(PropertyName = "height")]
        public int Height { get; protected set; }
        [JsonProperty(PropertyName = "can_link_external_content")]
        public bool CanLinkExternalContent { get; protected set; }
    }
}
