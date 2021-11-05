using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.UpdateRedemptionStatus
{
    public class UpdateRedemptionStatusResponse
    {
        [JsonProperty(PropertyName = "data")]
        public RewardRedemption[] Data { get; protected set; }
    }
}
