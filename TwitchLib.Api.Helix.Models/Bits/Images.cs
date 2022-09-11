using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits
{
    public class Images
    {
        [JsonProperty(PropertyName = "dark")]
        public ImageList Dark { get; protected set; }
        [JsonProperty(PropertyName = "light")]
        public ImageList Light { get; protected set; }
    }
}
