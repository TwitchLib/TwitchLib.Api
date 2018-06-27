using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Helix.Users.GetUserActiveExtensions
{
    public class GetUserActiveExtensionsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ActiveExtensions Data { get; protected set; }
    }
}
