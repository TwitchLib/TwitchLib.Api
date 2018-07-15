using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Communities
{
    public class CommunitiesResponse
    {
        [JsonProperty(PropertyName = "communities")]
        public Community[] Communities { get; protected set; }
    }
}
