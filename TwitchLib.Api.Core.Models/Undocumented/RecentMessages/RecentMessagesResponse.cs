using Newtonsoft.Json;

namespace TwitchLib.Api.Core.Models.Undocumented.RecentMessages
{
    public class RecentMessagesResponse
    {
        [JsonProperty(PropertyName = "messages")]
        public string[] Messages { get; protected set; }
    }
}
