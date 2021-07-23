using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Entitlements.GetCodeStatus
{
    public class GetCodeStatusResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Status[] Data { get; protected set; }
    }
}
