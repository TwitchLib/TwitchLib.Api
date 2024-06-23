using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule;

/// <summary>
/// Broadcaster’s streaming schedule.
/// </summary>
public class ChannelStreamSchedule
{
    /// <summary>
    /// A list that contains broadcast segments.
    /// </summary>
    [JsonProperty("segments")]
    public Segment[] Segments { get; protected set; }

    /// <summary>
    /// The ID of the broadcaster that owns the broadcast schedule.
    /// </summary>
    [JsonProperty("broadcaster_id")]
    public string BroadcasterId { get; protected set; }

    /// <summary>
    /// The broadcaster’s display name.
    /// </summary>
    [JsonProperty("broadcaster_name")]
    public string BroadcasterName { get; protected set; }

    /// <summary>
    /// The broadcaster’s login name.
    /// </summary>
    [JsonProperty("broadcaster_login")]
    public string BroadcasterLogin { get; protected set; }

    /// <summary>
    /// The dates when the broadcaster is on vacation and not streaming.
    /// </summary>
    [JsonProperty("vacation")]
    public Vacation Vacation { get; protected set; }
}