using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.Models;


namespace TwitchLib.Api.Core
{
    public class ApiBase
    {
        private readonly TwitchLibJsonSerializer _jsonSerializer;
        protected readonly IApiSettings Settings;
        private readonly IRateLimiter _rateLimiter;
        private readonly IHttpCallHandler _http;

        internal const string BaseV5 = "https://api.twitch.tv/kraken";
        internal const string BaseHelix = "https://api.twitch.tv/helix";
        internal const string BaseOauthToken = "https://id.twitch.tv/oauth2/token";

        public ApiBase(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
        {
            Settings = settings;
            _rateLimiter = rateLimiter;
            _http = http;
            _jsonSerializer = new TwitchLibJsonSerializer();
        }

        /// <summary>
        /// Checks the ClientId and AccessToken against the Twitch Api Endpoints
        /// </summary>
        /// <returns>CredentialCheckResponseModel with a success boolean and message</returns>
        public Task<CredentialCheckResponseModel> CheckCredentialsAsync()
        {
            var message = "Check successful";
            var failMessage = "";
            var result = true;
            if (!string.IsNullOrWhiteSpace(Settings.ClientId) && !ValidClientId(Settings.ClientId))
            {
                result = false;
                failMessage = "The passed Client Id was not valid. To get a valid Client Id, register an application here: https://www.twitch.tv/kraken/oauth2/clients/new";
            }

            if (!string.IsNullOrWhiteSpace(Settings.AccessToken) && ValidAccessToken(Settings.AccessToken) == null)
            {
                result = false;
                failMessage += "The passed Access Token was not valid. To get an access token, go here:  https://twitchtokengenerator.com/";
            }

            return Task.FromResult(new CredentialCheckResponseModel { Result = result, ResultMessage = result ? message : failMessage });
        }

        public void DynamicScopeValidation(AuthScopes requiredScope, string accessToken = null)
        {
            //Skip validation if skip is set or access token is null
            if (Settings.SkipDynamicScopeValidation || string.IsNullOrWhiteSpace(accessToken)) return;

            //set the scopes based on access token
            Settings.Scopes = ValidAccessToken(accessToken);
            //skip if no scopes
            if (Settings.Scopes == null)
                throw new InvalidCredentialException($"The current access token does not support this call. Missing required scope: {requiredScope.ToString().ToLower()}. You can skip this check by using: IApiSettings.SkipDynamicScopeValidation = true . You can also generate a new token with this scope here: https://twitchtokengenerator.com");

            if ((!Settings.Scopes.Contains(requiredScope) && requiredScope != AuthScopes.Any) || (requiredScope == AuthScopes.Any && Settings.Scopes.Any(x => x == AuthScopes.None)))
                throw new InvalidCredentialException($"The current access token ({String.Join(",", Settings.Scopes)}) does not support this call. Missing required scope: {requiredScope.ToString().ToLower()}. You can skip this check by using: IApiSettings.SkipDynamicScopeValidation = true . You can also generate a new token with this scope here: https://twitchtokengenerator.com");
        }

        internal virtual Task<Models.Root.Root> GetRootAsync(string authToken = null, string clientId = null)
        {
            return TwitchGetGenericAsync<Models.Root.Root>("", ApiVersion.V5, accessToken: authToken, clientId: clientId);
        }

        public string GetAccessToken(string accessToken = null)
        {
            if (!string.IsNullOrEmpty(accessToken))
                return accessToken;
            if (!string.IsNullOrEmpty(Settings.AccessToken))
                return Settings.AccessToken;
            if (!string.IsNullOrEmpty(Settings.Secret) && !string.IsNullOrEmpty(Settings.ClientId) && !Settings.SkipAutoServerTokenGeneration)
                return GenerateServerBasedAccessToken();

            return null;
        }

        internal string GenerateServerBasedAccessToken()
        {
            var result = _http.GeneralRequest($"{BaseOauthToken}?client_id={Settings.ClientId}&client_secret={Settings.Secret}&grant_type=client_credentials", "POST", null, ApiVersion.Helix, Settings.ClientId, null);
            if (result.Key == 200)
            {
                var user = JsonConvert.DeserializeObject<dynamic>(result.Value);
                var offset = (int)user.expires_in;
                return (string)user.access_token;
            }
            return null;
        }

        internal void ForceAccessTokenAndClientIdForHelix(string clientId, string accessToken, ApiVersion api)
        {
            if (api != ApiVersion.Helix)
                return;
            if (!string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(accessToken))
                return;
            throw new ClientIdAndOAuthTokenRequired("As of May 1, all calls to Twitch's Helix API require Client-ID and OAuth access token be set. Example: api.Settings.AccessToken = \"twitch-oauth-access-token-here\"; api.Settings.ClientId = \"twitch-client-id-here\";");
        }

        protected Task<T> TwitchGetGenericAsync<T>(string resource, ApiVersion api, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => JsonConvert.DeserializeObject<T>(_http.GeneralRequest(url, "GET", null, api, clientId, accessToken).Value, _twitchLibJsonDeserializer)).ConfigureAwait(false));
        }

        protected Task<T> TwitchPatchGenericAsync<T>(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => JsonConvert.DeserializeObject<T>(_http.GeneralRequest(url, "PATCH", payload, api, clientId, accessToken).Value, _twitchLibJsonDeserializer)).ConfigureAwait(false));
        }

        protected Task<string> TwitchPatchAsync(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => _http.GeneralRequest(url, "PATCH", payload, api, clientId, accessToken).Value).ConfigureAwait(false));
        }

        protected Task<string> TwitchDeleteAsync(string resource, ApiVersion api, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => _http.GeneralRequest(url, "DELETE", null, api, clientId, accessToken).Value).ConfigureAwait(false));
        }

        protected Task<T> TwitchPostGenericAsync<T>(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => JsonConvert.DeserializeObject<T>(_http.GeneralRequest(url, "POST", payload, api, clientId, accessToken).Value, _twitchLibJsonDeserializer)).ConfigureAwait(false));
        }

        protected Task<T> TwitchPostGenericModelAsync<T>(string resource, ApiVersion api, RequestModel model, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, api: api, overrideUrl: customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => JsonConvert.DeserializeObject<T>(_http.GeneralRequest(url, "POST", model != null ? _jsonSerializer.SerializeObject(model) : "", api, clientId, accessToken).Value, _twitchLibJsonDeserializer)).ConfigureAwait(false));
        }

        protected Task<T> TwitchDeleteGenericAsync<T>(string resource, ApiVersion api, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => JsonConvert.DeserializeObject<T>(_http.GeneralRequest(url, "DELETE", null, api, clientId, accessToken).Value, _twitchLibJsonDeserializer)).ConfigureAwait(false));
        }

        protected Task<T> TwitchPutGenericAsync<T>(string resource, ApiVersion api, string payload = null, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => JsonConvert.DeserializeObject<T>(_http.GeneralRequest(url, "PUT", payload, api, clientId, accessToken).Value, _twitchLibJsonDeserializer)).ConfigureAwait(false));
        }

        protected Task<string> TwitchPutAsync(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => _http.GeneralRequest(url, "PUT", payload, api, clientId, accessToken).Value).ConfigureAwait(false));
        }

        protected Task<KeyValuePair<int, string>> TwitchPostAsync(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => _http.GeneralRequest(url, "POST", payload, api, clientId, accessToken)).ConfigureAwait(false));
        }


        protected void PutBytes(string url, byte[] payload)
        {
            _http.PutBytes(url, payload);
        }

        internal int RequestReturnResponseCode(string url, string method, List<KeyValuePair<string, string>> getParams = null)
        {
            return _http.RequestReturnResponseCode(url, method, getParams);
        }

        protected Task<T> GetGenericAsync<T>(string url, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, ApiVersion api = ApiVersion.V5, string clientId = null)
        {
            if (getParams != null)
            {
                for (var i = 0; i < getParams.Count; i++)
                {
                    if (i == 0)
                        url += $"?{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                    else
                        url += $"&{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                }
            }

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => JsonConvert.DeserializeObject<T>(_http.GeneralRequest(url, "GET", null, api, clientId, accessToken).Value, _twitchLibJsonDeserializer)).ConfigureAwait(false));
        }

        internal Task<T> GetSimpleGenericAsync<T>(string url, List<KeyValuePair<string, string>> getParams = null)
        {
            if (getParams != null)
            {
                for (var i = 0; i < getParams.Count; i++)
                {
                    if (i == 0)
                        url += $"?{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                    else
                        url += $"&{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                }
            }
            return _rateLimiter.Perform(async () => JsonConvert.DeserializeObject<T>(await SimpleRequestAsync(url), _twitchLibJsonDeserializer));
        }

        // credit: https://stackoverflow.com/questions/14290988/populate-and-return-entities-from-downloadstringcompleted-handler-in-windows-pho
        private Task<string> SimpleRequestAsync(string url)
        {
            var tcs = new TaskCompletionSource<string>();
            var client = new WebClient();

            client.DownloadStringCompleted += DownloadStringCompletedEventHandler;
            client.DownloadString(new Uri(url));

            return tcs.Task;

            // local function
            void DownloadStringCompletedEventHandler(object sender, DownloadStringCompletedEventArgs args)
            {
                if (args.Cancelled)
                    tcs.SetCanceled();
                else if (args.Error != null)
                    tcs.SetException(args.Error);
                else
                    tcs.SetResult(args.Result);

                client.DownloadStringCompleted -= DownloadStringCompletedEventHandler;
            }
        }

        private readonly JsonSerializerSettings _twitchLibJsonDeserializer = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };

        private class TwitchLibJsonSerializer
        {
            private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
            {
                ContractResolver = new LowercaseContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            public string SerializeObject(object o)
            {
                return JsonConvert.SerializeObject(o, Formatting.Indented, _settings);
            }

            private class LowercaseContractResolver : DefaultContractResolver
            {
                protected override string ResolvePropertyName(string propertyName)
                {
                    return propertyName.ToLower();
                }
            }
        }

        private bool ValidClientId(string clientId)
        {
            try
            {
                var result = GetRootAsync(null, clientId).GetAwaiter().GetResult();
                return result.Token != null;
            }
            catch (BadRequestException)
            {
                return false;
            }
        }

        private List<AuthScopes> ValidAccessToken(string accessToken)
        {
            try
            {
                var resp = GetRootAsync(accessToken).GetAwaiter().GetResult();
                if (resp.Token == null) return null;

                return BuildScopesList(resp.Token);


            }
            catch
            {
                return null;
            }
        }

        private static List<AuthScopes> BuildScopesList(Models.Root.RootToken token)
        {
            var scopes = new List<AuthScopes>();
            foreach (var scope in token.Auth.Scopes)
            {
                switch (scope)
                {
                    case "channel_check_subscription":
                        scopes.Add(AuthScopes.Channel_Check_Subscription);
                        break;
                    case "channel_commercial":
                        scopes.Add(AuthScopes.Channel_Commercial);
                        break;
                    case "channel_editor":
                        scopes.Add(AuthScopes.Channel_Editor);
                        break;
                    case "channel_feed_edit":
                        scopes.Add(AuthScopes.Channel_Feed_Edit);
                        break;
                    case "channel_feed_read":
                        scopes.Add(AuthScopes.Channel_Feed_Read);
                        break;
                    case "channel_read":
                        scopes.Add(AuthScopes.Channel_Read);
                        break;
                    case "channel_stream":
                        scopes.Add(AuthScopes.Channel_Stream);
                        break;
                    case "channel_subscriptions":
                        scopes.Add(AuthScopes.Channel_Subscriptions);
                        break;
                    case "chat_login":
                        scopes.Add(AuthScopes.Chat_Login);
                        break;
                    case "collections_edit":
                        scopes.Add(AuthScopes.Collections_Edit);
                        break;
                    case "communities_edit":
                        scopes.Add(AuthScopes.Communities_Edit);
                        break;
                    case "communities_moderate":
                        scopes.Add(AuthScopes.Communities_Moderate);
                        break;
                    case "user_blocks_edit":
                        scopes.Add(AuthScopes.User_Blocks_Edit);
                        break;
                    case "user_blocks_read":
                        scopes.Add(AuthScopes.User_Blocks_Read);
                        break;
                    case "user_follows_edit":
                        scopes.Add(AuthScopes.User_Follows_Edit);
                        break;
                    case "user_read":
                        scopes.Add(AuthScopes.User_Read);
                        break;
                    case "user_subscriptions":
                        scopes.Add(AuthScopes.User_Subscriptions);
                        break;
                    case "openid":
                        scopes.Add(AuthScopes.OpenId);
                        break;
                    case "viewing_activity_read":
                        scopes.Add(AuthScopes.Viewing_Activity_Read);
                        break;
                    case "user:edit:broadcast":
                        scopes.Add(AuthScopes.Helix_User_Edit_Broadcast);
                        break;
                    case "analytics:read:extensions":
                        scopes.Add(AuthScopes.Helix_Analytics_Read_Extensions);
                        break;
                    case "analytics:read:games":
                        scopes.Add(AuthScopes.Helix_Analytics_Read_Games);
                        break;
                    case "bits:read":
                        scopes.Add(AuthScopes.Helix_Bits_Read);
                        break;
                    case "channel:edit:commercial":
                        scopes.Add(AuthScopes.Helix_Channel_Edit_Commercial);
                        break;
                    case "channel:manage:broadcast":
                        scopes.Add(AuthScopes.Helix_Channel_Manage_Broadcast);
                        break;
                    case "channel:manage:extensions":
                        scopes.Add(AuthScopes.Helix_Channel_Manage_Extensions);
                        break;
                    case "channel:manage:redemptions":
                        scopes.Add(AuthScopes.Helix_Channel_Manage_Redemptions);
                        break;
                    case "channel:read:hype_train":
                        scopes.Add(AuthScopes.Helix_Channel_Read_Hype_Train);
                        break;
                    case "channel:read:redemptions":
                        scopes.Add(AuthScopes.Helix_Channel_Read_Redemptions);
                        break;
                    case "channel:read:stream_key":
                        scopes.Add(AuthScopes.Helix_Channel_Read_Stream_Key);
                        break;
                    case "channel:read:subscriptions":
                        scopes.Add(AuthScopes.Helix_Channel_Read_Subscriptions);
                        break;
                    case "clips:edit":
                        scopes.Add(AuthScopes.Helix_Clips_Edit);
                        break;
                    case "moderation:read":
                        scopes.Add(AuthScopes.Helix_Moderation_Read);
                        break;
                    case "user:edit":
                        scopes.Add(AuthScopes.Helix_User_Edit);
                        break;
                    case "user:edit:follows":
                        scopes.Add(AuthScopes.Helix_User_Edit_Follows);
                        break;
                    case "user:read:broadcast":
                        scopes.Add(AuthScopes.Helix_User_Read_Broadcast);
                        break;
                    case "user:read:email":
                        scopes.Add(AuthScopes.Helix_User_Read_Email);
                        break;
                    case "channel:read:editors":
                        scopes.Add(AuthScopes.Helix_Channel_Read_Editors);
                        break;
                    case "channel:manage:videos":
                        scopes.Add(AuthScopes.Helix_Channel_Manage_Videos);
                        break;
                    case "user:read:blocked_users":
                        scopes.Add(AuthScopes.Helix_User_Read_BlockedUsers);
                        break;
                    case "user:manage:blocked_users":
                        scopes.Add(AuthScopes.Helix_User_Manage_BlockedUsers);
                        break;
                }
            }

            if (scopes.Count == 0)
                scopes.Add(AuthScopes.None);
            return scopes;
        }

        private string ConstructResourceUrl(string resource = null, List<KeyValuePair<string, string>> getParams = null, ApiVersion api = ApiVersion.V5, string overrideUrl = null)
        {
            var url = "";
            if (overrideUrl == null)
            {
                if (resource == null)
                    throw new Exception("Cannot pass null resource with null override url");
                switch (api)
                {
                    case ApiVersion.V5:
                        url = $"{BaseV5}{resource}";
                        break;
                    case ApiVersion.Helix:
                        url = $"{BaseHelix}{resource}";
                        break;
                }
            }
            else
            {
                url = resource == null ? overrideUrl : $"{overrideUrl}{resource}";
            }
            if (getParams != null)
            {
                for (var i = 0; i < getParams.Count; i++)
                {
                    if (i == 0)
                        url += $"?{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                    else
                        url += $"&{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                }
            }
            return url;
        }


    }
}
