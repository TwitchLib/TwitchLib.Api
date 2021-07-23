using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Entitlements.UpdateDropsEntitlements
{
    public class UpdateDropsEntitlementsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public DropEntitlementUpdate[] DropEntitlementUpdates { get; protected set; }
    }
}