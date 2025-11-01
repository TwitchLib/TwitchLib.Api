using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.GuestStar.GetGuestStarSession;

/// <summary>
/// Get guest star session response object.
/// </summary>
public class GetGuestStarSessionResponse
{
    /// <summary>
    /// <para>A list that contains the channels guest star sessions</para>
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public GuestStarSession[] Data { get; protected set; }
}