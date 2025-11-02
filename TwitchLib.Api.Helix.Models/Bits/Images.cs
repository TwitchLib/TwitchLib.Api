#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits;

/// <summary>
/// <para>The dark and light themes of a Cheermote.</para>
/// <para>Each theme has static and animated formats of the Cheermote. Each format has a dictionary
/// containing multiple image sizes and associated image URLs.</para>
///</summary>
public class Images
{
  /// <summary>
  /// <para>Dark theme key for a Cheermote.</para>
  /// <para>Contains the static and animated formats for the Cheermote. 
  /// Each format has a dictionary containing multiple image sizes and associated image URLs.</para>
  /// </summary>
  [JsonProperty(PropertyName = "dark")]
  public ImageList Dark { get; protected set; }

  /// <summary>
  /// <para>Light theme key for a Cheermote.</para>
  /// <para>Contains the static and animated formats for the Cheermote. 
  /// Each format has a dictionary containing multiple image sizes and associated image URLs.</para>
  /// </summary>
  [JsonProperty(PropertyName = "light")]
  public ImageList Light { get; protected set; }
}
