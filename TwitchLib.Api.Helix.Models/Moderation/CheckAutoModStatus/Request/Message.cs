using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus
{
    public class Message
    {
        /// <summary>
        /// Developer-generated identifier for mapping messages to results.
        /// </summary>
        [JsonProperty(PropertyName = "msg_id")]
        public string MsgId { get; set; }

        /// <summary>
        /// Message text.
        /// </summary>
        [JsonProperty(PropertyName = "msg_text")]
        public string MsgText { get; set; }
    }
}
