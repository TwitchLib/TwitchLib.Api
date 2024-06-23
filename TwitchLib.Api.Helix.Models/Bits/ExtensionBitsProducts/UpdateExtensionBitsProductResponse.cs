using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits.ExtensionBitsProducts;

/// <summary>
/// Adds or updates a Bits product that the extension created response object.
/// </summary>
public class UpdateExtensionBitsProductResponse
{
    /// <summary>
    /// A list of Bits products that the extension created. The list is in ascending SKU order.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public ExtensionBitsProduct[] Data { get; protected set; }
}
