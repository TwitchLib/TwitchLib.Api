using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Entitlements.CreateEntitlementGrantsUploadURL
{
    public class CreateEntitlementGrantsUploadUrlResponse
    {
        [JsonProperty(PropertyName = "data")]
        public UploadUrl[] Data { get; protected set; }
    }
}
