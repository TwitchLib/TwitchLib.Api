using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;

namespace TwitchLib.Api.Sections
{
    public class Analytics
    {
        public Analytics(TwitchAPI api)
        {
            helix = new Helix(api);
        }
        public Helix helix { get; }

        public class Helix : ApiSection
        {
            public Helix(TwitchAPI api) : base(api)
            {
            }
            #region GetGameAnalytics
            public Task<Models.Helix.Analytics.GetGameAnalyticsResponse> GetGameAnalytics(string gameId = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Helix_Analytics_Read_Games, authToken);
                var getParams = new List<KeyValuePair<string, string>>();
                if (gameId != null)
                    getParams.Add(new KeyValuePair<string, string>("game_id", gameId));

                return Api.TwitchGetGenericAsync<Models.Helix.Analytics.GetGameAnalyticsResponse>("/analytics/games", ApiVersion.Helix, getParams);
            }
            #endregion
        }
    }
}
