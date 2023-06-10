﻿using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.GuestStar
{
    public class GuestStarSettings
    {
        /// <summary>
        /// Flag determining if Guest Star moderators have access to control whether a guest is live once assigned to a slot.
        /// </summary>
        [JsonProperty(PropertyName = "is_moderator_send_live_enabled")]
        public bool IsModeratorSendLiveEnabled { get; protected set; }
        /// <summary>
        /// Number of slots the Guest Star call interface will allow the host to add to a call. Required to be between 1 and 6.
        /// </summary>
        [JsonProperty(PropertyName = "slot_count")]
        public int SlotCount { get; protected set; }
        /// <summary>
        /// Flag determining if Browser Sources subscribed to sessions on this channel should output audio
        /// </summary>
        [JsonProperty(PropertyName = "is_browser_source_audio_enabled")]
        public bool IsBrowserSourceAudioEnabled { get; protected set; }
        /// <summary>
        /// This setting determines how the guests within a session should be laid out within the browser source.
        /// </summary>
        [JsonProperty(PropertyName = "group_layout")]
        public GroupLayout GroupLayout { get; set; }
        /// <summary>
        /// View only token to generate browser source URLs
        /// </summary>
        [JsonProperty(PropertyName = "browser_source_token")]
        public string BrowserSourceToken { get; protected set; }
    }
}
