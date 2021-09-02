using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Goals
{
    public class GetCreatorGoalsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public CreatorGoal[] Data { get; protected set; }
    }
}
