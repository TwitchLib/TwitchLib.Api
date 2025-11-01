using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.CreateStreamMarker;

/// <summary>
/// Create stream marker request object.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class CreateStreamMarkerRequest
{
    /// <summary>
    /// The ID of the broadcaster that’s streaming content.
    /// This ID must match the user ID in the access token or the user in the access token must be one of the broadcaster’s editors.
    /// </summary>
    [JsonProperty(PropertyName = "user_id")]
    public string UserId { get; set; }

    /// <summary>
    /// A short description of the marker to help the user remember why they marked the location.
    /// The maximum length of the description is 140 characters.
    /// </summary>
    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }
}
