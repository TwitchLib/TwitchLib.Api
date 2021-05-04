using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Predictions.CreatePrediction
{
    public class CreatePredictionResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Prediction[] Data { get; protected set; }
    }
}
