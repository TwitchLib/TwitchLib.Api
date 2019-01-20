using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Streams
{
    public class GetStreamTagsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Tag[] Data { get; protected set; }
    }
}
