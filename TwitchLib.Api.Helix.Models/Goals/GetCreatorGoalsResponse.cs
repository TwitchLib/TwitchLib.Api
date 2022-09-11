using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Goals
{
    public class GetCreatorGoalsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public CreatorGoal[] Data { get; protected set; }
    }
}
