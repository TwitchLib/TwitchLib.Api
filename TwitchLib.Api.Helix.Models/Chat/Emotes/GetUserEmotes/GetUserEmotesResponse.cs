#nullable disable
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes.GetUserEmotes;

/// <summary>
/// Emotes available to the user across all channels response object.
/// </summary>
public class GetUserEmotesResponse
{
    /// <summary>
    /// A list of emotes available to the user across all channels.
    /// </summary>
    [JsonProperty("data")]
    public UserEmote[] Data { get; protected set; }

    /// <summary>
    /// A templated URL. Uses the values from the id, format, scale, and theme_mode fields to replace the like-named 
    /// placeholder strings in the templated URL to create a CDN (content delivery network) URL that you use to fetch the emote.
    /// </summary>
    [JsonProperty("template")]
    public string Template { get; protected set; }

    /// <summary>
    /// Contains the information used to page through the list of results.
    /// </summary>
    [JsonProperty("pagination")]
    public Pagination Pagination { get; protected set; }
}