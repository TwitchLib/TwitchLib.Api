using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Teams;

/// <summary>
/// A team member.
/// </summary>
public class TeamMember
{
    /// <summary>
    /// An ID that identifies the team member.
    /// </summary>
    [JsonProperty(PropertyName = "user_id")]
    public string UserId { get; protected set; }

    /// <summary>
    /// The team member’s display name.
    /// </summary>
    [JsonProperty(PropertyName = "user_name")]
    public string UserName { get; protected set; }

    /// <summary>
    /// The team member’s login name.
    /// </summary>
    [JsonProperty(PropertyName = "user_login")]
    public string UserLogin { get; protected set; }
}