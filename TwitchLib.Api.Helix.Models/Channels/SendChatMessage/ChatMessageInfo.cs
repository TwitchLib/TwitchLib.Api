using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.SendChatMessage
{
    public class ChatMessageInfo
    {
        /// <summary>
        /// The message id for the message that was sent.
        /// </summary>
        [JsonProperty(PropertyName = "message_id", NullValueHandling = NullValueHandling.Ignore)]
        public string MessageId { get; set; } = string.Empty;
        /// <summary>
        /// If the message passed all checks and was sent.
        /// </summary>
        [JsonProperty(PropertyName = "is_sent", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsSent { get; set; }
        /// <summary>
        /// 	The reason the message was dropped, if any.
        /// </summary>
        [JsonProperty(PropertyName = "drop_reason", NullValueHandling = NullValueHandling.Ignore)]
        public DropReason DropReason { get; set; }

    }
}
