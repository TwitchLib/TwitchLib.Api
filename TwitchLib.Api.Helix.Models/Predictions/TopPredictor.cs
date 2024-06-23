using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Predictions;

/// <summary>
/// A viewer who was the top predictor
/// </summary>
public class TopPredictor
{
    /// <summary>
    /// An ID that identifies the viewer.
    /// </summary>
    [JsonProperty(PropertyName = "user_id")]
    public string UserId { get; protected set; }

    /// <summary>
    /// The viewer’s display name.
    /// </summary>
    [JsonProperty(PropertyName = "user_name")]
    public string UserName { get; protected set; }

    /// <summary>
    /// The viewer’s login name.
    /// </summary>
    [JsonProperty(PropertyName = "user_login")]
    public string UserLogin { get; protected set; }

    /// <summary>
    /// The number of Channel Points the viewer spent.
    /// </summary>
    [JsonProperty(PropertyName = "channel_points_used")]
    public int ChannelPointsUsed { get; protected set; }

    /// <summary>
    /// The number of Channel Points distributed to the viewer.
    /// </summary>
    [JsonProperty(PropertyName = "channel_points_won")]
    public int ChannelPointsWon { get; protected set; }
}
