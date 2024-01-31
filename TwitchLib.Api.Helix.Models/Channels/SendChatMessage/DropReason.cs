using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.SendChatMessage
{
    public class DropReason
    {
        [JsonProperty(PropertyName = "code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; } = string.Empty;
    }
}
