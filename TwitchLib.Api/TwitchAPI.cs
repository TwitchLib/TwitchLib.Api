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
using System.Text;

namespace TwitchLib.Api
{
    public class TwitchAPI : ITwitchAPI
    {
        private readonly ILogger<TwitchAPI> _logger;
        private readonly IHttpCallHandler _http;
        private readonly TwitchLibJsonSerializer _jsonSerializer;
        private readonly IRateLimiter _rateLimiter;

        internal const string BaseV5 = "https://api.twitch.tv/kraken";
        internal const string BaseHelix = "https://api.twitch.tv/helix";

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
        public TwitchAPI(ILoggerFactory loggerFactory = null, IRateLimiter rateLimiter = null, IHttpCallHandler http = null)
        {
            _logger = loggerFactory?.CreateLogger<TwitchAPI>();
            _http = http ?? new TwitchHttpClient(loggerFactory?.CreateLogger<TwitchHttpClient>());
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
        
        #region Requests

        #region TwitchResources
        internal Task<T> TwitchGetGenericAsync<T>(string resource, ApiVersion api, List <KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            string url = ConstructResourceUrl(resource, getParams, api, customBase);
            
            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;
            if (string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(Settings.AccessToken))
                accessToken = Settings.AccessToken;

            return _rateLimiter.Perform(() => JsonConvert.DeserializeObject<T>(_http.GeneralRequestAsync(url, "GET", null, api, clientId, accessToken).Value, _twitchLibJsonDeserializer));
        }

        internal Task<string> TwitchDeleteAsync(string resource, ApiVersion api, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            string url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;
            if (string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(Settings.AccessToken))
                accessToken = Settings.AccessToken;

            return _rateLimiter.Perform(() => _http.GeneralRequestAsync(url, "DELETE", null, api, clientId, accessToken).Value);
        }

        internal Task<T> TwitchPostGenericAsync<T>(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            string url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;
            if (string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(Settings.AccessToken))
                accessToken = Settings.AccessToken;

            return _rateLimiter.Perform(() => JsonConvert.DeserializeObject<T>(_http.GeneralRequestAsync(url, "POST", payload, api, clientId, accessToken).Value, _twitchLibJsonDeserializer));
        }

        internal Task<T> TwitchPostGenericModelAsync<T>(string resource, ApiVersion api, Models.RequestModel model, string accessToken = null, string clientId = null, string customBase = null)
        {
            string url = ConstructResourceUrl(resource, api: api, overrideUrl: customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;
            if (string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(Settings.AccessToken))
                accessToken = Settings.AccessToken;

            return _rateLimiter.Perform(() => JsonConvert.DeserializeObject<T>(_http.GeneralRequestAsync(url, "POST", model != null ? _jsonSerializer.SerializeObject(model) : "", api, clientId, accessToken).Value, _twitchLibJsonDeserializer));
        }

        internal Task<T> TwitchDeleteGenericAsync<T>(string resource, ApiVersion api, string accessToken = null, string clientId = null, string customBase = null)
        {
            string url = ConstructResourceUrl(resource, null, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;
            if (string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(Settings.AccessToken))
                accessToken = Settings.AccessToken;

            return _rateLimiter.Perform(() => JsonConvert.DeserializeObject<T>(_http.GeneralRequestAsync(url, "DELETE", null, api, clientId, accessToken).Value, _twitchLibJsonDeserializer));
        }

        internal Task<T> TwitchPutGenericAsync<T>(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            string url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;
            if (string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(Settings.AccessToken))
                accessToken = Settings.AccessToken;

            return _rateLimiter.Perform(() => JsonConvert.DeserializeObject<T>(_http.GeneralRequestAsync(url, "PUT", payload, api, clientId, accessToken).Value, _twitchLibJsonDeserializer));
        }

        internal Task<string> TwitchPutAsync(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            string url = ConstructResourceUrl(resource, getParams, api, customBase);
            
            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;
            if (string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(Settings.AccessToken))
                accessToken = Settings.AccessToken;

            return _rateLimiter.Perform(() => _http.GeneralRequestAsync(url, "PUT", payload, api, clientId, accessToken).Value);
        }

        internal Task<KeyValuePair<int, string>> TwitchPostAsync(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            string url = ConstructResourceUrl(resource, getParams, api, customBase);
            
            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;
            if (string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(Settings.AccessToken))
                accessToken = Settings.AccessToken;

            return _rateLimiter.Perform(() => _http.GeneralRequestAsync(url, "POST", payload, api, clientId, accessToken));
        }

        private string ConstructResourceUrl(string resource = null, List<KeyValuePair<string, string>> getParams = null, ApiVersion api = ApiVersion.v5, string overrideUrl = null)
        {
            string url = "";
            if(overrideUrl == null)
            {
                if (resource == null)
                    throw new Exception("Cannot pass null resource with null override url");
                switch (api)
                {
                    case ApiVersion.v5:
                        url = $"{BaseV5}{resource}";
                        break;
                    case ApiVersion.Helix:
                        url = $"{BaseHelix}{resource}";
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

        internal void PutBytes(string url, byte[] payload)
        {
            _http.PutBytes(url, payload);
        }

        internal int RequestReturnResponseCode(string url, string method, List<KeyValuePair<string, string>> getParams = null)
        {
            return _http.RequestReturnResponseCode(url, method, getParams);
        }

        #region GetGenericAsync
        internal Task<T> GetGenericAsync<T>(string url, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, ApiVersion api = ApiVersion.v5, string clientId = null)
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
            if (string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(Settings.AccessToken))
                accessToken = Settings.AccessToken;


            return _rateLimiter.Perform(() => JsonConvert.DeserializeObject<T>(_http.GeneralRequestAsync(url, "GET", null, api, clientId, accessToken).Value, _twitchLibJsonDeserializer));
        }
        #endregion

        #region GetSimpleGenericAsync
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
        #endregion
        #endregion
       
      
        #region SimpleRequestAsync
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
