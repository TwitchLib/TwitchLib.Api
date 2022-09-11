using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack
{
    /// <summary>
    /// The source of the track that’s currently playing. For example, a playlist or station.
    /// </summary>
    public class Source
    {
        /// <summary>
        /// The playlist’s or station’s ASIN (Amazon Standard Identification Number).
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }

        /// <summary>
        /// The type of content that id maps to. Possible values are: PLAYLIST or STATION
        /// </summary>
        [JsonProperty(PropertyName = "content_type")]
        public string ContentType { get; protected set; }

        /// <summary>
        /// The playlist’s or station’s title.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }

        /// <summary>
        /// A URL to the playlist’s or station’s image art.
        /// </summary>
        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl { get; protected set; }

        /// <summary>
        /// A URL to the playlist on Soundtrack. The string is empty if content-type is STATION.
        /// </summary>
        [JsonProperty(PropertyName = "soundtrack_url")]
        public string SoundtrackUrl { get; protected set; }

        /// <summary>
        /// A URL to the playlist on Spotify. The string is empty if content-type is STATION.
        /// </summary>
        [JsonProperty(PropertyName = "spotify_url")]
        public string SpotifyUrl { get; protected set; }
    }
}
