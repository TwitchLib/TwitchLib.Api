#nullable disable
using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Goals;

/// <summary>
/// A goal.
/// </summary>
public class CreatorGoal
{
    /// <summary>
    /// An ID that identifies this goal.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; protected set; }

    /// <summary>
    /// An ID that identifies the broadcaster that created the goal.
    /// </summary>
    [JsonProperty(PropertyName = "broadcaster_id")]
    public string BroadcasterId { get; protected set; }

    /// <summary>
    /// The broadcaster’s display name.
    /// </summary>
    [JsonProperty(PropertyName = "broadcaster_name")]
    public string BroadcasterName { get; protected set; }

    /// <summary>
    /// The broadcaster’s login name.
    /// </summary>
    [JsonProperty(PropertyName = "broadcaster_login")]
    public string BroadcasterLogin { get; protected set; }

    /// <summary>
    /// The type of goal.
    /// </summary>
    [JsonProperty(PropertyName = "type")]
    public string Type { get; protected set; }

    /// <summary>
    /// A description of the goal.
    /// </summary>
    [JsonProperty(PropertyName = "description")]
    public string Description { get; protected set; }

    /// <summary>
    /// The goal’s current value.
    /// </summary>
    [JsonProperty(PropertyName = "current_amount")]
    public int CurrentAmount { get; protected set; }

    /// <summary>
    /// The goal’s target value.
    /// </summary>
    [JsonProperty(PropertyName = "target_amount")]
    public int TargetAmount { get; protected set; }

    /// <summary>
    /// The UTC date and time (in RFC3339 format) that the broadcaster created the goal.
    /// </summary>
    [JsonProperty(PropertyName = "created_at")]
    public DateTime CreatedAt { get; protected set; }
}
