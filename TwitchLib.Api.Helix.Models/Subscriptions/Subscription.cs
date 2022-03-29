using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Subscriptions
{
    public class Subscription
    {
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_name")]
        public string BroadcasterName { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_login")]
        public string BroadcasterLogin { get; protected set; }
        [JsonProperty(PropertyName = "is_gift")]
        public bool IsGift { get; protected set; }
        [JsonProperty(PropertyName = "tier")]
        public string Tier { get; protected set; }
        [JsonProperty(PropertyName = "plan_name")]
        public string PlanName { get; protected set; }
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; protected set; }
        [JsonProperty(PropertyName = "user_login")]
        public string UserLogin { get; protected set; }
        [JsonProperty(PropertyName = "gifter_id")]
        public string GiftertId { get; protected set; }
        [JsonProperty(PropertyName = "gifter_name")]
        public string GifterName { get; protected set; }
        [JsonProperty(PropertyName = "gifter_login")]
        public string GifterLogin { get; protected set; }
    }
}
