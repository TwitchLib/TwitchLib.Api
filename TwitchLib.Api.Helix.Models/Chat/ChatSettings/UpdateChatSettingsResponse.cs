using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.ChatSettings
{
    public class UpdateChatSettingsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public UpdateChatSettingsResponseModel[] Data { get; protected set; }
    }
}
