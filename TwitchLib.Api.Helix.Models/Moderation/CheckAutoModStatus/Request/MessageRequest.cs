using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus.Request
{
    public class MessageRequest
    {
        [JsonProperty(PropertyName = "data")]
        public Message[] Messages { get; set; }
    }
}
