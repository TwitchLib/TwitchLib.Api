using Newtonsoft.Json;

namespace TwitchLib.Api.ThirdParty.AuthorizationFlow
{
    public class CreatedFlow
    {
        [JsonProperty(PropertyName = "message")]
        public string Url { get; protected set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
    }
}
