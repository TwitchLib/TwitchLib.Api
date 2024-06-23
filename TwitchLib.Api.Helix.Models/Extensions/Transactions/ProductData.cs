using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.Transactions;

/// <summary>
/// Contains details about the digital product.
/// </summary>
public class ProductData
{
    /// <summary>
    /// Set to twitch.ext. + the extension's ID.
    /// </summary>
    [JsonProperty(PropertyName = "domain")]
    public string Domain { get; protected set; }

    /// <summary>
    /// An ID that identifies the digital product.
    /// </summary>
    [JsonProperty(PropertyName = "sku")]
    public string SKU { get; protected set; }

    /// <summary>
    /// Contains details about the digital product’s cost.
    /// </summary>
    [JsonProperty(PropertyName = "cost")]
    public Cost Cost { get; protected set; }

    /// <summary>
    /// A Boolean value that determines whether the product is in development.
    /// </summary>
    [JsonProperty(PropertyName = "inDevelopment")]
    public bool InDevelopment { get; protected set; }

    /// <summary>
    /// The name of the digital product.
    /// </summary>
    [JsonProperty(PropertyName = "displayName")]
    public string DisplayName { get; protected set; }

    /// <summary>
    /// This field is always empty since you may purchase only unexpired products.
    /// </summary>
    [JsonProperty(PropertyName = "expiration")]
    public string Expiration { get; protected set; }

    /// <summary>
    /// A Boolean value that determines whether the data was broadcast to all instances of the extension.
    /// </summary>
    [JsonProperty(PropertyName = "broadcast")]
    public bool Broadcast { get; protected set; }
}
