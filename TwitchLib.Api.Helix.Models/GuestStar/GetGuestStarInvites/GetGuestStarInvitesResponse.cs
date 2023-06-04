using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.GuestStar.GetChannelGuestStarSettings
{
    public class GetGuestStarInvitesResponse
    {
        [JsonProperty(PropertyName = "data")]
        public GuestStarInvite[] Data { get; protected set; }
    }
}