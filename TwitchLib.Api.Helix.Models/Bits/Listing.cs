#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits;

/// <summary>
/// A listing in the list of bit leadboard leaders.
/// </summary>
public class Listing
{
    /// <summary>
    /// An ID that identifies a user on the leaderboard.
    /// </summary>
    [JsonProperty(PropertyName = "user_id")]
    public string UserId { get; protected set; }

    /// <summary>
    /// The user’s login name.
    /// </summary>
    [JsonProperty(PropertyName = "user_login")]
    public string UserLogin { get; protected set; }

    /// <summary>
    /// The user’s display name.
    /// </summary>
    [JsonProperty(PropertyName = "user_name")]
    public string UserName { get; protected set; }

    /// <summary>
    /// The user’s position on the leaderboard.
    /// </summary>
    [JsonProperty(PropertyName = "rank")]
    public int Rank { get; protected set; }

    /// <summary>
    /// The number of Bits the user has cheered.
    /// </summary>
    [JsonProperty(PropertyName = "score")]
    public int Score { get; protected set; }
}
