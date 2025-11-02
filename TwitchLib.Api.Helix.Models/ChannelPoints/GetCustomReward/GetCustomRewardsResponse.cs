#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.GetCustomReward;

/// <summary>
/// The response for getting a list of custom rewards that the specified broadcaster created.
/// </summary>
public class GetCustomRewardsResponse
{
  /// <summary>
  /// A list of custom rewards. The list is in ascending order by id. If the broadcaster hasn’t created custom rewards, the list is empty.
  /// </summary>
  [JsonProperty(PropertyName = "data")]
  public CustomReward[] Data { get; protected set; }
}
