using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomReward;

/// <summary>
/// The response for updating a custom reward.
/// </summary>
public class UpdateCustomRewardResponse
{
  /// <summary>
  /// The list contains the single reward that you updated.
  /// </summary>
  [JsonProperty(PropertyName = "data")]
  public CustomReward[] Data { get; protected set; }
}
