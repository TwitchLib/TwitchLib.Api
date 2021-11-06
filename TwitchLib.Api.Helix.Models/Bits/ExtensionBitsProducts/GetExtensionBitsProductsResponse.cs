using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Bits.ExtensionBitsProducts
{
    public class GetExtensionBitsProductsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ExtensionBitsProduct[] Data { get; protected set; }
    }
}
