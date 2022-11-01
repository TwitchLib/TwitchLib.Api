using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Goals;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Creator Goals related APIs
    /// </summary>
    public class Goals : ApiBase
    {
        public Goals(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetCreatorGoals
        /// <summary>
        /// Gets the broadcaster’s list of active goals. Use this to get the current progress of each goal.
        /// <para>Requires a user OAuth access token with scope set to channel:read:goals. </para>
        /// <para>The ID in the broadcasterId query parameter must match the user ID associated with the user OAuth token.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster that created the goals.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetCreatorGoalsResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetCreatorGoalsResponse> GetCreatorGoalsAsync(string broadcasterId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null or empty");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetGenericAsync<GetCreatorGoalsResponse>("/goals", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion

    }
}
