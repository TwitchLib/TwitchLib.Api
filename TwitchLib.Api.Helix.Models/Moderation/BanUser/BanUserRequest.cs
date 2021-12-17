using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.BanUser
{
    public class BanUserRequest
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("reason")]
        public string Reason { get; set; }
        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        public int? Duration { get; set; }
    }
}