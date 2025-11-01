using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Teams;

/// <summary>
/// Get teams response object.
/// </summary>
public class GetTeamsResponse
{
    /// <summary>
    /// A list that contains the single team that you requested.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Team[] Teams { get; protected set; }
}