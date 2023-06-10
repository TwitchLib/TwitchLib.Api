using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.GuestStar
{
    public class GuestStarSession
    {
        /// <summary>
        /// ID uniquely representing the Guest Star session.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        /// <summary>
        /// List of guests currently interacting with the Guest Star session.
        /// </summary>
        [JsonProperty(PropertyName = "guests")]
        public Guest Guest { get; protected set; }
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