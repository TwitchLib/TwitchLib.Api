using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Raids
{
    public class Raid
    {
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "is_mature")]
        public bool IsMature { get; protected set; }
    }
}
