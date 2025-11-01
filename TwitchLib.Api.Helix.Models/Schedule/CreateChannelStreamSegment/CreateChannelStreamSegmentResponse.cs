using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule.CreateChannelStreamSegment;

/// <summary>
/// Create channel streams segment response object.
/// </summary>
public class CreateChannelStreamSegmentResponse
{
    /// <summary>
    /// The broadcaster’s streaming scheduled.
    /// </summary>
    [JsonProperty("data")]
    public ChannelStreamSchedule Schedule { get; protected set; }
}