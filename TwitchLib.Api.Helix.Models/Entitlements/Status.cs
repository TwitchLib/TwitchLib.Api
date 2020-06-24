using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.Entitlements
{
    public class Status
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; protected set; }
        [JsonProperty(PropertyName = "status")]
        public CodeStatusEnum StatusEnum { get; protected set; }
    }
}
