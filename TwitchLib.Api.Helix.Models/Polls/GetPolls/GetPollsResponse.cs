using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Polls.GetPolls;

/// <summary>
/// Get polls response object.
/// </summary>
public class GetPollsResponse
{
    /// <summary>
    /// A list of polls.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Poll[] Data { get; protected set; }

    /// <summary>
    /// Contains the information used to page through the list of results.
    /// </summary>
    [JsonProperty(PropertyName = "pagination")]
    public Pagination Pagination { get; protected set; }
}
