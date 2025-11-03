#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes;

/// <summary>
/// Global emote.
/// </summary>
public class GlobalEmote : Emote
{
    /// <summary>
    /// Contains the image URLs for the emote.
    /// </summary>
    [JsonProperty("images")]
    public EmoteImages Images { get; protected set; }
}