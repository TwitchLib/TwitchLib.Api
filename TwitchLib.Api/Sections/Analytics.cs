using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Models.Helix.Analytics;

namespace TwitchLib.Api.Sections
{
    public class Analytics
    {
        public Analytics(IApiSettings settings, IRateLimiter rateLimiter,  IHttpCallHandler http)
        {
            Helix = new HelixApi(settings, rateLimiter, http);
        }

        public HelixApi Helix { get; }

        public class HelixApi : ApiBase
        {
            public HelixApi(IApiSettings settings, IRateLimiter rateLimiter,  IHttpCallHandler http) : base(settings,rateLimiter,  http)
            {
            }

            #region GetGameAnalytics

            public Task<GetGameAnalyticsResponse> GetGameAnalyticsAsync(string gameId = null, string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.Helix_Analytics_Read_Games, authToken);
                var getParams = new List<KeyValuePair<string, string>>();
                if (gameId != null)
                    getParams.Add(new KeyValuePair<string, string>("game_id", gameId));

                return TwitchGetGenericAsync<GetGameAnalyticsResponse>("/analytics/games", ApiVersion.Helix, getParams, authToken);
            }

            #endregion

            #region GetExtensionAnalytics

            public Task<GetExtensionAnalyticsResponse> GetExtensionAnalyticsAsync(string extensionId, string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.Helix_Analytics_Read_Extensions, authToken);
                var getParams = new List<KeyValuePair<string, string>>();
                if (extensionId != null)
                    getParams.Add(new KeyValuePair<string, string>("extension_id", extensionId));

                return TwitchGetGenericAsync<GetExtensionAnalyticsResponse>("/analytics/extensions", ApiVersion.Helix, getParams, authToken);
            }

            #endregion
        }
    }
}