using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Predictions
{
    public class TopPredictor
    {
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; protected set; }
        [JsonProperty(PropertyName = "user_login")]
        public string UserLogin { get; protected set; }
        [JsonProperty(PropertyName = "channel_points_used")]
        public int ChannelPointsUsed { get; protected set; }
        [JsonProperty(PropertyName = "channel_points_won")]
        public int ChannelPointsWon { get; protected set; }
    }
}
