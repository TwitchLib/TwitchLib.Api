using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Users.GetUserBlockList
{
    public class GetUserBlockListResponse
    {
        [JsonProperty(PropertyName = "data")]
        public BlockedUser[] Data { get; protected set; }
    }
}
