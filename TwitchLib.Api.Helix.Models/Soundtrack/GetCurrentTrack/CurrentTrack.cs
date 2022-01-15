using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack.GetCurrentTrack
{
    public class CurrentTrack
    {
        [JsonProperty(PropertyName = "track")]
        public Track Track { get; protected set; }
        [JsonProperty(PropertyName = "source")]
        public Source Source { get; protected set; }
    }
}
