﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Predictions.GetPredictions
{
    public class GetPredictionsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Prediction[] Data { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
