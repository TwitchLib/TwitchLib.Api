using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Analytics
{
    public class GetGameAnalyticsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public GameAnalytics[] Data { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Common.Pagination Pagination { get; protected set; }
    }
}
