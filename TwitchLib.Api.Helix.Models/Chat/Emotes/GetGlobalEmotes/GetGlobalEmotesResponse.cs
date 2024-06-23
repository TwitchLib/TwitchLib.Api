using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes.GetGlobalEmotes;

/// <summary>
/// The list of global emotes response object.
/// </summary>
public class GetGlobalEmotesResponse
{
    /// <summary>
    /// The list of global emotes.
    /// </summary>
    [JsonProperty("data")]
    public GlobalEmote[] GlobalEmotes { get; protected set; }

    /// <summary>
    /// A templated URL. Use the values from the id, format, scale, and theme_mode fields to replace the like-named 
    /// placeholder strings in the templated URL to create a CDN (content delivery network) URL that you use to fetch the emote.
    /// </summary>
    [JsonProperty("template")]
    public string Template { get; protected set; }
}