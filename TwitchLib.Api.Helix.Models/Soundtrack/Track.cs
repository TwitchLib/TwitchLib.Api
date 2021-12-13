using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack
{
    public class Track
    {
        [JsonProperty(PropertyName = "artists")]
        public Artist[] Artists { get; protected set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        [JsonProperty(PropertyName = "album")]
        public Album Album { get; protected set; }
    }
}
