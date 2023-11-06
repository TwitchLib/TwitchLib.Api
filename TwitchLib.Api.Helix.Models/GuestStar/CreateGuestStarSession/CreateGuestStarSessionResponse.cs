using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.GuestStar.CreateGuestStarSession;

public class CreateGuestStarSessionResponse
{
    /// <summary>
    /// <para>Summary of the session details.</para>
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public GuestStarSession[] Data { get; protected set; }
}