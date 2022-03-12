using Newtonsoft.Json;
using System;

namespace TwitchLib.Api.Helix.Models.Bits
{
    public class Images
    {
        [JsonProperty(PropertyName = "dark")]
        public Image Dark { get; protected set; }
        [JsonProperty(PropertyName = "light")]
        public Image Light { get; protected set; }
    }
}
