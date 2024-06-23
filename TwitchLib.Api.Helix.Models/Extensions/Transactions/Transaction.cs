using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.Transactions;

/// <summary>
/// The transactions.
/// </summary>
public class Transaction
{
    /// <summary>
    /// An ID that identifies the transaction.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; protected set; }

    /// <summary>
    /// The UTC date and time (in RFC3339 format) of the transaction.
    /// </summary>
    [JsonProperty(PropertyName = "timestamp")]
    public DateTime Timestamp { get; protected set; }

    /// <summary>
    /// The ID of the broadcaster that owns the channel where the transaction occurred.
    /// </summary>
    [JsonProperty(PropertyName = "broadcaster_id")]
    public string BroadcasterId { get; protected set; }

    /// <summary>
    /// The broadcaster’s login name.
    /// </summary>
    [JsonProperty(PropertyName = "broadcaster_login")]
    public string BroadcasterLogin { get; protected set; }

    /// <summary>
    /// The broadcaster’s display name.
    /// </summary>
    [JsonProperty(PropertyName = "broadcaster_name")]
    public string BroadcasterName { get; protected set; }

    /// <summary>
    /// The ID of the user that purchased the digital product.
    /// </summary>
    [JsonProperty(PropertyName = "user_id")]
    public string UserId { get; protected set; }

    /// <summary>
    /// The user’s login name.
    /// </summary>
    [JsonProperty(PropertyName = "user_login")]
    public string UserLogin { get; protected set; }

    /// <summary>
    /// The user’s display name.
    /// </summary>
    [JsonProperty(PropertyName = "user_name")]
    public string UserName { get; protected set; }

    /// <summary>
    /// The type of transaction.
    /// </summary>
    [JsonProperty(PropertyName = "product_type")]
    public string ProductType { get; protected set; }

    /// <summary>
    /// Contains details about the digital product.
    /// </summary>
    [JsonProperty(PropertyName = "product_data")]
    public ProductData ProductData { get; protected set; }
}
