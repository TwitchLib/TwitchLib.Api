#nullable disable
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Schedule.GetChannelStreamSchedule;

/// <summary>
/// Get channel stream schedule response object.
/// </summary>
public class GetChannelStreamScheduleResponse
{
    /// <summary>
    /// The broadcaster’s streaming schedule.
    /// </summary>
    [JsonProperty("data")]
    public ChannelStreamSchedule Schedule { get; protected set; }

    /// <summary>
    /// The information used to page through a list of results.
    /// </summary>
    [JsonProperty("pagination")]
    public Pagination Pagination { get; protected set; }
}