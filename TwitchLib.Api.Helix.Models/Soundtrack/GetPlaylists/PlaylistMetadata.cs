using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack.GetPlaylists
{
    public class PlaylistMetadata
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl { get; protected set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; protected set; }
    }
}
