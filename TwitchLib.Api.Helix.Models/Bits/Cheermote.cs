using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits
{
    public class Cheermote
    {
        [JsonProperty(PropertyName = "prefix")]
        public string Prefix { get; protected set; }
        [JsonProperty(PropertyName = "tiers")]
        public Tier[] Tiers { get; protected set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }
        [JsonProperty(PropertyName = "order")]
        public int Order { get; protected set; }
        [JsonProperty(PropertyName = "last_updated")]
        public DateTime LastUpdated { get; protected set; }
        [JsonProperty(PropertyName = "is_charitable")]
        public bool IsCharitable { get; protected set; }
    }
}
