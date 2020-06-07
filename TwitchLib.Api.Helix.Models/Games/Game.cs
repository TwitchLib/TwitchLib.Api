﻿using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Games
{
    public class Game
    {
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        [JsonProperty(PropertyName = "box_art_url")]
        public string BoxArtUrl { get; protected set; }
    }
}
