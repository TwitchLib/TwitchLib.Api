using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Subscriptions;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Subscriptions related APIs
    /// </summary>
    public class Subscriptions : ApiBase
    {
        public Subscriptions(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        /// <summary>
        /// Checks if a specific user (userId) is subscribed to a specific channel (broadcasterId).
        /// <para>Requires User access token with scope user:read:subscriptions</para>
        /// <para>Or requires App access token if the user has authorized your application with scope user:read:subscriptions</para>
        /// </summary>
        /// <param name="broadcasterId">User ID of an Affiliate or Partner broadcaster.</param>
        /// <param name="userId">User ID of a Twitch viewer.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="CheckUserSubscriptionResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<CheckUserSubscriptionResponse> CheckUserSubscriptionAsync(string broadcasterId, string userId, string accessToken = null)
        {

            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("BroadcasterId must be set");

            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("UserId must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("user_id", userId)
            };

            return TwitchGetGenericAsync<CheckUserSubscriptionResponse>("/subscriptions/user", ApiVersion.Helix, getParams, accessToken);
        }

        /// <summary>
        /// Gets a list of users that subscribe to the specified broadcaster filtered by a list of UserIds.
        /// <para>Required scope: channel:read:subscriptions</para>
        /// </summary>
        /// <param name="broadcasterId">User ID of the broadcaster. Must match the User ID in the Bearer token.</param>
        /// <param name="userIds">Filters the list to include only the specified subscribers. You may specify a maximum of 100 subscribers.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetUserSubscriptionsResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetUserSubscriptionsResponse> GetUserSubscriptionsAsync(string broadcasterId, List<string> userIds, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("BroadcasterId must be set");
            
            if (userIds == null || userIds.Count == 0)
                throw new BadParameterException("UserIds must be set contain at least one user id");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            getParams.AddRange(userIds.Select(userId => new KeyValuePair<string, string>("user_id", userId)));

            return TwitchGetGenericAsync<GetUserSubscriptionsResponse>("/subscriptions", ApiVersion.Helix, getParams, accessToken);
        }

        /// <summary>
        /// Gets a list of users that subscribe to the specified broadcaster.
        /// <para>Required scope: channel:read:subscriptions</para>
        /// </summary>
        /// <param name="broadcasterId">User ID of the broadcaster. Must match the User ID in the Bearer token.</param>
        /// <param name="first">Maximum number of objects to return. Maximum: 100. Default: 20.</param>
        /// <param name="after">Cursor for forward pagination: tells the server where to start fetching the next set of results in a multi-page response.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetBroadcasterSubscriptionsResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetBroadcasterSubscriptionsResponse> GetBroadcasterSubscriptionsAsync(string broadcasterId, int first = 20, string after = null, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("BroadcasterId must be set");

            if (first > 100)
                throw new BadParameterException("First must be 100 or less");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if (!string.IsNullOrWhiteSpace(after)) 
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetBroadcasterSubscriptionsResponse>("/subscriptions", ApiVersion.Helix, getParams, accessToken);
        }
    }
}
