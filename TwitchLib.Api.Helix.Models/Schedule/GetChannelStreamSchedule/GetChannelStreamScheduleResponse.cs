using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Schedule.GetChannelStreamSchedule
{
    public class GetChannelStreamScheduleResponse
    {
        [JsonProperty("data")]
        public ChannelStreamSchedule Schedule { get; protected set; }
        [JsonProperty("pagination")]
        public Pagination Pagination { get; protected set; }
    }
}