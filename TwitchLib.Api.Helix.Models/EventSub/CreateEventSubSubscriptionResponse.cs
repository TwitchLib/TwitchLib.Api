using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub;

/// <summary>
/// Create an EventSub subscription resposne object.
/// </summary>
public class CreateEventSubSubscriptionResponse
{
    /// <summary>
    /// A list that contains the single subscription that you created.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public EventSubSubscription[] Subscriptions { get; protected set; }

    /// <summary>
    /// The total number of subscriptions you’ve created.
    /// </summary>
    [JsonProperty(PropertyName = "total")]
    public int Total { get; protected set; }

    /// <summary>
    /// The sum of all of your subscription costs. 
    /// </summary>
    [JsonProperty(PropertyName = "total_cost")]
    public int TotalCost { get; protected set; }

    /// <summary>
    /// The maximum total cost that you’re allowed to incur for all subscriptions you create.
    /// </summary>
    [JsonProperty(PropertyName = "max_total_cost")]
    public int MaxTotalCost { get; protected set; }
}