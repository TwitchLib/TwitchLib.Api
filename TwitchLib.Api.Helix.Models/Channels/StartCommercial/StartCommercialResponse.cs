using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.StartCommercial;

/// <summary>
/// The response for starting a commercial on a specified channel.
/// </summary>
public class StartCommercialResponse
{
  /// <summary>
  /// The length of the commercial you requested. 
  /// If you request a commercial that’s longer than 180 seconds, the API uses 180 seconds.
  /// </summary>
  [JsonProperty(PropertyName = "length")]
  public int Length { get; protected set; }

  /// <summary>
  /// A message that indicates whether Twitch was able to serve an ad.
  /// </summary>
  [JsonProperty(PropertyName = "message")]
  public string Message { get; protected set; }

  /// <summary>
  /// The number of seconds you must wait before running another commercial.
  /// </summary>
  [JsonProperty(PropertyName = "retry_after")]
  public int RetryAfter { get; protected set; }
}
