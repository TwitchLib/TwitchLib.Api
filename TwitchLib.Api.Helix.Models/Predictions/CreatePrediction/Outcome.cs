using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Predictions.CreatePrediction
{
    public class Outcome
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}
