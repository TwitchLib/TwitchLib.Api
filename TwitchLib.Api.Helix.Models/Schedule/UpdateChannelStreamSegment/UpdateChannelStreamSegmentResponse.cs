using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule.UpdateChannelStreamSegment
{
    public class UpdateChannelStreamSegmentResponse
    {
        [JsonProperty("data")]
        public ChannelStreamSchedule Schedule { get; protected set; }
    }
}