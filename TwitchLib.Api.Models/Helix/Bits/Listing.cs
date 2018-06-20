using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Helix.Bits
{
    public class Listing
    {
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "rank")]
        public int Rank { get; protected set; }
        [JsonProperty(PropertyName = "score")]
        public int Score { get; protected set; }
    }
}
