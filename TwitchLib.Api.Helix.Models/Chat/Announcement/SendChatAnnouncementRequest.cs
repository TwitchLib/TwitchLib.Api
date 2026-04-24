using Newtonsoft.Json;
namespace TwitchLib.Api.Helix.Models.Chat.Announcement
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SendChatAnnouncementRequest
    {
        /// <summary>
        /// The announcement to make in the broadcaster’s chat room. 
        /// Announcements are limited to a maximum of 500 characters; 
        /// announcements longer than 500 characters are truncated.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
        /// <summary>
        /// The color used to highlight the announcement.
        /// <br/>If color is set to primary or is not set, the channel’s accent color is used to highlight the announcement (see Profile Accent Color under profile settings, Channel and Videos, and Brand).
        /// </summary>
        [JsonProperty("color")]
        public AnnouncementColors? Color { get; set; }
        /// <summary>
        /// NOTE: This parameter can only be set when utilizing an App Access Token. 
        /// <br/>It cannot be specified when a User Access Token is used, and will instead result in an HTTP 400 error.
        /// <br/><br/>Determines if the chat announcement is sent only to the source channel(defined by broadcaster_id) during a shared chat session.
        /// <br/>This has no effect if the announcement is not sent during a shared chat session.
        /// <br/><br/>The default value when using an App Access Token is <see langword="true"/>.
        /// <br/>If you prefer to send an announcement to all channels in a shared chat session, set this parameter to <see langword="false"/>.
        /// </summary>
        [JsonProperty("for_source_only")]
        public bool? ForSourceOnly { get; set; }
    }
}
