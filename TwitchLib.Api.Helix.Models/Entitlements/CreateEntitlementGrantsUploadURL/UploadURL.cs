using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Entitlements.CreateEntitlementGrantsUploadURL
{
    public class UploadUrl
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; protected set; }
    }
}
