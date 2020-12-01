using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.GetCustomRewardRedemption
{
    public class GetCustomRewardRedemptionResponse
    {
        [JsonProperty(PropertyName = "data")]
        public RewardRedemption[] Data { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
