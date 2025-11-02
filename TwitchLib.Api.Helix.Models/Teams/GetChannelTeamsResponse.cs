#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Teams;

/// <summary>
/// Get channel teams response object.
/// </summary>
public class GetChannelTeamsResponse
{
    /// <summary>
    /// The list of teams that the broadcaster is a member of.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public ChannelTeam[] ChannelTeams { get; protected set; }
}