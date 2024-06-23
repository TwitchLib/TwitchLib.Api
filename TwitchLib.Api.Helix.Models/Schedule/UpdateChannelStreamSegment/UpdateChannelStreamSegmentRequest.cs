using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule.UpdateChannelStreamSegment;

/// <summary>
/// Update channel stream segment request object.
/// </summary>
public class UpdateChannelStreamSegmentRequest
{
    /// <summary>
    /// The date and time that the broadcast segment starts.
    /// </summary>
    [JsonProperty("start_time")]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// The length of time, in minutes, that the broadcast is scheduled to run.
    /// The duration must be in the range 30 through 1380 (23 hours).
    /// </summary>
    [JsonProperty("duration")]
    public string Duration { get; set; }

    /// <summary>
    /// The ID of the category that best represents the broadcast’s content.
    /// </summary>
    [JsonProperty("category_id")]
    public string CategoryId { get; set; }

    /// <summary>
    /// The broadcast’s title.
    /// The title may contain a maximum of 140 characters.
    /// </summary>
    [JsonProperty("title")]
    public string Title {  get; set; }

    /// <summary>
    /// A Boolean value that indicates whether the broadcast is canceled.
    /// </summary>
    [JsonProperty("is_canceled")]
    public bool IsCanceled { get; set; }

    /// <summary>
    /// The time zone where the broadcast takes place.
    /// </summary>
    [JsonProperty("timezone")]
    public string Timezone { get; set; }
}