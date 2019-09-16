using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus
{
    public class AutoModResult
    {
        [JsonProperty(PropertyName = "msg_id")]
        public string MsgId { get; protected set; }
        [JsonProperty(PropertyName = "is_permitted")]
        public bool IsPermitted { get; protected set; }
    }
}
