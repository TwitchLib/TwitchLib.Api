using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Predictions.EndPrediction
{
    public class EndPredictionResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Prediction[] Data { get; protected set; }
    }
}
