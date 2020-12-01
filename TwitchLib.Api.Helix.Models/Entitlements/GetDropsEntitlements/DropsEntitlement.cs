using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Entitlements.GetDropsEntitlements
{
    public class DropsEntitlement
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "benefit_id")]
        public string BenefitId { get; protected set; }
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; protected set; }
        [JsonProperty(PropertyName = "UserId")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "GameId")]
        public string GameId { get; protected set; }
    }
}
