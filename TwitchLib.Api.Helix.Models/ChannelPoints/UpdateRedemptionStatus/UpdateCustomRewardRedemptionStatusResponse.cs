using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.UpdateRedemptionStatus
{
    public class UpdateRedemptionStatusResponse
    {
        [JsonProperty(PropertyName = "data")]
        public RewardRedemption[] Data { get; protected set; }
    }
}
