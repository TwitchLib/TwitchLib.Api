using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Analytics
{
    public class ExtensionAnalytics
    {
        [JsonProperty(PropertyName = "extension_id")]
        public string ExtensionId { get; protected set; }
        [JsonProperty(PropertyName = "URL")]
        public string Url { get; protected set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }
        [JsonProperty(PropertyName = "date_range")]
        public Common.DateRange DateRange { get; protected set; }
    }
}
