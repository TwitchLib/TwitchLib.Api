using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Raids;

/// <summary>
/// A single object with information about the pending raid.
/// </summary>
public class Raid
{
    /// <summary>
    /// The UTC date and time, in RFC3339 format, of when the raid was requested.
    /// </summary>
    [JsonProperty(PropertyName = "created_at")]
    public DateTime CreatedAt { get; protected set; }

    /// <summary>
    /// A Boolean value that indicates whether the channel being raided contains mature content.
    /// </summary>
    [JsonProperty(PropertyName = "is_mature")]
    public bool IsMature { get; protected set; }
}
