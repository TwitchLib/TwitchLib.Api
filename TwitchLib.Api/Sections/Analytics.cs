using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Models.Helix.Analytics;

namespace TwitchLib.Api.Sections
{
    public class Analytics
    {
        public Analytics(TwitchAPI api)
        {
            helix = new HelixApi(api);
        }

        public HelixApi helix { get; }

        public class HelixApi : ApiSection
        {
            public HelixApi(TwitchAPI api) : base(api)
            {
            }

            #region GetGameAnalytics

            public Task<GetGameAnalyticsResponse> GetGameAnalyticsAsync(string gameId = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Helix_Analytics_Read_Games, authToken);
                var getParams = new List<KeyValuePair<string, string>>();
                if (gameId != null)
                    getParams.Add(new KeyValuePair<string, string>("game_id", gameId));

                return Api.TwitchGetGenericAsync<Models.Helix.Analytics.GetGameAnalyticsResponse>("/analytics/games", ApiVersion.Helix, getParams, authToken);
            }

            #endregion

            #region GetExtensionAnalytics

            public Task<GetExtensionAnalyticsResponse> GetExtensionAnalyticsAsync(string extensionId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Helix_Analytics_Read_Extensions, authToken);
                var getParams = new List<KeyValuePair<string, string>>();
                if (extensionId != null)
                    getParams.Add(new KeyValuePair<string, string>("extension_id", extensionId));

                return Api.TwitchGetGenericAsync<Models.Helix.Analytics.GetExtensionAnalyticsResponse>("/analytics/extensions", ApiVersion.Helix, getParams, authToken);
            }

            #endregion
        }
    }
}