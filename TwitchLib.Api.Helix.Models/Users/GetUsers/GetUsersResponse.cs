#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.GetUsers;

/// <summary>
/// Get users response object.
/// </summary>
public class GetUsersResponse
{
    /// <summary>
    /// The list of users.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public User[] Users { get; protected set; }
}
