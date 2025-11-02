#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes;

/// <summary>
/// Broadcaster created custom emote.
/// </summary>
public class ChannelEmote : Emote
{
    /// <summary>
    /// Contains the image URLs for the emote.
    /// </summary>
    [JsonProperty("images")]
    public EmoteImages Images { get; protected set; }

    /// <summary>
    /// The subscriber tier at which the emote is unlocked.
    /// </summary>
    [JsonProperty("tier")]
    public string Tier { get; protected set; }

    /// <summary>
    /// The type of emote.
    /// </summary>
    [JsonProperty("emote_type")]
    public string EmoteType { get; protected set; }

    /// <summary>
    /// An ID that identifies the emote set that the emote belongs to.
    /// </summary>
    [JsonProperty("emote_set_id")]
    public string EmoteSetId { get; protected set; }
}