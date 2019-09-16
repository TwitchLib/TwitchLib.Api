using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus
{
    public class Message
    {
        [JsonProperty(PropertyName = "msg_id")]
        public string MsgId { get; set; }
        [JsonProperty(PropertyName = "msg_text")]
        public bool MsgText { get; set; }
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; }
    }
}
