using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits.ExtensionBitsProducts
{
    public class ExtensionBitsProduct
    {
        [JsonProperty(PropertyName = "sku")]
        public string Sku { get; protected set; }
        [JsonProperty(PropertyName = "cost")]
        public Cost Cost { get; protected set; }
        [JsonProperty(PropertyName = "in_development")]
        public bool InDevelopment { get; protected set; }
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; protected set; }
        [JsonProperty(PropertyName = "expiration")]
        public DateTime Expiration { get; protected set; }
        [JsonProperty(PropertyName = "is_broadcast")]
        public bool IsBroadcast { get; protected set; }
    }
}
