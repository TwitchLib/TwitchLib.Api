using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Moderation.AutomodSettings
{
    public class GetAutomodSettingsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public AutomodSettingsResponseModel[] Data { get; protected set; }
    }
}
