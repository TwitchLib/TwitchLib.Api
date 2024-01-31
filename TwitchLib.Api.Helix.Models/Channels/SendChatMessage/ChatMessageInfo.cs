using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.SendChatMessage
{
    public class ChatMessageInfo
    {
        [JsonProperty(PropertyName = "message_id", NullValueHandling = NullValueHandling.Ignore)]
        public string MessageId { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "is_sent", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsSent { get; set; }
        [JsonProperty(PropertyName = "drop_reason", NullValueHandling = NullValueHandling.Ignore)]
        public DropReason? DropReason { get; set; }

    }
}
