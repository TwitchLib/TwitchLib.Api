using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Entitlements;

namespace TwitchLib.Api.Helix
{
    public class Entitlements : ApiBase
    {
        public Entitlements(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region CreateEntitlementGrantsUploadURL

        public Task<CreateEntitlementGrantsUploadURLResponse> CreateEntitlementGrantsUploadURL(string manifestId, EntitleGrantType type, string url = null, string applicationAccessToken = null)
        {
            if (manifestId == null)
                throw new BadParameterException("manifestId cannot be null");

            var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("manifest_id", manifestId)
                };

            switch (type)
            {
                case EntitleGrantType.BulkDropsGrant:
                    getParams.Add(new KeyValuePair<string, string>("type", "bulk_drops_grant"));
                    break;
                default:
                    throw new BadParameterException("Unknown entitlement grant type");
            }

            return TwitchGetGenericAsync<CreateEntitlementGrantsUploadURLResponse>("/entitlements/upload", ApiVersion.Helix, getParams);
        }

        #endregion

    }
}