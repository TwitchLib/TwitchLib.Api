using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Analytics;

/// <summary>
/// <para></para>
/// </summary>
public class GameAnalytics
{
  /// <summary>
  /// <para>An ID that identifies the game that the analytic report was generated for.</para>
  /// </summary>
  [JsonProperty(PropertyName = "game_id")]
  public string GameId { get; protected set; }

  /// <summary>
  /// <para>The URL that you use to download the analytic report.</para>
  /// <para><b>The URL is valid for 5 minutes.</b></para>
  /// </summary>
  [JsonProperty(PropertyName = "URL")]
  public string Url { get; protected set; }

  /// <summary>
  /// <para>The type of analytic report.</para>
  /// </summary>
  [JsonProperty(PropertyName = "type")]
  public string Type { get; protected set; }

  /// <summary>
  /// <para>The reporting period’s start and end dates.</para>
  /// </summary>
  [JsonProperty(PropertyName = "date_range")]
  public DateRange DateRange { get; protected set; }
}
