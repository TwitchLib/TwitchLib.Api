using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub.Conduits.UpdateConduits;

public class UpdateConduitsResponse
{
    /// <summary>
    /// <para>List of information about the client’s conduits.</para>
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Conduit[] Data { get; protected set; }
}