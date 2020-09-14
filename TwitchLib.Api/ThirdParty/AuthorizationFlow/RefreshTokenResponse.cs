using Newtonsoft.Json;

namespace TwitchLib.Api.ThirdParty.AuthorizationFlow
{
    public class RefreshTokenResponse
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; protected set; }
        [JsonProperty(PropertyName = "refresh")]
        public string Refresh { get; protected set; }
        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; protected set; }
    }
}
