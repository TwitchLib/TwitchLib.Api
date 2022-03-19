using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TwitchLib.Api.Helix.Models.Bits
{
    public class ImageList
    {
        [JsonProperty(PropertyName = "animated")]
        public Dictionary<string, string> Animated { get; protected set; }
        [JsonProperty(PropertyName = "static")]
        public Dictionary<string, string> Static { get; protected set; }
    }
}
