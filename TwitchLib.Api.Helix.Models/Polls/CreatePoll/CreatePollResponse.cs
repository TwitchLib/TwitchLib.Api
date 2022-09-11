using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Polls.CreatePoll
{
    public class CreatePollResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Poll[] Data { get; protected set; }
    }
}
