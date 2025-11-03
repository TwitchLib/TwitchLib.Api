#nullable disable
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.Channels.ModifyChannelInformation;

/// <summary>
/// <para>Labels that can be set for the channel's Content Classification Labels</para>
/// </summary>
public class ContentClassificationLabel
{
  /// <summary>
  /// <para>ID of the Content Classification Labels that must be added/removed from the channel.</para>
  /// <para>Can be one of the following values:
  /// DrugsIntoxication, SexualThemes, ViolentGraphic, Gambling, ProfanityVulgarity</para>
  /// </summary>
  [JsonConverter(typeof(StringEnumConverter))]
  [JsonProperty(PropertyName = "id")]
  public ContentClassificationLabelEnum Id { get; set; }

  /// <summary>
  /// <para>Boolean flag indicating whether the label should be enabled (true) or disabled (false) for the channel.</para>
  /// </summary>
  [JsonProperty(PropertyName = "is_enabled")]
  public bool IsEnabled { get; set; }
}
