using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Common
{
    public class Pagination
    {
        [JsonProperty(PropertyName = "cursor")]
        public string Cursor { get; protected set; }
    }
}
