using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule.CreateChannelStreamSegment
{
    public class CreateChannelStreamSegmentResponse
    {
        [JsonProperty("data")]
        public ChannelStreamSchedule Schedule { get; protected set; }
    }
}