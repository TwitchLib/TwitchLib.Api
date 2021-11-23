using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Moderation.BlockedTerms
{
    public class AddBlockedTermResponse
    {
        [JsonProperty(PropertyName = "data")]
        public BlockedTerm[] Data { get; protected set; }
    }
}
