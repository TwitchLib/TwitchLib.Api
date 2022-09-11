using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Polls.EndPoll
{
    public class EndPollResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Poll[] Data { get; protected set; }
    }
}
