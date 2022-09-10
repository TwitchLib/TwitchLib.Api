using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus.Request
{
    public class MessageRequest
    {
        [JsonProperty(PropertyName = "data")]
        public Message[] Messages { get; set; }
    }
}
