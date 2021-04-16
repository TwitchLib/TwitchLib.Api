using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.EventSub
{
    public class GetEventSubSubscriptionsResponse
    {
        [JsonProperty(PropertyName = "total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "data")]
        public EventSubSubscription[] Subscriptions { get; protected set; }
        [JsonProperty(PropertyName = "total_cost")]
        public int TotalCost { get; protected set; }
        [JsonProperty(PropertyName = "max_total_cost")]
        public int MaxTotalCost { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}