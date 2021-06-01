using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreamKey
{
    public class GetStreamKeyResponse
    {
        [JsonProperty(PropertyName = "data")]
        public StreamKey[] Streams { get; protected set; }
    }
}
