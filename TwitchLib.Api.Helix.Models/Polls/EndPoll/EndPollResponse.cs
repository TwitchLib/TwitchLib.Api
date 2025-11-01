using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Polls.EndPoll;

/// <summary>
/// End poll response object.
/// </summary>
public class EndPollResponse
{
    /// <summary>
    /// A list that contains the poll that you ended.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Poll[] Data { get; protected set; }
}
