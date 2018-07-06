using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Events;
using TwitchLib.Api.Models.ThirdParty.AuthorizationFlow;
using TwitchLib.Api.Models.ThirdParty.ModLookup;
using TwitchLib.Api.Models.ThirdParty.UsernameChange;

namespace TwitchLib.Api.Sections
{
    /// <summary>These endpoints are offered by third party services (NOT TWITCH), but are still pretty cool.</summary>
    public class ThirdParty
    {
        public ThirdParty(TwitchAPI api)
        {
            UsernameChange = new usernameChangeApi(api);
            ModLookup = new modLookupApi(api);
            AuthorizationFlow = new authorizationFlowApi(api);
        }

        public usernameChangeApi UsernameChange { get; }
        public modLookupApi ModLookup { get; }
        public authorizationFlowApi AuthorizationFlow { get; }

        public class usernameChangeApi : ApiSection
        {
            public usernameChangeApi(TwitchAPI api) : base(api)
            {
            }

            #region GetUsernameChanges

            public Task<List<UsernameChangeListing>> GetUsernameChangesAsync(string username)
            {
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("q", username),
                    new KeyValuePair<string, string>("format", "json")
                };
                return Api.GetGenericAsync<List<UsernameChangeListing>>("https://twitch-tools.rootonline.de/username_changelogs_search.php", getParams, null, ApiVersion.Void);
            }

            #endregion
        }

        public class modLookupApi : ApiSection
        {
            public modLookupApi(TwitchAPI api) : base(api)
            {
            }

            public Task<ModLookupResponse> GetChannelsModdedForByNameAsync(string username, int offset = 0, int limit = 100, bool useTls12 = true)
            {
                if (useTls12)
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("offset", offset.ToString()),
                    new KeyValuePair<string, string>("limit", limit.ToString())
                };
                return Api.GetGenericAsync<ModLookupResponse>($"https://twitchstuff.3v.fi/modlookup/api/user/{username}");
            }

            public Task<TopResponse> GetChannelsModdedForByTopAsync(bool useTls12 = true)
            {
                if (useTls12)
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                return Api.GetGenericAsync<TopResponse>("https://twitchstuff.3v.fi/modlookup/api/top");
            }

            public Task<StatsResponse> GetChannelsModdedForStatsAsync(bool useTls12 = true)
            {
                if (useTls12)
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                return Api.GetGenericAsync<StatsResponse>("https://twitchstuff.3v.fi/modlookup/api/stats");
            }
        }

        public class authorizationFlowApi : ApiSection
        {
            private const string BaseUrl = "https://twitchtokengenerator.com/api";
            private string _apiId;
            private Timer _pingTimer;

            public authorizationFlowApi(TwitchAPI api) : base(api)
            {
            }

            public event EventHandler<OnUserAuthorizationDetectedArgs> OnUserAuthorizationDetected;
            public event EventHandler<OnAuthorizationFlowErrorArgs> OnError;

            public CreatedFlow CreateFlow(string applicationTitle, IEnumerable<AuthScopes> scopes)
            {
                string scopesStr = null;
                foreach (var scope in scopes)
                    if (scopesStr == null)
                        scopesStr = Common.Helpers.AuthScopesToString(scope);
                    else
                        scopesStr += $"+{Common.Helpers.AuthScopesToString(scope)}";

                var createUrl = $"{BaseUrl}/create/{Common.Helpers.Base64Encode(applicationTitle)}/{scopesStr}";

                var resp = new WebClient().DownloadString(createUrl);
                return JsonConvert.DeserializeObject<CreatedFlow>(resp);
            }

            public RefreshTokenResponse RefreshToken(string refreshToken)
            {
                var refreshUrl = $"{BaseUrl}/refresh/{refreshToken}";

                var resp = new WebClient().DownloadString(refreshUrl);
                return JsonConvert.DeserializeObject<RefreshTokenResponse>(resp);
            }

            public void BeginPingingStatus(string id, int intervalMs = 5000)
            {
                _apiId = id;
                _pingTimer = new Timer(intervalMs);
                _pingTimer.Elapsed += OnPingTimerElapsed;
                _pingTimer.Start();
            }

            public PingResponse PingStatus(string id = null)
            {
                if (id != null)
                    _apiId = id;

                var resp = new WebClient().DownloadString($"{BaseUrl}/status/{_apiId}");
                var model = new PingResponse(resp);

                return model;
            }

            private void OnPingTimerElapsed(object sender, ElapsedEventArgs e)
            {
                var ping = PingStatus();
                if (ping.Success)
                {
                    _pingTimer.Stop();
                    OnUserAuthorizationDetected?.Invoke(null, new OnUserAuthorizationDetectedArgs {Id = ping.Id, Scopes = ping.Scopes, Token = ping.Token, Username = ping.Username, Refresh = ping.Refresh});
                }
                else
                {
                    if (ping.Error == 3) return;

                    _pingTimer.Stop();
                    OnError?.Invoke(null, new OnAuthorizationFlowErrorArgs {Error = ping.Error, Message = ping.Message});
                }
            }
        }
    }
}