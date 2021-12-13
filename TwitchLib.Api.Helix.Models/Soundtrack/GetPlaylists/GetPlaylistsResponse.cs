using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack.GetPlaylists
{
    public class GetPlaylistsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public PlaylistMetadata[] Data { get; protected set; }
    }
}
