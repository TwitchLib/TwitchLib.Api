using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.BanUser;

/// <summary>
/// Ban user response object.
/// </summary>
public class BanUserResponse
{
    /// <summary>
    /// Identifies the user and type of ban.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public BannedUser[] Data { get; protected set; }
}
