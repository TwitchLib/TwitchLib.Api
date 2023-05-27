using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.GuestStar.GetChannelGuestStarSettings
{
    public class GetChannelGuestStarSettingsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public GuestStarSettings[] Data { get; protected set; }
    }
}
