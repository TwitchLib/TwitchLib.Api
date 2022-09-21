using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.CreateCustomReward
{
    public class CreateCustomRewardsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public CustomReward[] Data { get; protected set; }
    }
}
