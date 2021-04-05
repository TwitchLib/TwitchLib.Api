using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Ads
{
    public class StartCommercialRequest
    {
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; set; }
        [JsonProperty(PropertyName = "length")]
        public int Length { get; set; }
    }
}
