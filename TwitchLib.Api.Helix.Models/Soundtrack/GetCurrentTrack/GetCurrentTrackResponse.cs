using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack.GetCurrentTrack
{
    public class GetCurrentTrackResponse
    {
        [JsonProperty(PropertyName = "data")]
        public CurrentTrack[] Data { get; protected set; }
    }
}
