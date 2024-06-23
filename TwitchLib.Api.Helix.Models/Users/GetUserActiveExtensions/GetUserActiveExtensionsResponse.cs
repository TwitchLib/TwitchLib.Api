using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Users.Internal;

namespace TwitchLib.Api.Helix.Models.Users.GetUserActiveExtensions;

/// <summary>
/// Get user active extensions response object.
/// </summary>
public class GetUserActiveExtensionsResponse
{
    /// <summary>
    /// The active extensions that the broadcaster has installed.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public ActiveExtensions Data { get; protected set; }
}
