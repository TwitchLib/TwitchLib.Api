using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomRewardRedemptionStatus
{
    public class UpdateCustomRewardRedemptionStatusResponse
    {
        [JsonProperty(PropertyName = "data")]
        public RewardRedemption[] Data { get; protected set; }
    }
}
