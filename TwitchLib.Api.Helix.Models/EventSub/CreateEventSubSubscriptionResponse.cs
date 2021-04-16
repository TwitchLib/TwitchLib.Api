using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub
{
    public class CreateEventSubSubscriptionResponse
    {
        [JsonProperty(PropertyName = "data")]
        public EventSubSubscription[] Subscriptions { get; protected set; }
        [JsonProperty(PropertyName = "total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "total_cost")]
        public int TotalCost { get; protected set; }
        [JsonProperty(PropertyName = "max_total_cost")]
        public int MaxTotalCost { get; protected set; }
    }
}