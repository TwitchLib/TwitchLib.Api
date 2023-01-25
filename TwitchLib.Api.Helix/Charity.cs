using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Charity.GetCharityCampaign;
using TwitchLib.Api.Helix.Models.Charity.GetCharityCampaignDonations;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Charity related APIs
    /// </summary>
    public class Charity : ApiBase
    {

        public Charity(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetCharityCampaign
        /// <summary>
        /// Gets information about the charity campaign that a broadcaster is running, such as their fundraising goal and the amount that’s been donated so far.
        /// Requires an user access token that includes the channel:read:charity scope. 
        /// The ID in the broadcaster_id query parameter must match the user ID in the access token.
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster that’s actively running a charity campaign.</param>
        /// <param name="accessToken">Optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetCharityCampaignResponse">A list that contains the charity campaign that the broadcaster is currently running.</returns>
        public Task<GetCharityCampaignResponse> GetCharityCampaignAsync(string broadcasterId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetGenericAsync<GetCharityCampaignResponse>("/charity/campaigns", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion

        #region GetCharityCampaignDonations
        /// <summary>
        /// Gets the list of donations that users have made to the broadcaster’s active charity campaign.
        /// Requires a user access token that includes the channel:read:charity scope.
        /// The ID in the broadcaster_id query parameter must match the user ID in the access token.
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster that’s actively running a charity campaign.</param>
        /// <param name="first">The maximum number of items to return per page in the response. The minimum page size is 1 item per page and the maximum is 100. The default is 20.</param>
        /// <param name="after">The cursor used to get the next page of results. The Pagination object in the response contains the cursor’s value.</param>
        /// <param name="accessToken">Optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetCharityCampaignResponse">A list that contains the charity campaign that the broadcaster is currently running.</returns>
        public Task<GetCharityCampaignDonationsResponse> GetCharityCampaignDonationsAsync(string broadcasterId, int first = 20, string after = null, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            if (first < 1 || first > 100)
                throw new BadParameterException("first cannot be less than 1 or greater than 100");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetCharityCampaignDonationsResponse>("/charity/donations", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion  
    }
}
