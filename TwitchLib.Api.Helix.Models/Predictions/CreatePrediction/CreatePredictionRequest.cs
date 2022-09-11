using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Predictions.CreatePrediction
{
    public class CreatePredictionRequest
    {
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "outcomes")]
        public Outcome[] Outcomes { get; set; }
        [JsonProperty(PropertyName = "prediction_window")]
        public int PredictionWindowSeconds { get; set; }
    }
}
