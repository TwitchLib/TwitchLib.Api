#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Games;

/// <summary>
/// The category or game.
/// </summary>
public class Game
{
    /// <summary>
    /// An ID that identifies the category or game.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; protected set; }

    /// <summary>
    /// The category’s or game’s name.
    /// </summary>
    [JsonProperty(PropertyName = "name")]
    public string Name { get; protected set; }

    /// <summary>
    /// A URL to the category’s or game’s box art. You must replace the {width}x{height} placeholder with the size of image you want.
    /// </summary>
    [JsonProperty(PropertyName = "box_art_url")]
    public string BoxArtUrl { get; protected set; }

    /// <summary>
    /// The ID that IGDB uses to identify this game. 
    /// </summary>
    [JsonProperty(PropertyName = "igdb_id")]
    public string IgdbId { get; protected set; }
}
