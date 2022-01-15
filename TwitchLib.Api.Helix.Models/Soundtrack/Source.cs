using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack
{
    public class Source
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "content_type")]
        public string ContentType { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl { get; protected set; }
        [JsonProperty(PropertyName = "soundtrack_url")]
        public string SoundtrackUrl { get; protected set; }
        [JsonProperty(PropertyName = "spotify_url")]
        public string SpotifyUrl { get; protected set; }
    }
}
