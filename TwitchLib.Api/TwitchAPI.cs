using Microsoft.Extensions.Logging;
using TwitchLib.Api.Core;
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
            rateLimiter = rateLimiter ?? BypassLimiter.CreateLimiterBypassInstance();
            http = http ?? new TwitchHttpClient(loggerFactory?.CreateLogger<TwitchHttpClient>());
            Settings = settings ?? new ApiSettings();

            Helix = new Helix.Helix(loggerFactory, rateLimiter, Settings, http);
            V5 = new V5.V5(loggerFactory, rateLimiter, Settings, http);
            ThirdParty = new ThirdParty.ThirdParty(Settings, rateLimiter, http);
            Undocumented = new Undocumented(Settings, rateLimiter, http);

            Settings.PropertyChanged += SettingsPropertyChanged;
        }

        private void SettingsPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IApiSettings.AccessToken):
                    V5.Settings.AccessToken = Settings.AccessToken;
                    Helix.Settings.AccessToken = Settings.AccessToken;
                    break;
                case nameof(IApiSettings.Secret):
                    V5.Settings.Secret = Settings.Secret;
                    Helix.Settings.Secret = Settings.Secret;
                    break;
                case nameof(IApiSettings.ClientId):
                    V5.Settings.ClientId = Settings.ClientId;
                    Helix.Settings.ClientId = Settings.ClientId;
                    break;
                case nameof(IApiSettings.SkipDynamicScopeValidation):
                    V5.Settings.SkipDynamicScopeValidation = Settings.SkipDynamicScopeValidation;
                    Helix.Settings.SkipDynamicScopeValidation = Settings.SkipDynamicScopeValidation;
                    break;
                case nameof(IApiSettings.SkipAutoServerTokenGeneration):
                    V5.Settings.SkipAutoServerTokenGeneration = Settings.SkipAutoServerTokenGeneration;
                    Helix.Settings.SkipAutoServerTokenGeneration = Settings.SkipAutoServerTokenGeneration;
                    break;
                case nameof(IApiSettings.Scopes):
                    V5.Settings.Scopes = Settings.Scopes;
                    Helix.Settings.Scopes = Settings.Scopes;
                    break;
            }
        }
    }
}
