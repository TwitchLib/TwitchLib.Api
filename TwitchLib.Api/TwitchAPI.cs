using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Internal;
using TwitchLib.Api.RateLimiter;
using TwitchLib.Api.Sections;

namespace TwitchLib.Api
{
    public class TwitchAPI : ITwitchAPI
    {
        private readonly ILogger<TwitchAPI> _logger;
        private readonly HttpClient _http;
        private readonly TwitchLibJsonSerializer _jsonSerializer;
        private readonly IRateLimiter _rateLimiter;

        internal const string baseV5 = "https://api.twitch.tv/kraken";
        internal const string baseHelix = "https://api.twitch.tv/helix";

        public IApiSettings Settings { get; }
        public Auth Auth { get; }
        public Badges Badges { get; }
        public Bits Bits { get; }
        public ChannelFeeds ChannelFeeds { get; }
        public Channels Channels { get; }
        public Chat Chat { get; }
        public Clips Clips { get; }
        public Collections Collections { get; }
        public Communities Communities { get; }
        public Games Games { get; }
        public Ingests Ingests { get; }
        public Root Root { get; }
        public Search Search { get; }
        public Streams Streams { get; }
        public Teams Teams { get; }
        public Debugging Debugging { get; }
        public Videos Videos { get; }
        public Users Users { get; }
        public Undocumented Undocumented { get; }
        public ThirdParty ThirdParty { get; }
        public Webhooks Webhooks { get; }

        /// <summary>
        /// Creates an Instance of the TwitchAPI Class.
        /// </summary>
        /// <param name="logger">Instance Of Logger, otherwise no logging is used,  </param>
        /// <param name="rateLimiter">Instance Of RateLimiter, otherwise no ratelimiter is used. </param>
        public TwitchAPI(ILogger<TwitchAPI> logger = null, IRateLimiter rateLimiter = null)
        {
            _logger = logger;
            _http = new HttpClient(new TwitchLibCustomHttpMessageHandler(new HttpClientHandler(), _logger));
            _rateLimiter = rateLimiter ?? BypassLimiter.CreateLimiterBypassInstance();
            Auth = new Auth(this);
            Badges = new Badges(this);
            Bits = new Bits(this);
            ChannelFeeds = new ChannelFeeds(this);
            Channels = new Channels(this);
            Chat = new Chat(this);
            Clips = new Clips(this);
            Collections = new Collections(this);
            Communities = new Communities(this);
            Games = new Games(this);
            Ingests = new Ingests(this);
            Root = new Root(this);
            Search = new Search(this);
            Streams = new Streams(this);
            Teams = new Teams(this);
            ThirdParty = new ThirdParty(this);
            Undocumented = new Undocumented(this);
            Users = new Users(this);
            Videos = new Videos(this);
            Webhooks = new Webhooks(this);
            Debugging = new Debugging();
            Settings = new ApiSettings(this);
            _jsonSerializer = new TwitchLibJsonSerializer();
        }

        /// <summary>
        /// Initializes the Instance of the TwitchAPI Class.
        /// </summary>
        /// <param name="clientId">Twitch Client Id.</param>
        /// <param name="accessToken">Twitch Access Token.</param>
        public async Task InitializeAsync(string clientId = null, string accessToken = null)
        {
            if (!string.IsNullOrWhiteSpace(clientId))
                await Settings.SetClientIdAsync(clientId);
            if (!string.IsNullOrWhiteSpace(accessToken))
                await Settings.SetAccessTokenAsync(accessToken);
        }


        #region Requests

        #region TwitchResources
        internal async Task<T> TwitchGetGenericAsync<T>(string resource, ApiVersion api, List <KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            string url = constructResourceUrl(resource, getParams, api, customBase);
            return await _rateLimiter.Perform(async () =>
                JsonConvert.DeserializeObject<T>((await GeneralRequestAsync(url, HttpMethod.Get, null, accessToken, api, clientId)).Value, _twitchLibJsonDeserializer));
        }

        internal async Task<string> TwitchDeleteAsync(string resource, ApiVersion api, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            return await _rateLimiter.Perform(async () =>
            {
                string url = constructResourceUrl(resource, getParams, api, customBase);
                return (await GeneralRequestAsync(url, HttpMethod.Delete, null, accessToken, api, clientId)).Value; });
        }

        internal async Task<T> TwitchPostGenericAsync<T>(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            string url = constructResourceUrl(resource, getParams, api, customBase);
            return await _rateLimiter.Perform(async () =>
                JsonConvert.DeserializeObject<T>((await GeneralRequestAsync(url, HttpMethod.Post, payload, accessToken, api, clientId)).Value, _twitchLibJsonDeserializer));
        }

        internal async Task<T> TwitchPostGenericModelAsync<T>(string resource, ApiVersion api, Models.RequestModel model, string accessToken = null, string clientId = null, string customBase = null)
        {
            string url = constructResourceUrl(resource, api: api, overrideUrl: customBase);
            return await _rateLimiter.Perform(async () => JsonConvert.DeserializeObject<T>(model != null
                ? (await GeneralRequestAsync(url, HttpMethod.Post, _jsonSerializer.SerializeObject(model), accessToken, api, clientId)).Value
                : (await GeneralRequestAsync(url, HttpMethod.Post, "", accessToken, api)).Value, _twitchLibJsonDeserializer));
        }

        internal async Task<T> TwitchDeleteGenericAsync<T>(string resource, ApiVersion api, string accessToken = null, string clientId = null, string customBase = null)
        {
            string url = constructResourceUrl(resource, null, api, customBase);
            return await _rateLimiter.Perform(async () =>
                JsonConvert.DeserializeObject<T>((await GeneralRequestAsync(url, HttpMethod.Delete, null, accessToken, api, clientId)).Value, _twitchLibJsonDeserializer));
        }

        internal async Task<T> TwitchPutGenericAsync<T>(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            string url = constructResourceUrl(resource, getParams, api, customBase);
            return await _rateLimiter.Perform(async () =>
                JsonConvert.DeserializeObject<T>((await GeneralRequestAsync(url, HttpMethod.Put, payload, accessToken, api, clientId)).Value, _twitchLibJsonDeserializer));
        }

        internal async Task<string> TwitchPutAsync(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            string url = constructResourceUrl(resource, getParams, api, customBase);
            return await _rateLimiter.Perform(async () =>
            {
                return (await GeneralRequestAsync(url, HttpMethod.Put, payload, accessToken, api, clientId)).Value;
            });
        }

        internal async Task<KeyValuePair<int, string>> TwitchPostAsync(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            string url = constructResourceUrl(resource, getParams, api, customBase);
            return await _rateLimiter.Perform(async () => await GeneralRequestAsync(url, HttpMethod.Post, payload, accessToken, api, clientId));
        }

        private string constructResourceUrl(string resource = null, List<KeyValuePair<string, string>> getParams = null, ApiVersion api = ApiVersion.v5, string overrideUrl = null)
        {
            string url = "";
            if(overrideUrl == null)
            {
                if (resource == null)
                    throw new Exception("Cannot pass null resource with null override url");
                switch (api)
                {
                    case ApiVersion.v5:
                        url = $"{baseV5}{resource}";
                        break;
                    case ApiVersion.Helix:
                        url = $"{baseHelix}{resource}";
                        break;
                }
            } else
            {
                if (resource == null)
                    url = overrideUrl;
                else
                    url = $"{overrideUrl}{resource}";
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
        #endregion

        #region GET
        #region GetGenericAsync
        internal async Task<T> GetGenericAsync<T>(string url, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, ApiVersion api = ApiVersion.v5, string clientId = null)
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
            return await _rateLimiter.Perform(async () =>
                JsonConvert.DeserializeObject<T>((await GeneralRequestAsync(url, HttpMethod.Get, null, accessToken, api, clientId)).Value, _twitchLibJsonDeserializer));
        }
        #endregion
        #region GetSimpleGenericAsync
        internal async Task<T> GetSimpleGenericAsync<T>(string url, List<KeyValuePair<string, string>> getParams = null)
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
            return await _rateLimiter.Perform(async () => JsonConvert.DeserializeObject<T>(await SimpleRequestAsync(url), _twitchLibJsonDeserializer));
        }
        #endregion
        #endregion
        #region PutBytesAsync
        internal async void PutBytes(string url, byte[] payload)
        {
            var response = await _http.PutAsync(new Uri(url), new ByteArrayContent(payload));

            if (!response.IsSuccessStatusCode)
                HandleWebException(response);
        }
        #endregion

        #region GeneralRequestAsync
        private async Task<KeyValuePair<int, string>> GeneralRequestAsync(string url, HttpMethod method, string payload = null, string accessToken = null, ApiVersion api = ApiVersion.v5, string clientId = null)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Headers =
                {
                    {HttpRequestHeader.ContentType.ToString(), "application/json"},
                },
                Method = method
            };

            if (string.IsNullOrEmpty(clientId))
                CheckForCredentials(accessToken);
            if (!string.IsNullOrEmpty(clientId) || !string.IsNullOrEmpty(Settings.ClientId))
            {
                request.Headers.Add("Client-ID", string.IsNullOrWhiteSpace(clientId) ? Settings.ClientId : clientId);
            }

            var authPrefix = "OAuth";
            if (api == ApiVersion.Helix)
            {
                request.Headers.Add(HttpRequestHeader.Accept.ToString(), "application/json");
                authPrefix = "Bearer";
            }
            else if (api != ApiVersion.Void)
            {
                request.Headers.Add(HttpRequestHeader.Accept.ToString(), $"application/vnd.twitchtv.v{(int)api}+json");
            }
            if (!string.IsNullOrEmpty(accessToken))
                request.Headers.Add(HttpRequestHeader.Authorization.ToString(), $"{authPrefix} {Common.Helpers.FormatOAuth(accessToken)}");
            else if (!string.IsNullOrEmpty(Settings.AccessToken))
                request.Headers.Add(HttpRequestHeader.Authorization.ToString(), $"{authPrefix} {Settings.AccessToken}");

            if (payload != null)
                request.Content = new StringContent(payload);

            var response = await _http.SendAsync(request);
            if (response.IsSuccessStatusCode)
                return new KeyValuePair<int, string>((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            HandleWebException(response);
            return new KeyValuePair<int, string>(0, null);
        }
        #endregion
        #region SimpleRequestAsync
        // credit: https://stackoverflow.com/questions/14290988/populate-and-return-entities-from-downloadstringcompleted-handler-in-windows-pho
        private async Task<string> SimpleRequestAsync(string url)
        {
            var tcs = new TaskCompletionSource<string>();
            var client = new WebClient();

            client.DownloadStringCompleted += DownloadStringCompletedEventHandler;
            client.DownloadString(new Uri(url));

            return await tcs.Task;

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
        #endregion
        #region requestReturnResponseCode
        internal async Task<int> RequestReturnResponseCode(string url, HttpMethod method, List<KeyValuePair<string, string>> getParams = null)
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

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = method
            };
            var response = await _http.SendAsync(request);
            return (int)response.StatusCode;
        }
        #endregion

        #region AppendClientId
        private string AppendClientId(string url, string clientId = null)
        {
            if (clientId == null)
                return url.Contains("?")
                    ? $"{url}&client_id={Settings.ClientId}"
                    : $"{url}?client_id={Settings.ClientId}";

            return url.Contains("?")
                ? $"{url}&client_id={clientId}"
                : $"{url}?client_id={clientId}";
        }
        #endregion
        #region checkForCredentials
        private void CheckForCredentials(string passedAccessToken)
        {
            if (string.IsNullOrEmpty(Settings.ClientId) && string.IsNullOrWhiteSpace(Settings.AccessToken) && string.IsNullOrEmpty(passedAccessToken))
                throw new InvalidCredentialException("All API calls require Client-Id or OAuth token. Set Client-Id by using SetClientId(\"client_id_here\")");
        }
        #endregion

        #region HandleWebException
        private void HandleWebException(HttpResponseMessage errorResp)
        {
            switch (errorResp.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException("Your request failed because either: \n 1. Your ClientID was invalid/not set. \n 2. Your refresh token was invalid. \n 3. You requested a username when the server was expecting a user ID.");
                case HttpStatusCode.Unauthorized:
                    var authenticateHeader = errorResp.Headers.WwwAuthenticate;
                    if (authenticateHeader == null || authenticateHeader.Count <= 0)
                        throw new BadScopeException("Your request was blocked due to bad credentials (Do you have the right scope for your access token?).");
                    else
                        throw new TokenExpiredException("Your request was blocked due to an expired Token. Please refresh your token and update your API instance settings.");
                case HttpStatusCode.NotFound:
                    throw new BadResourceException("The resource you tried to access was not valid.");
                case (HttpStatusCode)422:
                    throw new NotPartneredException("The resource you requested is only available to channels that have been partnered by Twitch.");
                case HttpStatusCode.BadGateway:
                    throw new BadGatewayException("The API answered with a 502 Bad Gateway. Please retry your request");
                case HttpStatusCode.GatewayTimeout:
                    throw new GatewayTimeoutException("The API answered with a 504 Gateway Timeout. Please retry your request");
                case HttpStatusCode.InternalServerError:
                    throw new InternalServerErrorException("The API answered with a 500 Internal Server Error. Please retry your request");
                default:
                    throw new HttpRequestException("Something went wrong during the request! Please try again later");
            }
        }
        #endregion

        #region SerialiazationSettings

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
            #endregion

        #endregion
        }
    }
}
