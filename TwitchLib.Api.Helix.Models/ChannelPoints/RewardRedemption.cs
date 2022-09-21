using System;
using Newtonsoft.Json;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
    public class RewardRedemption
    {
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_login")]
        public string BroadcasterLogin { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_name")]
        public string BroadcasterName { get; protected set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "user_login")]
        public string UserLogin { get; protected set; }
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; protected set; }
        [JsonProperty(PropertyName = "user_input")]
        public string UserInput { get; protected set; }
        [JsonProperty(PropertyName = "status")]
        public CustomRewardRedemptionStatus Status { get; protected set; }
        [JsonProperty(PropertyName = "redeemed_at")]
        public DateTime RedeemedAt { get; protected set; }
        [JsonProperty(PropertyName = "reward")]
        public Reward Reward { get; protected set; }
    }
}
