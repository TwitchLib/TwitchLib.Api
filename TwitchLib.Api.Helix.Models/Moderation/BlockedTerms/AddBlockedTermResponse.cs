using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.BlockedTerms
{
    public class AddBlockedTermResponse
    {
        [JsonProperty(PropertyName = "data")]
        public BlockedTerm[] Data { get; protected set; }
    }
}
