using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Charity.GetCharityCampaignDonations
{
    public class GetCharityCampaignDonationsResponse
    {
        /// <summary>
        /// A list that contains the donations that users have made to the broadcaster’s charity campaign.
        /// The list is empty if the broadcaster is not currently running a charity campaign;
        /// The donation information is not available after the campaign ends.
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public CharityCampaignDonationsResponseModel[] Data { get; protected set; }
        /// <summary>
        /// Contains the information used to page through the list of results.
        /// </summary>
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
