using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Polls
{
    /// <summary>
    /// A choice that viewers can choose from.
    /// </summary>
    public class Choice
    {
        /// <summary>
        /// An ID that identifies this choice.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }

        /// <summary>
        /// The choice's title. The title may contain a maximum of 25 characters.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }

        /// <summary>
        /// The total number of votes cast for this choice.
        /// </summary>
        [JsonProperty(PropertyName = "votes")]
        public int Votes { get; protected set; }

        /// <summary>
        /// The number of votes cast using Channel Points.
        /// </summary>
        [JsonProperty(PropertyName = "channel_points_votes")]
        public int ChannelPointsVotes { get; protected set; }

        /// <summary>
        /// Not used; will be set to 0.
        /// </summary>
        [JsonProperty(PropertyName = "bits_votes")]
        public int BitsVotes { get; protected set; }
    }
}
