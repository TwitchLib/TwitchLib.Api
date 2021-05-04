using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Polls.GetPolls
{
    public class GetPollsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Poll[] Data { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
