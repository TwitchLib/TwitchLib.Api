using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.HypeTrain
{
    public class HypeTrainContribution
    {
        [JsonProperty(PropertyName = "total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }
        [JsonProperty(PropertyName = "user")]
        public string UserId { get; protected set; }
    }
}