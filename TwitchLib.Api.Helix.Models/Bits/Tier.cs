using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits;

/// <summary>
/// <para>A list of tier levels that the Cheermote supports.</para> 
/// <para>Each tier identifies the range of Bits that you can cheer at that tier level and an image that graphically identifies the tier level.</para>
/// </summary>
public class Tier
{
  /// <summary>
  /// <para>The minimum number of Bits that you must cheer at this tier level.</para>
  /// <para>The maximum number of Bits that you can cheer at this level is determined by the required minimum Bits of the next tier level minus 1. </para>
  /// <para>For example, if min_bits is 1 and min_bits for the next tier is 100, the Bits range for this tier level is 1 through 99. </para>
  /// The minimum Bits value of the last tier is the maximum number of Bits you can cheer using this Cheermote. For example, 10000.
  /// </summary>
  [JsonProperty(PropertyName = "min_bits")]
  public int MinBits { get; protected set; }

  /// <summary>
  /// The tier level. Possible tiers are:<br/>
  /// 1 | 100 | 500 | 1,000 | 5,000 | 10,000 | 100,000
  /// </summary>
  [JsonProperty(PropertyName = "id")]
  public string Id { get; protected set; }

  /// <summary>
  /// The hex code of the color associated with this tier level (for example, #979797).
  /// </summary>
  [JsonProperty(PropertyName = "color")]
  public string Color { get; protected set; }

  /// <summary>
  /// <para>The animated and static image sets for the Cheermote - organized by theme, format, and size. </para>
  /// The theme keys are dark and light. <br/>
  /// Each theme is a dictionary of formats: animated and static. <br/>
  /// Each format is a dictionary with the following sizes:  1, 1.5, 2, 3, and 4. <br/>
  /// The value of each size contains the URL to the image.
  /// </summary>
  [JsonProperty(PropertyName = "images")]
  public Images Images { get; protected set; }

  /// <summary>
  /// A Boolean value that determines whether users can cheer at this tier level.
  /// </summary>
  [JsonProperty(PropertyName = "can_cheer")]
  public bool CanCheer { get; protected set; }

  /// <summary>
  /// <para>A Boolean value that determines whether this tier level is shown in the Bits card. </para>
  /// <para>Is true if this tier level is shown in the Bits card.</para>
  /// </summary>
  [JsonProperty(PropertyName = "show_in_bits_card")]
  public bool ShowInBitsCard { get; protected set; }
}
