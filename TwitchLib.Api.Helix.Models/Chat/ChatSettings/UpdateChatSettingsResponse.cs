using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.ChatSettings;

/// <summary>
/// Update Broadcaster chat settings response model
/// </summary>
public class UpdateChatSettingsResponse
{
    /// <summary>
    /// The list of chat settings. The list contains a single object with all the settings.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public UpdateChatSettingsResponseModel[] Data { get; protected set; }
}
