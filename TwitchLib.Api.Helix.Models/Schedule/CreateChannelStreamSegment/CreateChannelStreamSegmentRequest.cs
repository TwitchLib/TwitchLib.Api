#nullable disable
using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule.CreateChannelStreamSegment;

/// <summary>
/// Create channel stream segment request object.
/// </summary>
public class CreateChannelStreamSegmentRequest
{
    /// <summary>
    /// REQUIRED
    /// The date and time that the broadcast segment starts.
    /// </summary>
    [JsonProperty("start_time")]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// REQUIRED
    /// The time zone where the broadcast takes place.
    /// Specify the time zone using IANA time zone database format.
    /// </summary>
    [JsonProperty("timezone")]
    public string Timezone { get; set; }

    /// <summary>
    /// A Boolean value that determines whether the broadcast recurs weekly.
    /// Only partners and affiliates may add non-recurring broadcasts.
    /// </summary>
    [JsonProperty("is_recurring")]
    public bool IsRecurring { get; set; }

    /// <summary>
    /// REQUIRED
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
    public string Title { get; set; }
}