using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Soundtrack.GetPlaylists
{
    /// <summary>
    /// Returns a list of Soundtrack playlists.
    /// </summary>
    public class GetPlaylistsResponse
    {
        /// <summary>
        /// The list of Soundtrack playlists.
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public PlaylistMetadata[] Data { get; protected set; }

        /// <summary>
        /// Contains the information used to page through a list of playlists. The object is empty if there are no more playlists to page through.
        /// </summary>
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
