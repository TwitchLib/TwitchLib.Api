using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Teams
{
    public class Team : TeamBase
    {
        [JsonProperty(PropertyName = "users")]
        public TeamMember[] Users { get; protected set; }
    }
}