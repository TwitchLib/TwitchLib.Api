#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes.GetEmoteSets;

/// <summary>
/// Emotes for one or more specified emote sets response object
/// </summary>
public class GetEmoteSetsResponse
{
    /// <summary>
    /// The list of emotes found in the specified emote sets.
    /// </summary>
    [JsonProperty("data")]
    public EmoteSet[] EmoteSets { get; protected set; }

    /// <summary>
    /// A templated URL. Use the values from the id, format, scale, and theme_mode fields to replace the like-named 
    /// placeholder strings in the templated URL to create a CDN (content delivery network) URL that you use to fetch the emote.
    /// </summary>
    [JsonProperty("template")]
    public string Template { get; protected set; }
}