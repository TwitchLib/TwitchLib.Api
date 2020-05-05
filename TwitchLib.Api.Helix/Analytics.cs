using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Analytics;

namespace TwitchLib.Api.Helix
{
    public class Analytics : ApiBase
    {
        public Analytics(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetGameAnalytics

        public async Task<GetGameAnalyticsResponse> GetGameAnalyticsAsync(string gameId = null, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Helix_Analytics_Read_Games, authToken).ConfigureAwait(false);
            var getParams = new List<KeyValuePair<string, string>>();
            if (gameId != null)
                getParams.Add(new KeyValuePair<string, string>("game_id", gameId));

            return await TwitchGetGenericAsync<GetGameAnalyticsResponse>("/analytics/games", ApiVersion.Helix, getParams, authToken).ConfigureAwait(false);
        }

        #endregion

        #region GetExtensionAnalytics

        public async Task<GetExtensionAnalyticsResponse> GetExtensionAnalyticsAsync(string extensionId, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Helix_Analytics_Read_Extensions, authToken).ConfigureAwait(false);
            var getParams = new List<KeyValuePair<string, string>>();
            if (extensionId != null)
                getParams.Add(new KeyValuePair<string, string>("extension_id", extensionId));

            return await TwitchGetGenericAsync<GetExtensionAnalyticsResponse>("/analytics/extensions", ApiVersion.Helix, getParams, authToken).ConfigureAwait(false);
        }

        #endregion

    }
}