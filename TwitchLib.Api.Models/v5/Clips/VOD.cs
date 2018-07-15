﻿using Newtonsoft.Json;

namespace TwitchLib.Api.Models.V5.Clips
{
    public class VOD
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; protected set; }
    }
}
