using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Helix.Analytics
{
    public class GetExtensionAnalyticsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ExtensionAnalytics[] Data { get; protected set; }
    }
}
