using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Chat
{
    public class EmoteSet
    {
        [JsonProperty(PropertyName = "set_id")]
        public string SetId { get; protected set; }
        [JsonProperty(PropertyName = "versions")]
        public Version[] Versions { get; protected set; }
    }
}
