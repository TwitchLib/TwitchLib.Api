using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.GetCustomReward
{
    public class GetCustomRewardsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public CustomReward[] Data { get; protected set; }
    }
}
