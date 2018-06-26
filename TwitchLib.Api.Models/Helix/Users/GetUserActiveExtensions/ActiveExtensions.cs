using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Helix.Users.GetUserActiveExtensions
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
