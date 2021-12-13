using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack.GetPlaylist
{
    public class GetPlaylistResponse
    {
        [JsonProperty(PropertyName = "data")]
        public PlaylistTrack[] Data { get; protected set; }
    }
}
