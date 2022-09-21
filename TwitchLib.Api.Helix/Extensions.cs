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
    public class Extensions : ApiBase
    {
        public Extensions(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        { }

        #region GetExtensionTransactions

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
