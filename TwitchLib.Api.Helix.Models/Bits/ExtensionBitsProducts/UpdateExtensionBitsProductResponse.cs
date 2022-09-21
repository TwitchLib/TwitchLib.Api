using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits.ExtensionBitsProducts
{
    public class UpdateExtensionBitsProductResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ExtensionBitsProduct[] Data { get; protected set; }
    }
}
