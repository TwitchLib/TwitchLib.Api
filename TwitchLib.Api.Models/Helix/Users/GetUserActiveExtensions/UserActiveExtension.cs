using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Helix.Users.GetUserActiveExtensions
{
    public class UserActiveExtension
    {
        [JsonProperty(PropertyName = "active")]
        public bool Active { get; protected set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "version")]
        public string Version { get; protected set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        [JsonProperty(PropertyName = "x")]
        public int X { get; protected set; }
        [JsonProperty(PropertyName = "y")]
        public int Y { get; protected set; }
    }
}
