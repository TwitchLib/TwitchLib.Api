using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomRewardRedemptionStatus
{
    public class UpdateCustomRewardRedemptionStatusRequest
    {
        [JsonProperty(PropertyName = "status")]
        public CustomRewardRedemptionStatus Status { get; protected set; }
    }
}
