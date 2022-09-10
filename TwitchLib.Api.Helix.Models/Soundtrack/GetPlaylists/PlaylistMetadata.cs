using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack.GetPlaylists
{
    /// <summary>
    /// Soundtrack playlist.
    /// </summary>
    public class PlaylistMetadata
    {
        /// <summary>
        /// The playlist’s title.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }

        /// <summary>
        /// The playlist’s ASIN (Amazon Standard Identification Number).
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }

        /// <summary>
        /// A URL to the playlist’s image art. Is empty if the playlist doesn’t include art.
        /// </summary>
        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl { get; protected set; }

        /// <summary>
        /// A short description about the music that the playlist includes.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; protected set; }
    }
}
