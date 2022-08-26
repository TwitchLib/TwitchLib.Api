using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Charity.GetCharityCampaign
{
    public class CharityCampaignResponseModel
    {
        /// <summary>
        /// An ID that uniquely identifies the charity campaign.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }

        /// <summary>
        /// An ID that uniquely identifies the broadcaster that’s running the campaign.
        /// </summary>
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; protected set; }

        /// <summary>
        /// The broadcaster’s login name.
        /// </summary>
        [JsonProperty(PropertyName = "broadcaster_login")]
        public string BroadcasterLogin { get; protected set; }

        /// <summary>
        /// The broadcaster’s display name.
        /// </summary>
        [JsonProperty(PropertyName = "broadcaster_name")]
        public string BroadcasterName { get; protected set; }

        /// <summary>
        /// The charity’s name.
        /// </summary>
        [JsonProperty(PropertyName = "charity_name")]
        public string CharityName { get; protected set; }

        /// <summary>
        /// A description of the charity.
        /// </summary>
        [JsonProperty(PropertyName = "charity_description")]
        public string CharityDescription { get; protected set; }

        /// <summary>
        /// A URL to an image of the charity’s logo. The image’s type is PNG and its size is 100px X 100px.
        /// </summary>
        [JsonProperty(PropertyName = "charity_logo")]
        public string CharityLogo { get; protected set; }

        /// <summary>
        /// A URL to the charity’s website.
        /// </summary>
        [JsonProperty(PropertyName = "charity_website")]
        public string CharityWebsite { get; protected set; }

        /// <summary>
        /// An object that contains the current amount of donations that the campaign has received.
        /// </summary>
        [JsonProperty(PropertyName = "current_amount")]
        public Amount CurrentAmount { get; protected set; }

        /// <summary>
        /// An object that contains the amount of money that the campaign is trying to raise. 
        /// This field may be null if the broadcaster has not defined a target goal.
        /// </summary>
        [JsonProperty(PropertyName = "target_amount")]
        public Amount TargetAmount { get; protected set; }
    }
}
