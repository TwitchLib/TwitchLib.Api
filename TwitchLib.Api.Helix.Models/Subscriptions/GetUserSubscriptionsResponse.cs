using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Subscriptions
{
    public class GetUserSubscriptionsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Subscription[] Data { get; protected set; }
    }
}