using System.Collections.Generic;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Users.Internal;

namespace TwitchLib.Api.Helix.Models.Users.UpdateUserExtensions
{
    public class UpdateUserExtensionsRequest
    {
        [JsonProperty(PropertyName = "panel")]
        public Dictionary<string, UserExtensionState> Panel { get; set; }
        [JsonProperty(PropertyName = "component")]
        public Dictionary<string, UserExtensionState> Component { get; set; }
        [JsonProperty(PropertyName = "overlay")]
        public Dictionary<string, UserExtensionState> Overlay { get; set; }
    }
}
