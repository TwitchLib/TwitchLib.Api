using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Subscriptions;

/// <summary>
/// Get user subscriptions response object.
/// </summary>
public class GetUserSubscriptionsResponse
{
    /// <summary>
    /// The list of subscriptions for the user.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Subscription[] Data { get; protected set; }
}