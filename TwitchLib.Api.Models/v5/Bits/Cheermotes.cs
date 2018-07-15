using Newtonsoft.Json;

namespace TwitchLib.Api.Models.V5.Bits
{
    public class Cheermotes
    {
        [JsonProperty(PropertyName = "actions")]
        public Action[] Actions { get; protected set; }
    }
}
