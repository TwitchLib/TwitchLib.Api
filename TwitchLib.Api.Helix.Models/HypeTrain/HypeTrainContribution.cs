#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.HypeTrain;

/// <summary>
/// The contribution towards the Hype Train’s goal.
/// </summary>
public class HypeTrainContribution
{
    /// <summary>
    /// The total amount contributed. If type is BITS, total represents the amount of Bits used.
    /// If type is SUBS, total is 500, 1000, or 2500 to represent tier 1, 2, or 3 subscriptions, respectively.
    /// </summary>
    [JsonProperty(PropertyName = "total")]
    public int Total { get; protected set; }

    /// <summary>
    /// The contribution method used.
    /// </summary>
    [JsonProperty(PropertyName = "type")]
    public string Type { get; protected set; }

    /// <summary>
    /// The ID of the user that made the contribution.
    /// </summary>
    [JsonProperty(PropertyName = "user")]
    public string UserId { get; protected set; }
}