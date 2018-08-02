using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Webhooks
{
    public class GetWebhookSubscriptionsResponse
    {
        [JsonProperty(PropertyName = "total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "data")]
        public Subscription[] Subscriptions { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
