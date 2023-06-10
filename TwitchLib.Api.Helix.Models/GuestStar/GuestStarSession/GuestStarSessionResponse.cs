using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.GuestStar.GetChannelGuestStarSettings
{
    public class GuestStarSessionResponse
    {
        [JsonProperty(PropertyName = "data")]
        public GuestStarSession[] Data { get; protected set; }
    }
}