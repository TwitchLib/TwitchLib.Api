using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.GuestStar;

public class GuestStarSession
{
    /// <summary>
    /// ID uniquely representing the Guest Star session.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; protected set; }
    
    /// <summary>
    /// List of guests currently interacting with the Guest Star session.
    /// </summary>
    [JsonProperty(PropertyName = "guests")]
    public GuestStarGuest[] Guests { get; protected set; }
}