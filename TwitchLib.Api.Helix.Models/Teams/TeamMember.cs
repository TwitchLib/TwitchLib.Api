using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Teams
{
    public class TeamMember
    {
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; protected set; }
        [JsonProperty(PropertyName = "user_login")]
        public string UserLogin { get; protected set; }
    }
}