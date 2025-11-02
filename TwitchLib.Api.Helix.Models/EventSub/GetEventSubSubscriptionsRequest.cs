#nullable disable
#nullable enable
namespace TwitchLib.Api.Helix.Models.EventSub;

/// <summary>
/// A class to represent the request query data for a <see href="https://dev.twitch.tv/docs/api/reference/#get-eventsub-subscriptions">Get EventSub Subscriptions</see> request.
/// </summary>
public class GetEventSubSubscriptionsRequest {
    /// <summary>
    /// Filter subscriptions by its status.
    /// </summary>
    public string? Status { get; set; }
    
    /// <summary>
    /// Filter subscriptions by subscription type (e.g., channel.update).
    /// </summary>
    public string? Type { get; set; }
    
    /// <summary>
    /// Filter subscriptions by user ID.
    /// </summary>
    public string? UserId { get; set; }
    
    /// <summary>
    /// Filter subscriptions to the one with the provided ID (as long as it is owned by the client making the request).
    /// </summary>
    public string? SubscriptionId { get; set; }
    
    /// <summary>
    /// The cursor used to get the next page of results.
    /// </summary>
    public string? After { get; set; }
}