using Newtonsoft.Json;
using System;

namespace TwitchLib.Api.Helix.Models.GuestStar
{
    public class GuestStarInvite
    {
        /// <summary>
        /// Twitch User ID corresponding to the invited guest
        /// </summary>
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        /// <summary>
        /// Timestamp when this user was invited to the session.
        /// </summary>
        [JsonProperty(PropertyName = "invited_at")]
        public DateTime InvitedAt { get; protected set; }
        /// <summary>
        /// Status representing the invited user’s join state. Invited,Accepted,Ready
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public InviteStatus Status { get; protected set; }
        /// <summary>
        /// Flag signaling that the invited user has chosen to disable their local video device.
        /// The user has hidden themselves, but they may choose to reveal their video feed upon joining the session.
        /// </summary>
        [JsonProperty(PropertyName = "is_video_enabled")]
        public bool IsVideoEnabled { get; set; }
        /// <summary>
        /// Flag signaling that the invited user has chosen to disable their local audio device.
        /// The user has muted themselves, but they may choose to unmute their audio feed upon joining the session.
        /// </summary>
        [JsonProperty(PropertyName = "is_audio_enabled")]
        public bool IsAudioEnabled { get; protected set; }
        /// <summary>
        /// Flag signaling that the invited user has a video device available for sharing.
        /// </summary>
        [JsonProperty(PropertyName = "is_video_available")]
        public bool IsVideoAvailable { get; protected set; }
        /// <summary>
        /// Flag signaling that the invited user has an audio device available for sharing.
        /// </summary>
        [JsonProperty(PropertyName = "is_audio_available")]
        public bool IsAudioAvailable { get; protected set; }
    }
}