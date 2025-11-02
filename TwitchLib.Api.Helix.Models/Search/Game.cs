#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Search;

/// <summary>
/// The category or game.
/// </summary>
public class Game
{
    /// <summary>
    /// An ID that uniquely identifies the game or category.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; protected set; }

    /// <summary>
    /// The name of the game or category.
    /// </summary>
    [JsonProperty(PropertyName = "name")]
    public string Name { get; protected set; }

    /// <summary>
    /// A URL to an image of the game’s box art or streaming category.
    /// </summary>
    [JsonProperty(PropertyName = "box_art_url")]
    public string BoxArtUrl { get; protected set; }
}
