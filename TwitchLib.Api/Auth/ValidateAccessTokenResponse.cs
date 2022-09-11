using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.Auth
{
    public class ValidateAccessTokenResponse
    {
        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; protected set; }
        [JsonProperty(PropertyName = "login")]
        public string Login { get; protected set; }
        [JsonProperty(PropertyName = "scopes")]
        public List<string> Scopes { get; protected set; }
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; protected set; }
    }
}
