using Newtonsoft.Json;

namespace TwitchLib.Api.Auth
{
    public class AccessCodeResponse
    {
        [JsonProperty(PropertyName = "access_code")]
        public string AccessCode { get; set; }

        [JsonProperty(PropertyName = "scope")]
        public string[] Scopes { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
    }
}
