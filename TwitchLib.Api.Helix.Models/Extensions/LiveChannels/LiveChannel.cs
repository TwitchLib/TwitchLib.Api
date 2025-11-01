using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.LiveChannels;

/// <summary>
/// A broadcaster that is streaming live and that have installed or activated the extension.
/// </summary>
public class LiveChannel
{
    /// <summary>
    /// The ID of the broadcaster that is streaming live and has installed or activated the extension.
    /// </summary>
    [JsonProperty(PropertyName = "broadcaster_id")]
    public string BroadcasterId { get; protected set; }

    /// <summary>
    /// The broadcaster’s display name.
    /// </summary>
    [JsonProperty(PropertyName = "broadcaster_name")]
    public string BroadcasterName { get; protected set; }

    /// <summary>
    /// The name of the category or game being streamed.
    /// </summary>
    [JsonProperty(PropertyName = "game_name")]
    public string GameName { get; protected set; }

    /// <summary>
    /// The ID of the category or game being streamed.
    /// </summary>
    [JsonProperty(PropertyName = "game_id")]
    public string GameId { get; protected set; }

    /// <summary>
    /// The title of the broadcaster’s stream.
    /// </summary>
    [JsonProperty(PropertyName = "title")]
    public string Title { get; protected set; }
}
