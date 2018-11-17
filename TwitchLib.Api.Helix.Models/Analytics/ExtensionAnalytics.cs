using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Analytics
{
    public class ExtensionAnalytics
    {
        [JsonProperty(PropertyName = "extension_id")]
        public string ExtensionId { get; protected set; }
        [JsonProperty(PropertyName = "URL")]
        public string Url { get; protected set; }
    }
}
