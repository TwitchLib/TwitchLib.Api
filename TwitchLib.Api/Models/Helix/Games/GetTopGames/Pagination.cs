using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Helix.Games.GetTopGames
{
    public class Pagination
    {
        [JsonProperty(PropertyName = "cursor")]
        public string Cursor { get; protected set; }
    }
}
