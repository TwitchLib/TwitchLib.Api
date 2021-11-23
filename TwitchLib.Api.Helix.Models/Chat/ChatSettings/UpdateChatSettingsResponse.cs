using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Chat.ChatSettings
{
    public class UpdateChatSettingsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public UpdateChatSettingsResponseModel[] Data { get; protected set; }
    }
}
