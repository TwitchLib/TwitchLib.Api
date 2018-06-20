using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Helix.Users.UpdateUserExtensions
{
    public class UserExtensionState
    {
        [JsonProperty(PropertyName = "active")]
        public bool Active { get; protected set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "version")]
        public string Version { get; protected set; }

        public UserExtensionState(bool active, string id, string version)
        {
            Active = active;
            Id = id;
            Version = version;
        }
    }
}
