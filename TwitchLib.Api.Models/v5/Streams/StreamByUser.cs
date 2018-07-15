using Newtonsoft.Json;

namespace TwitchLib.Api.Models.V5.Streams
{
    public class StreamByUser
    {
        #region Stream
        [JsonProperty(PropertyName = "stream")]
        public Stream Stream { get; protected set; }
        #endregion
    }
}
