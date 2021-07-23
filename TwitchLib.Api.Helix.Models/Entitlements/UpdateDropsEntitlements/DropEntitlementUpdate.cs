using Newtonsoft.Json;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.Entitlements.UpdateDropsEntitlements
{
    public class DropEntitlementUpdate
    {
        [JsonProperty(PropertyName = "status")]
        public DropEntitlementUpdateStatus Status { get; protected set; }
        [JsonProperty(PropertyName = "ids")]
        public string[] Ids { get; protected set; }
    }
}