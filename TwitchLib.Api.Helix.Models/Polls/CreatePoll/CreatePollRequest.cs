using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Polls.CreatePoll;

/// <summary>
/// Create poll request object.
/// </summary>
public class CreatePollRequest
{
    /// <summary>
    /// The ID of the broadcaster that’s running the poll. 
    /// This ID must match the user ID in the user access token.
    /// </summary>
    [JsonProperty(PropertyName = "broadcaster_id")]
    public string BroadcasterId { get; set; }

    /// <summary>
    /// The question that viewers will vote on.
    /// The question may contain a maximum of 60 characters.
    /// </summary>
    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; }

    /// <summary>
    /// A list of choices that viewers may choose from. 
    /// The list must contain a minimum of 2 choices and up to a maximum of 5 choices.
    /// </summary>
    [JsonProperty(PropertyName = "choices")]
    public Choice[] Choices { get; set; }

    /// <summary>
    /// A Boolean value that indicates whether viewers may cast additional votes using Channel Points.
    /// </summary>
    [JsonProperty(PropertyName = "channel_points_voting_enabled")]
    public bool ChannelPointsVotingEnabled { get; set; }

    /// <summary>
    /// The number of points that the viewer must spend to cast one additional vote.
    /// </summary>
    [JsonProperty(PropertyName = "channel_points_per_vote")]
    public int ChannelPointsPerVote { get; set; }

    /// <summary>
    /// The length of time (in seconds) that the poll will run for.
    /// The minimum is 15 seconds and the maximum is 1800 seconds (30 minutes).
    /// </summary>
    [JsonProperty(PropertyName = "duration")]
    public int DurationSeconds { get; set; }
}
