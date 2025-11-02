#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Teams;

/// <summary>
/// A Team.
/// </summary>
public class Team : TeamBase
{
    /// <summary>
    /// The list of team members.
    /// </summary>
    [JsonProperty(PropertyName = "users")]
    public TeamMember[] Users { get; protected set; }
}