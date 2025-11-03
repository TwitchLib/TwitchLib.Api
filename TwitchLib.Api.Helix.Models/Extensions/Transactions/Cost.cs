#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.Transactions;

/// <summary>
/// Details about the digital product’s cost.
/// </summary>
public class Cost
{
    /// <summary>
    /// The amount exchanged for the digital product.
    /// </summary>
    [JsonProperty(PropertyName = "amount")]
    public int Amount { get; protected set; }

    /// <summary>
    /// The type of currency exchanged. 
    /// </summary>
    [JsonProperty(PropertyName = "type")]
    public string Type { get; protected set; }
}
