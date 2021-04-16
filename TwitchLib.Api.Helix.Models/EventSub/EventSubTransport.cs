using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub
{
    public class EventSubTransport
    {
        [JsonProperty(PropertyName = "method")]
        public string Method { get; protected set; }
        [JsonProperty(PropertyName = "callback")]
        public string Callback { get; protected set; }
    }
}