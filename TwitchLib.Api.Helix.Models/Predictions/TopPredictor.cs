using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Predictions
{
    public class TopPredictor
    {
        [JsonProperty(PropertyName = "user")]
        public User User { get; protected set; }
    }
}
