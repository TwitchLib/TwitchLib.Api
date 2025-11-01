using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints;

/// <summary>
/// A set of custom images for the reward. This field is null if the broadcaster didn’t upload images.
/// </summary>
public class Image
{
  /// <summary>
  /// The URL to a small version of the image.
  /// </summary>
  [JsonProperty(PropertyName = "url_1x")]
  public string Url1x { get; protected set; }

  /// <summary>
  /// The URL to a medium version of the image.
  /// </summary>
  [JsonProperty(PropertyName = "url_2x")]
  public string Url2x { get; protected set; }

  /// <summary>
  /// The URL to a large version of the image.
  /// </summary>
  [JsonProperty(PropertyName = "url_4x")]
  public string Url4x { get; protected set; }
}
