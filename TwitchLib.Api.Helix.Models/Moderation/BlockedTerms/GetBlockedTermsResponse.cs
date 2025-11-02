#nullable disable
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Moderation.BlockedTerms;

/// <summary>
/// Get blocked terms response object.
/// </summary>
public class GetBlockedTermsResponse
{
    /// <summary>
    /// The list of blocked terms. 
    /// The list is in descending order of when they were created.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public BlockedTerm[] Data { get; protected set; }

    /// <summary>
    /// Contains the information used to page through the list of results.
    /// </summary>
    [JsonProperty(PropertyName = "pagination")]
    public Pagination Pagination { get; protected set; }
}
