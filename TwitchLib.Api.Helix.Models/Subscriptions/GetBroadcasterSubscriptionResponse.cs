using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Subscriptions;

/// <summary>
/// Get broadcaster subsriptions response object.
/// </summary>
public class GetBroadcasterSubscriptionsResponse
{
    /// <summary>
    /// The list of users that subscribe to the broadcaster.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Subscription[] Data { get; protected set; }

    /// <summary>
    /// Contains the information used to page through the list of results.
    /// </summary>
    [JsonProperty(PropertyName = "pagination")]
    public Pagination Pagination { get; protected set; }

    /// <summary>
    /// The total number of users that subscribe to this broadcaster.
    /// </summary>
    [JsonProperty(PropertyName = "total")]
    public int Total { get; protected set; }

    /// <summary>
    /// The current number of subscriber points earned by this broadcaster.
    /// </summary>
    [JsonProperty(PropertyName = "points")]
    public int Points { get; protected set; }
}