﻿using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Games
{
    public class Game
    {
        #region Id
        [JsonProperty(PropertyName = "_id")]
        public int Id { get; protected set; }
        #endregion
        #region Box
        [JsonProperty(PropertyName = "box")]
        public GameBox Box { get; protected set; }
        #endregion
        #region GiantbombId
        [JsonProperty(PropertyName = "giantbomb_id")]
        public int GiantbombId { get; protected set; }
        #endregion
        #region Logo
        [JsonProperty(PropertyName = "logo")]
        public GameLogo Logo { get; protected set; }
        #endregion
        #region Name
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        #endregion
    }
}
