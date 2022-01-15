using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.BanUser
{
    public class BanUserResponse
    {
        [JsonProperty(PropertyName = "data")]
        public BannedUser[] Data { get; protected set; }
    }
}
