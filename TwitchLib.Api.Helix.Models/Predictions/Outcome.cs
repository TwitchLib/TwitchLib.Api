using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Predictions
{
    public class Outcome
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        [JsonProperty(PropertyName = "users")]
        public int ChannelPoints { get; protected set; }
        [JsonProperty(PropertyName = "channel_points")]
        public int ChannelPointsVotes { get; protected set; }
        [JsonProperty(PropertyName = "top_predictors")]
        public TopPredictor[] TopPredictors { get; protected set; }
        [JsonProperty(PropertyName = "color")]
        public string Color { get; protected set; }
    }
}
