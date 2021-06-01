using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Polls.CreatePoll
{
    public class Choice
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}
