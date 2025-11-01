using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Channels.SendChatMessage
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SendChatMessageRequest
    {
        /// <summary>
        /// The ID of the broadcaster whose chat room the message will be sent to.
        /// </summary>
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; set; }
        /// <summary>
        /// The ID of the user sending the message. 
        /// This ID must match the user ID in the user access token.
        /// </summary>
        [JsonProperty(PropertyName = "sender_id")]
        public string SenderId { get; set; }
        /// <summary>
        /// The message to send. The message is limited to a maximum of 500 characters. 
        /// Chat messages can also include emoticons. To include emoticons, use the name of the emote. 
        /// The names are case sensitive. Don’t include colons around the name (e.g., :bleedPurple:). 
        /// If Twitch recognizes the name, Twitch converts the name to the emote before writing the chat message to the chat room
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
        /// <summary>
        /// The ID of the chat message being replied to. 
        /// If omitted, the message is not a reply
        /// </summary>
        [JsonProperty(PropertyName = "reply_parent_message_id")]
        public string? ReplyParentMessageId { get; set; }
        /// <summary>
        /// <para>NOTE: This parameter can only be set when utilizing an App Access Token. It cannot be specified when a User Access Token is used, and will instead result in an HTTP 400 error.</para>
        /// <para>Determines if the chat message is sent only to the source channel (defined by <see cref="BroadcasterId"/>) during a shared chat session. This has no effect if the message is sent during a shared chat session.</para>
        /// <para>If this parameter is <c>null</c>, the default value when using an App Access Token is <c>false</c></para>
        /// </summary>
        [JsonProperty(PropertyName = "for_source_only")]
        public bool? ForSourceOnly { get; set; }

    }
}
