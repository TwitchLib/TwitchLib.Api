using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Predictions
{
    public class TopPredictor
    {
        [JsonProperty(PropertyName = "user")]
        public User User { get; protected set; }
    }
}
