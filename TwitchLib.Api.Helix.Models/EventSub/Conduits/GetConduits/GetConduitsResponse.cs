using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub.Conduits.GetConduits;

public class GetConduitsResponse
{
    /// <summary>
    /// <para>List of information about the client’s conduits.</para>
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Conduit[] Data { get; protected set; }
}