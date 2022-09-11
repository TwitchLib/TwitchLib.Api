using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Polls.CreatePoll
{
    public class Choice
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}
