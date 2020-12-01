using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomReward
{
    public class UpdateCustomRewardResponse
    {
        [JsonProperty(PropertyName = "data")]
        public CustomReward[] Data { get; protected set; }
    }
}
