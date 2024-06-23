using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.SnoozeNextAd;

/// <summary>
/// <para>Response to attempting to snooze an ad.</para>
/// </summary>
public class SnoozeNextAdResponse
{
    /// <summary>
    /// <para>A list that contains information about the channel’s snoozes and next upcoming ad after successfully snoozing.</para>
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public SnoozeNextAd[] Data { get; protected set; }
}
