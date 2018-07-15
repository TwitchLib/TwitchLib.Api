using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Users.Internal
{
    public class UserExtension
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "version")]
        public string Version { get; protected set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        [JsonProperty(PropertyName = "can_activate")]
        public bool CanActivate { get; protected set; }
        [JsonProperty(PropertyName = "type")]
        public string[] Type { get; protected set; }
    }
}
