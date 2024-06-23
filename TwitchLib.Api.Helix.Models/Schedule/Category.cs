using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule;

/// <summary>
/// The type of content.
/// </summary>
public class Category
{
    /// <summary>
    /// An ID that identifies the category that best represents the content.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; protected set; }

    /// <summary>
    /// The name of the category.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; protected set; }
}