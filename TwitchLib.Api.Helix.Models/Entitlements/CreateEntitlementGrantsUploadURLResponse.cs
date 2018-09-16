using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Entitlements
{
    public class CreateEntitlementGrantsUploadUrlResponse
    {
        [JsonProperty(PropertyName = "data")]
        public UploadUrl[] Data { get; protected set; }
    }
}
