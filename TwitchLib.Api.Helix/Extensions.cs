using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Extensions.LiveChannels;
using TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions;
using TwitchLib.Api.Helix.Models.Extensions.Transactions;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Extensions related APIs
    /// </summary>
    public class Extensions : ApiBase
    {
        public Extensions(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        { }

        #region GetExtensionTransactions
        /// <summary>
        /// Gets the list of Extension transactions for a given Extension.
        /// </summary>
        /// <param name="extensionId">ID of the Extension to list transactions for.</param>
        /// <param name="ids">Transaction IDs to look up. Maximum: 100.</param>
        /// <param name="after">
        /// The cursor used to fetch the next page of data. This only applies to queries without ID. If an ID is specified, it supersedes the cursor.
        /// </param>
        /// <param name="first">Maximum number of objects to return. Maximum: 100. Default: 20.</param>
        /// <param name="applicationAccessToken">optional app access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetExtensionTransactionsResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetExtensionTransactionsResponse> GetExtensionTransactionsAsync(string extensionId, List<string> ids = null, string after = null, int first = 20, string applicationAccessToken = null)
        {
            if(string.IsNullOrWhiteSpace(extensionId))
                throw new BadParameterException("extensionId cannot be null");

            if (first < 1 || first > 100)
                throw new BadParameterException("'first' must between 1 (inclusive) and 100 (inclusive).");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("extension_id", extensionId)
            };

            if (ids != null)
            {
                getParams.AddRange(ids.Select(id => new KeyValuePair<string, string>("id", id)));
            }

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            getParams.Add(new KeyValuePair<string, string>("first", first.ToString()));

            return TwitchGetGenericAsync<GetExtensionTransactionsResponse>("/extensions/transactions", ApiVersion.Helix, getParams, applicationAccessToken);
        }

        #endregion

        #region GetExtensionLiveChannels
        /// <summary>
        /// Returns live channels that have installed or activated a specific Extension
        /// <para>A channel that recently went live may take a few minutes to appear in this list, and a channel may continue to appear on this list for a few minutes after it stops broadcasting.</para>
        /// </summary>
        /// <param name="extensionId">ID of the Extension to search for.</param>
        /// <param name="first">Maximum number of objects to return. Maximum: 100. Default: 20.</param>
        /// <param name="after">The cursor used to fetch the next page of data.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetExtensionLiveChannelsResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetExtensionLiveChannelsResponse> GetExtensionLiveChannelsAsync(string extensionId, int first = 20, string after = null, string accessToken = null)
        {
            if (string.IsNullOrEmpty(extensionId))
                throw new BadParameterException("extensionId must be set");

            if (first < 1 || first > 100)
                throw new BadParameterException("'first' must between 1 (inclusive) and 100 (inclusive).");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("extension_id", extensionId),
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetExtensionLiveChannelsResponse>("/extensions/live", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region GetReleasedExtensions
        /// <summary>
        /// Gets information about a released Extension; either the current version or a specified version.
        /// </summary>
        /// <param name="extensionId">ID of the Extension.</param>
        /// <param name="extensionVersion">The specific version of the Extension to return. If not provided, the current version is returned.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetReleasedExtensionsResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetReleasedExtensionsResponse> GetReleasedExtensionsAsync(string extensionId, string extensionVersion = null, string accessToken = null)
        {
            if (string.IsNullOrEmpty(extensionId))
                throw new BadParameterException("extensionId must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("extension_id", extensionId),
            };

            if (!string.IsNullOrWhiteSpace(extensionVersion))
                getParams.Add(new KeyValuePair<string, string>("extension_version", extensionVersion));

            return TwitchGetGenericAsync<GetReleasedExtensionsResponse>("/extensions/released", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion
    }
}
