using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack.GetCurrentTrack
{
    /// <summary>
    /// Soundtrack track that the broadcaster is playing.
    /// </summary>
    public class CurrentTrack
    {
        /// <summary>
        /// The track that’s currently playing.
        /// </summary>
        [JsonProperty(PropertyName = "track")]
        public Track Track { get; protected set; }

        /// <summary>
        /// The source of the track that’s currently playing. For example, a playlist or station.
        /// </summary>
        [JsonProperty(PropertyName = "source")]
        public Source Source { get; protected set; }
    }
}