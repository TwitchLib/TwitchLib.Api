using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack.GetCurrentTrack
{
    /// <summary>
    /// Returns the Soundtrack track that the broadcaster is playing.
    /// </summary>
    public class GetCurrentTrackResponse
    {
        /// <summary>
        /// A list that contains the Soundtrack track that the broadcaster is playing.
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public CurrentTrack[] Data { get; protected set; }
    }
}
