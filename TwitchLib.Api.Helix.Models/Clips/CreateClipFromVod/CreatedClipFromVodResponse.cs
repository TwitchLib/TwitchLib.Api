using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Clips.CreateClip;

namespace TwitchLib.Api.Helix.Models.Clips.CreateClipFromVod;

/// <summary>
/// Response for CreateClipFromVod which creates a clip from the broadcaster's stream.
/// </summary>
public class CreatedClipFromVodResponse
{
    /// <summary>
    /// A list containing the created clip.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public CreatedClip[] Data { get; protected set; } = null!;
}
