using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.GetModerators
{
    public class Moderator
    {
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "user_login")]
        public string UserLogin { get; protected set; }
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; protected set; }
    }
}
