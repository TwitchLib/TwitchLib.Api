using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits;

/// <summary>
/// The reporting window’s start and end dates
/// </summary>
public class DateRange
{
    /// <summary>
    /// The reporting window’s start date.
    /// </summary>
    [JsonProperty(PropertyName = "started_at")]
    public DateTime StartedAt { get; protected set; }

    /// <summary>
    /// The reporting window’s end date.
    /// </summary>
    [JsonProperty(PropertyName = "ended_at")]
    public DateTime EndedAt { get; protected set; }
}
