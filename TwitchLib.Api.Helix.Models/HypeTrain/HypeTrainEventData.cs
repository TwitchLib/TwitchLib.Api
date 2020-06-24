using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.HypeTrain
{
    public class HypeTrainEventData
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonProperty(PropertyName = "started_at")]
        public string StartedAt { get; protected set; }
        [JsonProperty(PropertyName = "expires_at")]
        public string ExpiresAt { get; protected set; }
        [JsonProperty(PropertyName = "cooldown_end_time")]
        public string CooldownEndTime { get; protected set; }
        [JsonProperty(PropertyName = "level")]
        public int Level { get; protected set; }
        [JsonProperty(PropertyName = "goal")]
        public int Goal { get; protected set; }
        [JsonProperty(PropertyName = "total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "top_contribution")]
        public HypeTrainContribution TopContribution { get; protected set; }
        [JsonProperty(PropertyName = "last_contribution")]
        public HypeTrainContribution LastContribution { get; protected set; }
    }
}