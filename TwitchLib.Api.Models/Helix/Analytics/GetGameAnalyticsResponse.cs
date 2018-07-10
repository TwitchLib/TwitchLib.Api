using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Helix.Analytics
{
    public class GetGameAnalyticsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public GameAnalytics[] Data { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Common.Pagination Pagination { get; protected set; }
    }
}
