using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Helix.Users.UpdateUserExtensions
{
    public class Payload
    {
        [JsonProperty(PropertyName = "panel")]
        public Dictionary<string, UserExtensionState> panel { get; set; }
        [JsonProperty(PropertyName = "component")]
        public Dictionary<string, UserExtensionState> component { get; set; }
        [JsonProperty(PropertyName = "overlay")]
        public Dictionary<string, UserExtensionState> overlay { get; set; }
    }
}
