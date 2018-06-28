using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Helix.Users.GetUserExtensions
{
    public class GetUserExtensionsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public UserExtension[] Users { get; protected set; }
    }
}
