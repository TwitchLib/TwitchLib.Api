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
        }
    }
}
