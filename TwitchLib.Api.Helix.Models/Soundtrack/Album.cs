using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack
{
    /// <summary>
    /// The album that includes the track.
    /// </summary>
    public class Album
    {
        /// <summary>
        /// The album’s ASIN (Amazon Standard Identification Number).
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }

        /// <summary>
        /// The album’s name. If the album contains explicit content, the name will contain [Explicit] in the string. For example, Let It Die [Explicit].
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }

        /// <summary>
        /// A URL to the album’s cover art.
        /// </summary>
        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl { get; protected set; }
    }
}
