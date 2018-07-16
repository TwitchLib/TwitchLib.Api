using Microsoft.Extensions.Logging;
using TwitchLib.Api.Core.HttpCallHandlers;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.RateLimiter;
using TwitchLib.Api.Core.Undocumented;
using TwitchLib.Api.Interfaces;

namespace TwitchLib.Api
{
    public class TwitchAPI : ITwitchAPI
    {
        private readonly ILogger<TwitchAPI> _logger;
        public IApiSettings Settings { get; }
        public V5.V5 V5 { get; }
        public Helix.Helix Helix { get; }
        public ThirdParty.ThirdParty ThirdParty { get; }
        public Undocumented Undocumented { get; }

        /// <summary>
        /// Creates an Instance of the TwitchAPI Class.
        /// </summary>
        /// <param name="loggerFactory">Instance Of LoggerFactory, otherwise no logging is used, </param>
        /// <param name="rateLimiter">Instance Of RateLimiter, otherwise no ratelimiter is used.</param>
        /// <param name="settings">Instance of ApiSettings, otherwise defaults used, can be changed later</param>
        /// <param name="http">Instance of HttpCallHandler, otherwise default handler used</param>
        public TwitchAPI(ILoggerFactory loggerFactory = null, IRateLimiter rateLimiter = null, IApiSettings settings = null, IHttpCallHandler http = null)
        {
            _logger = loggerFactory?.CreateLogger<TwitchAPI>();
            var _rateLimiter = rateLimiter ?? BypassLimiter.CreateLimiterBypassInstance();
            var _http = http ?? new TwitchHttpClient(loggerFactory?.CreateLogger<TwitchHttpClient>());
            Settings = settings ?? new ApiSettings();

            Helix = new Helix.Helix(loggerFactory, rateLimiter, Settings, _http);
            V5 = new V5.V5(loggerFactory, _rateLimiter, Settings, _http);
            ThirdParty = new ThirdParty.ThirdParty(Settings, _rateLimiter, _http);
            Undocumented = new Undocumented(Settings, _rateLimiter, _http);

            settings.PropertyChanged += Settings_PropertyChanged;
        }

        private void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "AccessToken":
                    V5.Settings.AccessToken = Settings.AccessToken;
                    Helix.Settings.AccessToken = Settings.AccessToken;
                    break;
                case "Secret":
                    V5.Settings.Secret = Settings.Secret;
                    Helix.Settings.Secret = Settings.Secret;
                    break;
                case "ClientId":
                    V5.Settings.ClientId = Settings.ClientId;
                    Helix.Settings.ClientId = Settings.ClientId;
                    break;
                case "SkipDynamicScopeValidation":
                    V5.Settings.SkipDynamicScopeValidation = Settings.SkipDynamicScopeValidation;
                    Helix.Settings.SkipDynamicScopeValidation = Settings.SkipDynamicScopeValidation;
                    break;
                case "SkipAutoServerTokenGeneration":
                    V5.Settings.SkipAutoServerTokenGeneration = Settings.SkipAutoServerTokenGeneration;
                    Helix.Settings.SkipAutoServerTokenGeneration = Settings.SkipAutoServerTokenGeneration;
                    break;
                case "Scopes":
                    V5.Settings.Scopes = Settings.Scopes;
                    Helix.Settings.Scopes = Settings.Scopes;
                    break;
            }
            throw new System.NotImplementedException();
        }
    }
}
