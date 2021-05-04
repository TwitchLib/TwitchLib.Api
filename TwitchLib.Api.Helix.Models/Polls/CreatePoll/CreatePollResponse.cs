using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Polls.CreatePoll
{
    public class CreatePollResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Poll[] Data { get; protected set; }
    }
}
