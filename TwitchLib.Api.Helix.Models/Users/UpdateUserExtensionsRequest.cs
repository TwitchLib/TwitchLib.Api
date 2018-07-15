using Newtonsoft.Json;
using System.Collections.Generic;
using TwitchLib.Api.Helix.Models.Users.Internal;

namespace TwitchLib.Api.Helix.Models.Users
{
    public class UpdateUserExtensionsRequest
    {
        [JsonProperty(PropertyName = "panel")]
        public Dictionary<string, UserExtensionState> panel { get; set; }
        [JsonProperty(PropertyName = "component")]
        public Dictionary<string, UserExtensionState> component { get; set; }
        [JsonProperty(PropertyName = "overlay")]
        public Dictionary<string, UserExtensionState> overlay { get; set; }
    }
}
