#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Clips.GetClipsDownload;

/// <summary>
/// Response for GetClipsDownload.
/// </summary>
public class GetClipsDownloadResponse
{
    /// <summary>
    /// List of clips and their download URLs.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public ClipDownload[] Clips { get; protected set; }
}
