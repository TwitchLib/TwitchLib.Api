using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.GetUserBlockList
{
    public class BlockedUser
    {
        [JsonProperty(PropertyName = "user_id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "user_login")]
        public string UserLogin { get; protected set; }
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; protected set; }
    }
}
