using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Charity.GetCharityCampaignDonations
{
    public class CharityCampaignDonationsResponseModel
    {
        /// <summary>
        /// An ID that identifies the donation. The ID is unique across campaigns.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }

        /// <summary>
        /// An ID that identifies the charity campaign that the donation applies to.
        /// </summary>
        [JsonProperty(PropertyName = "campaign_id")]
        public string CampaignId { get; protected set; }

        /// <summary>
        /// An ID that identifies a user that donated money to the campaign.
        /// </summary>
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }

        /// <summary>
        /// The user’s login name.
        /// </summary>
        [JsonProperty(PropertyName = "user_login")]
        public string UserLogin { get; protected set; }

        /// <summary>
        /// The user’s display name.
        /// </summary>
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; protected set; }

        /// <summary>
        /// An object that contains the amount of money that the user donated.
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public Amount Amount { get; protected set; }
    }
}
