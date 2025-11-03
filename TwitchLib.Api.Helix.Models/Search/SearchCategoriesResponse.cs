#nullable disable
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Search;

/// <summary>
/// Search categories response object.
/// </summary>
public class SearchCategoriesResponse
{
    /// <summary>
    /// The list of games or categories that match the query. The list is empty if there are no matches.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Game[] Games { get; protected set; }

    /// <summary>
    /// The information used to page through the list of results.
    /// </summary>
    [JsonProperty(PropertyName = "pagination")]
    public Pagination Pagination { get; protected set; }
}
