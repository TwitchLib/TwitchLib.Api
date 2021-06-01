using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.GetCustomReward
{
    public class GetCustomRewardsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public CustomReward[] Data { get; protected set; }
    }
}
