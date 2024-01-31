using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.SendChatMessage
{
    public class SendChatMessageResponse
    {
        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public ChatMessageInfo[] Data { get; protected set; }
    }
}
