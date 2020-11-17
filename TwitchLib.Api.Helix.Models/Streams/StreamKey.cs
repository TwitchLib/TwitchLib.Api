using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Streams
{
    public class StreamKey
    {
        [JsonProperty(PropertyName = "stream_key")]
        public string Key { get; protected set; }
    }
}
