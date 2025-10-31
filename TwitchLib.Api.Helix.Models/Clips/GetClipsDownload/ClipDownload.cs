using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

#nullable enable

namespace TwitchLib.Api.Helix.Models.Clips.GetClipsDownload;

/// <summary>
/// A clip that was created from a broadcaster's stream.
/// </summary>
public class ClipDownload
{
    /// <summary>
    /// An ID that uniquely identifies the clip.
    /// </summary>
    [JsonProperty(PropertyName = "clip_id")]
    public string ClipId { get; protected set; } = default!;

    /// <summary>
    /// The landscape URL to download the clip. This field is null if the URL is not available.
    /// </summary>
    [JsonProperty(PropertyName = "landscape_download_url")]
    public string? LandscapeDownloadUrl { get; protected set; }

    /// <summary>
    /// The portrait URL to download the clip. This field is null if the URL is not available.
    /// </summary>
    [JsonProperty(PropertyName = "portrait_download_url")]
    public string? PortraitDownloadUrl { get; protected set; }

    /// <summary>
    /// Indicates whether both download URLs are available.
    /// </summary>

#if NETCOREAPP
    [MemberNotNullWhen(true, nameof(LandscapeDownloadUrl), nameof(PortraitDownloadUrl))]
#endif
    [JsonIgnore]
    public bool IsDownloadUrlAvailable 
        => !string.IsNullOrWhiteSpace(LandscapeDownloadUrl) && !string.IsNullOrWhiteSpace(PortraitDownloadUrl);
}
