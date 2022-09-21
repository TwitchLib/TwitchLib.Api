using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack
{
    /// <summary>
    /// The artists included on the track.
    /// </summary>
    public class Artist
    {
        /// <summary>
        /// The artist’s ASIN (Amazon Standard Identification Number).
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }

        /// <summary>
        /// The artist’s name. This can be the band’s name or the solo artist’s name.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }

        /// <summary>
        /// The ID of the Twitch user that created the track. Is empty if a Twitch user didn’t create the track.
        /// </summary>
        [JsonProperty(PropertyName = "creator_channel_id")]
        public string CreatorChannelId { get; protected set; }
    }
}
