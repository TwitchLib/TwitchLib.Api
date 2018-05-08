using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Helix.Games.GetTopGames
{
    public class GetTopGamesResponse
    {
        [JsonProperty(PropertyName = "data")]
        public TopGame[] Data { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
