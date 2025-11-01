using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions;

/// <summary>
/// Get released extensions response object.
/// </summary>
public class GetReleasedExtensionsResponse
{
    /// <summary>
    /// A list that contains the specified extension.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public ReleasedExtension[] Data { get; protected set; }
}
