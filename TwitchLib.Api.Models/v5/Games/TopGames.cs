﻿using Newtonsoft.Json;

namespace TwitchLib.Api.Models.V5.Games
{
    public class TopGames
    {
        #region Total
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        #endregion
        #region Top
        [JsonProperty(PropertyName = "top")]
        public TopGame[] Top { get; protected set; }
        #endregion
    }
}
