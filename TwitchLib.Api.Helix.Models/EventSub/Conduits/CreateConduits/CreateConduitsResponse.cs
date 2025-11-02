#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub.Conduits.CreateConduits;

/// <summary>
/// Create a new conduit response object.
/// </summary>
public class CreateConduitsResponse
{
    /// <summary>
    /// <para>List of information about the client’s conduits.</para>
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Conduit[] Data { get; protected set; }
}