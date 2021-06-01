using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Streams.CreateStreamMarker
{
    public class CreateStreamMarkerResponse
    {
        [JsonProperty(PropertyName = "data")]
        public CreatedMarker[] Data { get; protected set; }
    }
}
