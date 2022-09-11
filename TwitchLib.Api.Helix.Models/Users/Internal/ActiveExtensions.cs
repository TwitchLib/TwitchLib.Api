using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.Internal
{
    public class ActiveExtensions
    {
        [JsonProperty(PropertyName = "panel")]
        public Dictionary<string, UserActiveExtension> Panel { get; protected set; }
        [JsonProperty(PropertyName = "overlay")]
        public Dictionary<string, UserActiveExtension> Overlay { get; protected set; }
        [JsonProperty(PropertyName = "component")]
        public Dictionary<string, UserActiveExtension> Component { get; protected set; }
    }
}
