#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints;

/// <summary>
/// The reward that the user redeemed.
/// </summary>
public class Reward
{
  /// <summary>
  /// The ID that uniquely identifies the redeemed reward.
  /// </summary>
  [JsonProperty(PropertyName = "id")]
  public string Id { get; protected set; }

  /// <summary>
  /// The reward’s title.
  /// </summary>
  [JsonProperty(PropertyName = "title")]
  public string Title { get; protected set; }

  /// <summary>
  /// The prompt displayed to the viewer if user input is required.
  /// </summary>
  [JsonProperty(PropertyName = "prompt")]
  public string Prompt { get; protected set; }

  /// <summary>
  /// The reward’s cost, in Channel Points.
  /// </summary>
  [JsonProperty(PropertyName = "cost")]
  public int Cost { get; protected set; }
}
