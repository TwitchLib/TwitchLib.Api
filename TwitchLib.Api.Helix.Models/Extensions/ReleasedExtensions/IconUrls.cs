using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions
{
    public class IconUrls
    {
        [JsonProperty(PropertyName = "100x100")]
        public string Size100x100 { get; protected set; }
        [JsonProperty(PropertyName = "24x24")]
        public string Size24x24 { get; protected set; }
        [JsonProperty(PropertyName = "300x200")]
        public string Size300x200 { get; protected set; }
    }
}
