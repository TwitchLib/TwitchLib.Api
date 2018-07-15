using Microsoft.Extensions.Logging;
using TwitchLib.Api.Core.HttpCallHandlers;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.RateLimiter;

namespace TwitchLib.Api.Helix
{
    public class Helix 
    {
        private readonly ILogger<Helix> _logger;
        public IApiSettings Settings { get; }
        public Analytics Analytics { get; }
     
        public Bits Bits { get; }
        public Clips Clips { get; }
        public Entitlements Entitlements { get; }
        public Games Games { get; }
        public Streams Streams { get; }
        public Videos Videos { get; }
        public Users Users { get; }
        public Webhooks Webhooks { get; }


        /// <summary>
        /// Creates an Instance of the Helix Class.
        /// </summary>
        /// <param name="loggerFactory">Instance Of LoggerFactory, otherwise no logging is used, </param>
        /// <param name="rateLimiter">Instance Of RateLimiter, otherwise no ratelimiter is used.</param>
        /// <param name="settings">Instance of ApiSettings, otherwise defaults used, can be changed later</param>
        /// <param name="http">Instance of HttpCallHandler, otherwise default handler used</param>
        public Helix(ILoggerFactory loggerFactory = null, IRateLimiter rateLimiter = null, IApiSettings settings = null, IHttpCallHandler http = null)
        {
            _logger = loggerFactory?.CreateLogger<Helix>();
            var _rateLimiter = rateLimiter ?? BypassLimiter.CreateLimiterBypassInstance();
            var _http = http ?? new TwitchHttpClient(loggerFactory?.CreateLogger<TwitchHttpClient>());
            Settings = settings ?? new ApiSettings();

            Analytics = new Analytics(Settings, _rateLimiter, _http);
            Bits = new Bits(Settings, _rateLimiter, _http);
            Clips = new Clips(Settings, _rateLimiter, _http);
            Entitlements = new Entitlements(Settings, _rateLimiter, _http);
            Games = new Games(Settings, _rateLimiter, _http);
            Streams = new Streams(Settings, _rateLimiter, _http);
            Users = new Users(Settings, _rateLimiter, _http);
            Videos = new Videos(Settings, _rateLimiter, _http);
            Webhooks = new Webhooks(Settings, _rateLimiter, _http);
        }
    }
}
