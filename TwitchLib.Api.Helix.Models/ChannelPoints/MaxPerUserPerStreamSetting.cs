using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints;

/// <summary>
/// The settings used to determine whether to apply a maximum to the number of redemptions allowed per user per live stream.
/// </summary>
public class MaxPerUserPerStreamSetting
{
  /// <summary>
  /// A Boolean value that determines whether the reward applies a limit on the number of redemptions allowed per user per live stream. Is true if the reward applies a limit.
  /// </summary>
  [JsonProperty(PropertyName = "is_enabled")]
  public bool IsEnabled { get; protected set; }

  /// <summary>
  /// The maximum number of redemptions allowed per user per live stream.
  /// </summary>
  [JsonProperty(PropertyName = "max_per_user_per_stream")]
  public int MaxPerUserPerStream { get; protected set; }
}
