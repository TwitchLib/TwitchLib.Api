using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Soundtrack.GetPlaylist
{
    /// <summary>
    /// Returned tracks of a Soundtrack playlist.
    /// </summary>
    public class GetPlaylistResponse
    {
        /// <summary>
        /// The list of tracks in the playlist.
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public Track[] Data { get; protected set; }

        /// <summary>
        /// Contains the information used to page through a list of tracks. The object is empty if there are no more tracks to page through.
        /// </summary>
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
