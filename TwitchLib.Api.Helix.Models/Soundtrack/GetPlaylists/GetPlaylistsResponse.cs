using System;
using Newtonsoft.Json;

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
    }
}
