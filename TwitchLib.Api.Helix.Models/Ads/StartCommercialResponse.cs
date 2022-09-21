using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Ads
{
    public class StartCommercialResponse
    {
        [JsonProperty(PropertyName = "length")]
        public int Length { get; protected set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; protected set; }
        [JsonProperty(PropertyName = "retry_after")]
        public int RetryAfter { get; protected set; }
    }
}
