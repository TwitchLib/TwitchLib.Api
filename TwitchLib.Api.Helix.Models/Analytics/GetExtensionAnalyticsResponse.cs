using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Analytics
{
    public class GetExtensionAnalyticsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ExtensionAnalytics[] Data { get; protected set; }
    }
}
