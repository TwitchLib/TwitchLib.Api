using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Entitlements.RedeemCode
{
    public class RedeemCodeResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Status[] Data { get; protected set; }
    }
}
