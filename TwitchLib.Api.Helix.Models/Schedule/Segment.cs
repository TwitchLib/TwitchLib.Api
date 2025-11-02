#nullable disable
using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule;

/// <summary>
/// A single broadcast segment.
/// </summary>
public class Segment
{
    /// <summary>
    /// An ID that identifies this broadcast segment.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; protected set; }

    /// <summary>
    /// The UTC date and time (in RFC3339 format) of when the broadcast starts.
    /// </summary>
    [JsonProperty("start_time")]
    public DateTime StartTime { get; protected set; }

    /// <summary>
    /// The UTC date and time (in RFC3339 format) of when the broadcast ends.
    /// </summary>
    [JsonProperty("end_time")]
    public DateTime EndTime { get; protected set; }

    /// <summary>
    /// The broadcast segment’s title.
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; protected set; }

    /// <summary>
    /// Indicates whether the broadcaster canceled this segment of a recurring broadcast.
    /// </summary>
    [JsonProperty("canceled_until")]
    public DateTime? CanceledUntil { get; protected set; }

    /// <summary>
    /// The type of content.
    /// </summary>
    [JsonProperty("category")]
    public Category Category { get; protected set; }

    /// <summary>
    /// A Boolean value that determines whether the broadcast is part of a recurring
    /// series that streams at the same time each week or is a one-time broadcast.
    /// </summary>
    [JsonProperty("is_recurring")]
    public bool IsRecurring { get; protected set; }
}