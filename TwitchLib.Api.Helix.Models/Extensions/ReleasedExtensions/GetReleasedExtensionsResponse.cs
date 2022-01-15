using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions
{
    public class GetReleasedExtensionsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ReleasedExtension[] Data { get; protected set; }
    }
}
