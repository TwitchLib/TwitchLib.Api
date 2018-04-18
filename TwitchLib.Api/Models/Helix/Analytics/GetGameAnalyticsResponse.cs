using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Helix.Analytics
{
    public class GetGameAnalyticsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public GameAnalytics[] GameId { get; protected set; }
    }
}
