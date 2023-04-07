using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Entitlements.GetDropsEntitlements;
using TwitchLib.Api.Helix.Models.Entitlements.UpdateDropsEntitlements;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Entitlements related APIs
    /// </summary>
    public class Entitlements : ApiBase
    {
        public Entitlements(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetDropsEntitlements
        /// <summary>
        /// Gets a list of entitlements for a given organization that have been granted to a game, user, or both.
        /// <para>The client ID associated with the access token must have ownership of the game:</para>
        /// </summary>
        /// <param name="id">Unique identifier of the entitlement.</param>
        /// <param name="userId">A Twitch user ID to filter by.</param>
        /// <param name="gameId">A Twitch game ID to filter by.</param>
        /// <param name="after">The cursor used to fetch the next page of data.</param>
        /// <param name="first">Maximum number of entitlements to return. Default: 20 Max: 1000</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetDropsEntitlementsResponse"></returns>
        public Task<GetDropsEntitlementsResponse> GetDropsEntitlementsAsync(string id = null, string userId = null, string gameId = null, string after = null, int first = 20, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if(!string.IsNullOrWhiteSpace(id))
            {
                getParams.Add(new KeyValuePair<string, string>("id", id));
            }

            if(!string.IsNullOrWhiteSpace(userId))
            {
                getParams.Add(new KeyValuePair<string, string>("user_id", userId));
            }

            if(!string.IsNullOrWhiteSpace(gameId))
            {
                getParams.Add(new KeyValuePair<string, string>("game_id", gameId));
            }

            if(!string.IsNullOrWhiteSpace(after))
            {
                getParams.Add(new KeyValuePair<string, string>("after", after));
            }

            return TwitchGetGenericAsync<GetDropsEntitlementsResponse>("/entitlements/drops", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion

        #region UpdateDropsEntitlements
        /// <summary>
        /// Updates the fulfillment status on a set of Drops entitlements, specified by their entitlement IDs.
        /// <para>The client ID associated with the access token must have ownership of the game</para>
        /// </summary>
        /// <param name="entitlementIds">An array of unique identifiers of the entitlements to update. Maximum: 100.</param>
        /// <param name="fulfillmentStatus">What fulfillment status to set to. Valid values are "CLAIMED" or "FULFILLED"</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="UpdateDropsEntitlementsResponse"></returns>
        public Task<UpdateDropsEntitlementsResponse> UpdateDropsEntitlementsAsync(string[] entitlementIds, FulfillmentStatus fulfillmentStatus, string accessToken)
        {
            var body = new
            {
                entitlement_ids = entitlementIds,
                fulfillment_status = fulfillmentStatus.ToString()
            };

            return TwitchPatchGenericAsync<UpdateDropsEntitlementsResponse>("/entitlements/drops", ApiVersion.Helix, JsonConvert.SerializeObject(body), null, accessToken);
        }

        #endregion

    }
}