using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Polls.CreatePoll;

/// <summary>
/// Create poll response object.
/// </summary>
public class CreatePollResponse
{
    /// <summary>
    /// A list that contains the single poll that you created.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Poll[] Data { get; protected set; }
}
