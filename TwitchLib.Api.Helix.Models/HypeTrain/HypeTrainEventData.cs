using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.HypeTrain;

/// <summary>
/// The event’s data.
/// </summary>
public class HypeTrainEventData
{
    /// <summary>
    /// An ID that identifies this Hype Train.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; protected set; }

    /// <summary>
    /// The ID of the broadcaster that’s running the Hype Train.
    /// </summary>
    [JsonProperty(PropertyName = "broadcaster_id")]
    public string BroadcasterId { get; protected set; }

    /// <summary>
    /// The UTC date and time (in RFC3339 format) that this Hype Train started.
    /// </summary>
    [JsonProperty(PropertyName = "started_at")]
    public string StartedAt { get; protected set; }

    /// <summary>
    /// The UTC date and time (in RFC3339 format) that the Hype Train ends.
    /// </summary>
    [JsonProperty(PropertyName = "expires_at")]
    public string ExpiresAt { get; protected set; }

    /// <summary>
    /// The UTC date and time (in RFC3339 format) that another Hype Train can start.
    /// </summary>
    [JsonProperty(PropertyName = "cooldown_end_time")]
    public string CooldownEndTime { get; protected set; }

    /// <summary>
    /// The highest level that the Hype Train reached.
    /// </summary>
    [JsonProperty(PropertyName = "level")]
    public int Level { get; protected set; }

    /// <summary>
    /// The value needed to reach the next level.
    /// </summary>
    [JsonProperty(PropertyName = "goal")]
    public int Goal { get; protected set; }

    /// <summary>
    /// The current total amount raised.
    /// </summary>
    [JsonProperty(PropertyName = "total")]
    public int Total { get; protected set; }

    /// <summary>
    /// The top contributors for each contribution type. 
    /// For example, the top contributor using BITS (by aggregate) and the top contributor using SUBS (by count).
    /// </summary>
    [JsonProperty(PropertyName = "top_contribution")]
    public HypeTrainContribution[] TopContribution { get; protected set; }

    /// <summary>
    /// The most recent contribution towards the Hype Train’s goal.
    /// </summary>
    [JsonProperty(PropertyName = "last_contribution")]
    public HypeTrainContribution LastContribution { get; protected set; }
}