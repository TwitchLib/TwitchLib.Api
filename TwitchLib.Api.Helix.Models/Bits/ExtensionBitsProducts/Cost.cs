#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits.ExtensionBitsProducts;

/// <summary>
/// An object that contains the product’s cost information.
/// </summary>
public class Cost
{
    /// <summary>
    /// The product’s price.
    /// </summary>
    [JsonProperty(PropertyName = "amount")]
    public int Amount { get; protected set; }

    /// <summary>
    /// The type of currency.
    /// </summary>
    [JsonProperty(PropertyName = "type")]
    public string Type { get; protected set; }
}
