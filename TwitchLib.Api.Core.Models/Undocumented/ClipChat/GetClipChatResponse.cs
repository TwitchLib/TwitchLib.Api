using Newtonsoft.Json;

namespace TwitchLib.Api.Core.Models.Undocumented.ClipChat
{
    public class GetClipChatResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ReChatMessage[] Messages { get; protected set; }
    }
}
