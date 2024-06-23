using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Goals;

/// <summary>
/// Get creator goals response object.
/// </summary>
public class GetCreatorGoalsResponse
{
    /// <summary>
    /// The list of goals.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public CreatorGoal[] Data { get; protected set; }
}
