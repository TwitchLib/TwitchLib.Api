using Newtonsoft.Json;
using System;

namespace TwitchLib.Api.Helix.Models.GuestStar
{
    public class Guest
    {
        /// <summary>
        /// ID representing this guest’s slot assignment.
        /// Host:0 Guests:1,2,3,etc ScreenShare:SCREENSHARE 
        /// </summary>
        [JsonProperty(PropertyName = "slot_id")]
        public string SlotId { get; protected set; }
        /// <summary>
        /// Flag determining whether or not the guest is visible in the browser source in the host’s streaming software.
        /// </summary>
        [JsonProperty(PropertyName = "is_live")]
        public bool IsLive { get; protected set; }
        /// <summary>
        /// User ID of the guest assigned to this slot.
        /// </summary>
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        /// <summary>
        /// Display name of the guest assigned to this slot.
        /// </summary>
        [JsonProperty(PropertyName = "user_display_name")]
        public string UserDisplayName { get; set; }
        /// <summary>
        /// Login of the guest assigned to this slot.
        /// </summary>
        [JsonProperty(PropertyName = "user_login")]
        public string UserLogin { get; protected set; }
        /// <summary>
        /// Value from 0 to 100 representing the host’s volume setting for this guest.
        /// </summary>
        [JsonProperty(PropertyName = "volume")]
        public int Volume { get; protected set; }
        /// <summary>
        /// Timestamp when this guest was assigned a slot in the session.
        /// </summary>
        [JsonProperty(PropertyName = "assigned_at")]
        public DateTime AssignedAt { get; protected set; }
        /// <summary>
        /// Information about the guest’s audio settings
        /// </summary>
        [JsonProperty(PropertyName = "audio_settings")]
        public MediaSettings AudioSettings { get; protected set; }
        /// <summary>
        /// Information about the guest’s audio settings
        /// </summary>
        [JsonProperty(PropertyName = "video_settings")]
        public MediaSettings VideoSettings { get; protected set; }
    }
}