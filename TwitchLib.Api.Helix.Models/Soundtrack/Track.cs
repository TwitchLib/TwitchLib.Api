using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack
{
    /// <summary>
    /// The track
    /// </summary>
    public class Track
    {
        /// <summary>
        /// The artists included on the track.
        /// </summary>
        [JsonProperty(PropertyName = "artists")]
        public Artist[] Artists { get; protected set; }

        /// <summary>
        /// The track’s ASIN (Amazon Standard Identification Number).
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }

        /// <summary>
        /// The duration of the track, in seconds.
        /// </summary>
        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; protected set; }

        /// <summary>
        /// The track’s title. If the track contains explicit content, the title will contain [Explicit] in the string. For example, Let It Die [Explicit].
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }

        /// <summary>
        /// The album that includes the track.
        /// </summary>
        [JsonProperty(PropertyName = "album")]
        public Album Album { get; protected set; }

        /// <summary>
        /// The track’s ISRC (International Standard Recording Code).
        /// </summary>
        [JsonProperty(PropertyName = "isrc")]
        public string ISRC { get; protected set; }
    }
}
