using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Entitlements.GetDropsEntitlements
{
    public class GetDropsEntitlementsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public DropsEntitlement[] DropEntitlements { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
