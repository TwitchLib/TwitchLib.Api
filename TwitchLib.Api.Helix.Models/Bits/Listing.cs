using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits
{
    public class Listing
    {
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "user_login")]
        public string UserLogin { get; protected set; }
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; protected set; }
        [JsonProperty(PropertyName = "rank")]
        public int Rank { get; protected set; }
        [JsonProperty(PropertyName = "score")]
        public int Score { get; protected set; }
    }
}
