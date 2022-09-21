using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
    public class Image
    {
        [JsonProperty(PropertyName = "url_1x")]
        public string Url1x { get; protected set; }
        [JsonProperty(PropertyName = "url_2x")]
        public string Url2x { get; protected set; }
        [JsonProperty(PropertyName = "url_4x")]
        public string Url4x { get; protected set; }
    }
}
