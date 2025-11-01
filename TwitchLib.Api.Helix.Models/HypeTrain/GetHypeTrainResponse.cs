using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.HypeTrain;

/// <summary>
/// Get hype train response object.
/// </summary>
public class GetHypeTrainResponse
{
    /// <summary>
    /// The list of Hype Train events. The list is empty if the broadcaster hasn’t run a Hype Train within the last 5 days.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public HypeTrain[] HypeTrain { get; protected set; }

    /// <summary>
    /// Contains the information used to page through the list of results.
    /// </summary>
    [JsonProperty(PropertyName = "pagination")]
    public Pagination Pagination { get; protected set; }
}