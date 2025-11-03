#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.GetUserBlockList;

/// <summary>
/// A block user.
/// </summary>
public class BlockedUser
{
    /// <summary>
    /// An ID that identifies the blocked user.
    /// </summary>
    [JsonProperty(PropertyName = "user_id")]
    public string Id { get; protected set; }

    /// <summary>
    /// The blocked user’s login name.
    /// </summary>
    [JsonProperty(PropertyName = "user_login")]
    public string UserLogin { get; protected set; }

    /// <summary>
    /// The blocked user’s display name.
    /// </summary>
    [JsonProperty(PropertyName = "display_name")]
    public string DisplayName { get; protected set; }
}
