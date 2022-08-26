using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Charity.GetCharityCampaign;

namespace TwitchLib.Api.Helix
{
    public class Charity : ApiBase
    {

        public Charity(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetCharityCampaign
        /// <summary>
        /// [BETA] - Gets information about the charity campaign that a broadcaster is running, such as their fundraising goal and the amount that’s been donated so far.
        /// Requires a user access token that includes the channel:read:charity scope. 
        /// The ID in the broadcaster_id query parameter must match the user ID in the access token.
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster that’s actively running a charity campaign.</param>
        /// <param name="accessToken"></param>
        /// <returns>A list that contains the charity campaign that the broadcaster is currently running.</returns>
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
    }
}
