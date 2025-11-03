#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule.UpdateChannelStreamSegment;

/// <summary>
/// Update channel stream segment response object.
/// </summary>
public class UpdateChannelStreamSegmentResponse
{
    /// <summary>
    /// The broadcaster’s streaming schedule.
    /// </summary>
    [JsonProperty("data")]
    public ChannelStreamSchedule Schedule { get; protected set; }
}