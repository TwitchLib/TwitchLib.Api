using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits;

/// <summary>
/// Bits leaderboard for the authenticated broadcaster response object.
/// </summary>
public class GetBitsLeaderboardResponse
{
    /// <summary>
    /// A list of leaderboard leaders. The leaders are returned in rank order by how much they’ve cheered.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Listing[] Listings { get; protected set; }

    /// <summary>
    /// The reporting window’s start and end dates.
    /// </summary>
    [JsonProperty(PropertyName = "date_range")]
    public DateRange DateRange { get; protected set; }

    /// <summary>
    /// The number of ranked users in data.
    /// </summary>
    [JsonProperty(PropertyName = "total")]
    public int Total { get; protected set; }
}
