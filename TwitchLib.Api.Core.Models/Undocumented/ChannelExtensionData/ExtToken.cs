using Newtonsoft.Json;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class ExtToken
    {
        [JsonProperty(PropertyName = "extension_id")]
        public string ExtensionId { get; protected set; }
        [JsonProperty(PropertyName = "token")]
        public string Token { get; protected set; }
    }
}
