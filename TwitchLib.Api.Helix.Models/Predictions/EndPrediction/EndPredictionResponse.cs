using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Predictions.EndPrediction
{
    public class EndPredictionResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Prediction[] Data { get; protected set; }
    }
}
