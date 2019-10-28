using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Extensions.Transactions
{
    public class ProductData
    {
        [JsonProperty(PropertyName = "sku")]
        public string SKU { get; protected set; }
        [JsonProperty(PropertyName = "cost")]
        public Cost Cost { get; protected set; }
        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; protected set; }
        [JsonProperty(PropertyName = "inDevelopment")]
        public bool InDevelopment { get; protected set; }
    }

    public class Cost
    {
        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; protected set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }
    }
}
