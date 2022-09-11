using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.Transactions
{
    public class ProductData
    {
        [JsonProperty(PropertyName = "domain")]
        public string Domain { get; protected set; }
        [JsonProperty(PropertyName = "sku")]
        public string SKU { get; protected set; }
        [JsonProperty(PropertyName = "cost")]
        public Cost Cost { get; protected set; }
        [JsonProperty(PropertyName = "inDevelopment")]
        public bool InDevelopment { get; protected set; }
        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; protected set; }
        [JsonProperty(PropertyName = "expiration")]
        public string Expiration { get; protected set; }
        [JsonProperty(PropertyName = "broadcast")]
        public bool Broadcast { get; protected set; }
    }

    public class Cost
    {
        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; protected set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }
    }
}
