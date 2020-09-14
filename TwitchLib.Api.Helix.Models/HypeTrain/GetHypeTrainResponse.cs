using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.HypeTrain
{
    public class GetHypeTrainResponse
    {
        [JsonProperty(PropertyName = "data")]
        public HypeTrain[] HypeTrain { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}