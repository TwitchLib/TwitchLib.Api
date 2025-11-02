#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreamMarkers;

/// <summary>
/// A marker grouped by the user that created the marks.
/// </summary>
public class UserMarkerListing
{
    /// <summary>
    /// The ID of the user that created the marker.
    /// </summary>
    [JsonProperty(PropertyName = "user_id")]
    public string UserId { get; protected set; }

    /// <summary>
    /// The user’s display name.
    /// </summary>
    [JsonProperty(PropertyName = "user_name")]
    public string UserName { get; protected set; }

    /// <summary>
    /// The user’s login name.
    /// </summary>
    [JsonProperty(PropertyName = "user_login")]
    public string UserLogin { get; protected set; }

    /// <summary>
    /// A list of videos that contain marker.
    /// </summary>
    [JsonProperty(PropertyName = "videos")]
    public Video[] Videos { get; protected set; }
}
