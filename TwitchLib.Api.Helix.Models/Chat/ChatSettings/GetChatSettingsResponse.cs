using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.ChatSettings
{
    public class GetChatSettingsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ChatSettingsResponseModel[] Data { get; protected set; }
    }
}
