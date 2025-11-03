#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Subscriptions;

/// <summary>
/// Get user subscription response object.
/// </summary>
public class CheckUserSubscriptionResponse
{
    /// <summary>
    /// A list that contains a single object with information about the user’s subscription.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Subscription[] Data { get; protected set; }
}