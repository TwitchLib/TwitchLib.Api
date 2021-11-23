using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Chat.ChatSettings
{
    public class GetChatSettingsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ChatSettingsResponseModel[] Data { get; protected set; }
    }
}
