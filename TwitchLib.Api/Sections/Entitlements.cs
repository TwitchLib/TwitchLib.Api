using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;

namespace TwitchLib.Api.Sections
{
    public class Entitlements
    {
        public Entitlements(TwitchAPI api)
        {
            helix = new HelixApi(api);
        }

        public HelixApi helix { get; }

        public class HelixApi : ApiSection
        {
            public HelixApi(TwitchAPI api) : base(api)
            {
            }

            #region CreateEntitlementGrantsUploadURL

            public Task<Models.Helix.Entitlements.CreateEntitlementGrantsUploadURLResponse>
                CreateEntitlementGrantsUploadURL(string manifestId, Enums.EntitleGrantType type, string url = null,
                    string applicationAccessToken = null)
            {
                if (manifestId == null)
                    throw new BadParameterException("manifestId cannot be null");

                var getParams =
                    new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("manifest_id", manifestId)
                    };
                switch (type)
                {
                    case Enums.EntitleGrantType.BulkDropsGrant:
                        getParams.Add(new KeyValuePair<string, string>("type", "bulk_drops_grant"));
                        break;
                    default:
                        throw new BadParameterException("Unknown entitlement grant type");
                }

                return Api.TwitchGetGenericAsync<Models.Helix.Entitlements.CreateEntitlementGrantsUploadURLResponse>(
                    "/entitlements/upload", ApiVersion.Helix, getParams);
            }

            #endregion
        }
    }
}