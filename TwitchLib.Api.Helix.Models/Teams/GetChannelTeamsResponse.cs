using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Teams
{
    public class GetChannelTeamsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ChannelTeam[] ChannelTeams { get; protected set; }
    }
}