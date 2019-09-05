using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Subscriptions
{
    public class Subscription
    {
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_name")]
        public string BroadcasterName { get; protected set; }
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
    }
}