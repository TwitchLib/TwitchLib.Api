using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Polls.EndPoll
{
    public class EndPollResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Poll[] Data { get; protected set; }
    }
}
