using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Models.Helix.Entitlements;

namespace TwitchLib.Api.Sections
{
    public class Entitlements
    {
        public Entitlements(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
        {
            Helix = new HelixApi(settings, rateLimiter, http);
        }

        public HelixApi Helix { get; }

        public class HelixApi : ApiBase
        {
            public HelixApi(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
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
}