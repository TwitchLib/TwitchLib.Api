#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Clips.CreateClip;

/// <summary>
/// Response for CreateClip which creates a clip from the broadcaster's stream.
/// </summary>
public class CreatedClipResponse
{
  /// <summary>
  /// Contains clip's ID and edit_URL that can be used to edit the clip's title, identify the part of the clip to publish, and publish the clip.
  /// </summary>
  [JsonProperty(PropertyName = "data")]
  public CreatedClip[] CreatedClips { get; protected set; }
}
