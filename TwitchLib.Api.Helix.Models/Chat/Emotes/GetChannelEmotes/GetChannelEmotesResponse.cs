using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes.GetChannelEmotes;

/// <summary>
/// Broadcaster’s list of custom emotes response object.
/// </summary>
public class GetChannelEmotesResponse
{
    /// <summary>
    /// The list of emotes that the specified broadcaster created.
    /// </summary>
    [JsonProperty("data")]
    public ChannelEmote[] ChannelEmotes { get; protected set; }

    /// <summary>
    /// A templated URL. Use the values from the id, format, scale, and theme_mode fields to replace the like-named 
    /// placeholder strings in the templated URL to create a CDN (content delivery network) URL that you use to fetch the emote.
    /// </summary>
    [JsonProperty("template")]
    public string Template { get; protected set; }
}