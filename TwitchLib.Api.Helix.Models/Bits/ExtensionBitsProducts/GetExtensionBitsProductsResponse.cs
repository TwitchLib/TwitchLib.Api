#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits.ExtensionBitsProducts;

/// <summary>
/// List of Bits products that belongs to the extension response object.
/// </summary>
public class GetExtensionBitsProductsResponse
{
    /// <summary>
    /// A list of Bits products that the extension created. The list is in ascending SKU order. 
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public ExtensionBitsProduct[] Data { get; protected set; }
}
