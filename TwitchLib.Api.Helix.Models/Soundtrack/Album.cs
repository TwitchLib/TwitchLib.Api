using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack
{
    public class Album
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl { get; protected set; }
    }
}
