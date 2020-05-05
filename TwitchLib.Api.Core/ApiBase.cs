using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
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
        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);
        private string _serverToken;
        private DateTime _expires = DateTime.MinValue;

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
        public async Task<CredentialCheckResponseModel> CheckCredentialsAsync()
        {
            var message = "Check successful";
            var failMessage = "";
            var result = true;
            if (!string.IsNullOrWhiteSpace(Settings.ClientId) && !(await ValidClientId(Settings.ClientId).ConfigureAwait(false)))
            {
                result = false;
                failMessage = "The passed Client Id was not valid. To get a valid Client Id, register an application here: https://www.twitch.tv/kraken/oauth2/clients/new";
            }

            if (!string.IsNullOrWhiteSpace(Settings.AccessToken) && (await ValidAccessToken(Settings.AccessToken).ConfigureAwait(false)) == null)
            {
                result = false;
                failMessage += "The passed Access Token was not valid. To get an access token, go here:  https://twitchtokengenerator.com/";
            }

            return new CredentialCheckResponseModel { Result = result, ResultMessage = result ? message : failMessage };
        }

        public async Task DynamicScopeValidationAsync(AuthScopes requiredScope, string accessToken = null)
        {
            //Skip validation if skip is set or access token is null
            if (Settings.SkipDynamicScopeValidation || string.IsNullOrWhiteSpace(accessToken)) return;

            //set the scopes based on access token
            Settings.Scopes = await ValidAccessToken(accessToken).ConfigureAwait(false);
            //skip if no scopes
            if (Settings.Scopes == null)
                throw new InvalidCredentialException($"The current access token does not support this call. Missing required scope: {requiredScope.ToString().ToLower()}. You can skip this check by using: IApiSettings.SkipDynamicScopeValidation = true . You can also generate a new token with this scope here: https://twitchtokengenerator.com");

            if ((!Settings.Scopes.Contains(requiredScope) && requiredScope != AuthScopes.Any) || (requiredScope == AuthScopes.Any && Settings.Scopes.Any(x=>x == AuthScopes.None)))
                throw new InvalidCredentialException($"The current access token ({String.Join(",", Settings.Scopes)}) does not support this call. Missing required scope: {requiredScope.ToString().ToLower()}. You can skip this check by using: IApiSettings.SkipDynamicScopeValidation = true . You can also generate a new token with this scope here: https://twitchtokengenerator.com");
        }
        
        internal virtual Task<Models.Root.Root> GetRootAsync(string authToken = null, string clientId = null)
        {
            return TwitchGetGenericAsync<Models.Root.Root>("", ApiVersion.V5, accessToken: authToken, clientId: clientId);
        }

        public async Task<string> GetAccessTokenAsync(string accessToken = null)
        {
            if (!string.IsNullOrEmpty(accessToken))
                return accessToken;
            if (!string.IsNullOrEmpty(Settings.AccessToken))
                return Settings.AccessToken;

            await _lock.WaitAsync();

            try
            {
                if (!string.IsNullOrEmpty(Settings.Secret) && !string.IsNullOrEmpty(Settings.ClientId) &&
                    !Settings.SkipAutoServerTokenGeneration)
                    return await GenerateServerBasedAccessTokenAsync();
            }
            finally
            {
                _lock.Release();
            }

            return null;
        }

        internal async Task<string> GenerateServerBasedAccessTokenAsync()
        {
            if (_serverToken != null && DateTime.UtcNow < _expires)
                return _serverToken;

            var result = await _http.GeneralRequest($"{BaseOauthToken}?client_id={Settings.ClientId}&client_secret={Settings.Secret}&grant_type=client_credentials", "POST", null, ApiVersion.Helix, Settings.ClientId, null).ConfigureAwait(false);
            if (result.Key == 200)
            {
                var user = JsonConvert.DeserializeObject<JObject>(result.Value);

                _serverToken = user.Value<string>("access_token");
                _expires = DateTime.UtcNow.AddSeconds(user.Value<int>("expires_in") - 5);
                return _serverToken;
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

        protected async Task<T> TwitchGetGenericAsync<T>(string resource, ApiVersion api, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = await GetAccessTokenAsync(accessToken).ConfigureAwait(false);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return await _rateLimiter.Perform(async () =>
            {
                var req = await _http.GeneralRequest(url, "GET", null, api, clientId, accessToken).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(req.Value, _twitchLibJsonDeserializer);
            }).ConfigureAwait(false);
        }

        protected async Task<string> TwitchDeleteAsync(string resource, ApiVersion api, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = await GetAccessTokenAsync(accessToken).ConfigureAwait(false);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return await _rateLimiter.Perform(async () =>
            {
                var req = await _http.GeneralRequest(url, "GET", null, api, clientId, accessToken).ConfigureAwait(false);
                return req.Value;
            }).ConfigureAwait(false);
        }

        protected async Task<T> TwitchPostGenericAsync<T>(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = await GetAccessTokenAsync(accessToken).ConfigureAwait(false);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return await _rateLimiter.Perform(async () =>
            {
                var req = await _http.GeneralRequest(url, "POST", payload, api, clientId, accessToken).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(req.Value, _twitchLibJsonDeserializer);
            }).ConfigureAwait(false);
        }

        protected async Task<T> TwitchPostGenericModelAsync<T>(string resource, ApiVersion api, RequestModel model, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, api: api, overrideUrl: customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = await GetAccessTokenAsync(accessToken).ConfigureAwait(false);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return await _rateLimiter.Perform(async () =>
            {
                var req = await _http.GeneralRequest(url, "POST", model != null ? _jsonSerializer.SerializeObject(model) : "", api, clientId, accessToken).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(req.Value, _twitchLibJsonDeserializer);
            }).ConfigureAwait(false);
        }

        protected async Task<T> TwitchDeleteGenericAsync<T>(string resource, ApiVersion api, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, null, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = await GetAccessTokenAsync(accessToken).ConfigureAwait(false);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);


            return await _rateLimiter.Perform(async () =>
            {
                var req = await _http.GeneralRequest(url, "DELETE", null, api, clientId, accessToken).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(req.Value, _twitchLibJsonDeserializer);
            }).ConfigureAwait(false);
        }

        protected async Task<T> TwitchPutGenericAsync<T>(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = await GetAccessTokenAsync(accessToken).ConfigureAwait(false);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return await _rateLimiter.Perform(async () =>
            {
                var req = await _http.GeneralRequest(url, "PUT", payload, api, clientId, accessToken);
                return JsonConvert.DeserializeObject<T>(req.Value, _twitchLibJsonDeserializer);
            }).ConfigureAwait(false);
        }

        protected async Task<string> TwitchPutAsync(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = await GetAccessTokenAsync(accessToken).ConfigureAwait(false);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return await _rateLimiter.Perform(async () =>
            {
                var req = await _http.GeneralRequest(url, "PUT", payload, api, clientId, accessToken).ConfigureAwait(false);
                return req.Value;
            }).ConfigureAwait(false);
        }

        protected async Task<KeyValuePair<int, string>> TwitchPostAsync(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = await GetAccessTokenAsync(accessToken).ConfigureAwait(false);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return await _rateLimiter.Perform(() => _http.GeneralRequest(url, "POST", payload, api, clientId, accessToken)).ConfigureAwait(false);
        }


        protected Task PutBytesAsync(string url, byte[] payload)
        {
            return _http.PutBytes(url, payload);
        }

        internal Task<int> RequestReturnResponseCodeAsync(string url, string method, List<KeyValuePair<string, string>> getParams = null)
        {
            return _http.RequestReturnResponseCode(url, method, getParams);
        }

        protected async Task<T> GetGenericAsync<T>(string url, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, ApiVersion api = ApiVersion.V5, string clientId = null)
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

            accessToken = await GetAccessTokenAsync(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return await _rateLimiter.Perform(async () =>
            {
                var req = await _http.GeneralRequest(url, "GET", null, api, clientId, accessToken).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(
                    req.Value,
                    _twitchLibJsonDeserializer);
            }).ConfigureAwait(false);
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

        private async Task<bool> ValidClientId(string clientId)
        {
            try
            {
                var result = await GetRootAsync(null, clientId).ConfigureAwait(false);
                return result.Token != null;
            }
            catch (BadRequestException)
            {
                return false;
            }
        }

        private async Task<List<AuthScopes>> ValidAccessToken(string accessToken)
        {
            try
            {
                var resp = await GetRootAsync(accessToken).ConfigureAwait(false);
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
                    case "user:edit":
                        scopes.Add(AuthScopes.Helix_User_Edit);
                        break;
                    case "user:edit:broadcast":
                        scopes.Add(AuthScopes.Helix_User_Edit_Broadcast);
                        break;
                    case "user:read:broadcast":
                        scopes.Add(AuthScopes.Helix_User_Read_Broadcast);
                        break;
                    case "user:read:email":
                        scopes.Add(AuthScopes.Helix_User_Read_Email);
                        break;
                    case "clips:edit":
                        scopes.Add(AuthScopes.Helix_Clips_Edit);
                        break;
                    case "bits:read":
                        scopes.Add(AuthScopes.Helix_Bits_Read);
                        break;
                    case "analytics:read:games":
                        scopes.Add(AuthScopes.Helix_Analytics_Read_Games);
                        break;
                    case "analytics:read:extensions":
                        scopes.Add(AuthScopes.Helix_Analytics_Read_Extensions);
                        break;
                    case "channel:read:subscriptions":
                        scopes.Add(AuthScopes.Helix_Channel_Read_Subscriptions);
                        break;
                    case "moderation:read":
                        scopes.Add(AuthScopes.Helix_Moderation_Read);
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
