using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
    public class DefaultImage
    {

        [JsonProperty(PropertyName = "url_1x")]
        public string Url1x { get; }
        [JsonProperty(PropertyName = "url_2x")]
        public string Url2x { get; }
        [JsonProperty(PropertyName = "url_4x")]
        public string Url4x { get; }
    }
}
