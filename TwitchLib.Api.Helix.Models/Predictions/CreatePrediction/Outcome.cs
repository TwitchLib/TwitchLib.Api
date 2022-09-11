using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Predictions.CreatePrediction
{
    public class Outcome
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}
