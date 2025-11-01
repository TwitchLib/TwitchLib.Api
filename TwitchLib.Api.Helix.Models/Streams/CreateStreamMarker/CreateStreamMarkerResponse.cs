using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.CreateStreamMarker;

/// <summary>
/// Create stream marker response object.
/// </summary>
public class CreateStreamMarkerResponse
{
    /// <summary>
    /// A list that contains the single marker that you added.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public CreatedMarker[] Marker { get; protected set; }
}
