#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.UnbanRequests.ResolveUnbanRequests;

/// <summary>
/// Resolve unban requests response object.
/// </summary>
public class ResolveUnbanRequestsResponse
{
    /// <summary>
    /// Contains information about the channel's unban request.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public UnbanRequest[] Data { get; protected set; }
}
