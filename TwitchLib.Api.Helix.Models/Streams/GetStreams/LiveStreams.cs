using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreams
{
    public class LiveStreams
    {
        #region Total
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        #endregion
        #region Streams
        [JsonProperty(PropertyName = "streams")]
        public Stream[] Streams { get; protected set; }
        #endregion
    }
}
