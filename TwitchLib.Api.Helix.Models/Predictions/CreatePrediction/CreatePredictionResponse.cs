#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Predictions.CreatePrediction;

/// <summary>
/// Create prediction respsone object.
/// </summary>
public class CreatePredictionResponse
{
    /// <summary>
    /// A list that contains the single prediction that you created.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Prediction[] Data { get; protected set; }
}
