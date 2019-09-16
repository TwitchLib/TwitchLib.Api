using Microsoft.Extensions.Logging;
using TwitchLib.Api.Core;
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
        public Moderation Moderation { get; }
        public Subscriptions Subscriptions { get; }
        public Streams Streams { get; }
        public Tags Tags { get; }
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
            rateLimiter = rateLimiter ?? BypassLimiter.CreateLimiterBypassInstance();
            http = http ?? new TwitchHttpClient(loggerFactory?.CreateLogger<TwitchHttpClient>());
            Settings = settings ?? new ApiSettings();

            Analytics = new Analytics(Settings, rateLimiter, http);
            Bits = new Bits(Settings, rateLimiter, http);
            Clips = new Clips(Settings, rateLimiter, http);
            Entitlements = new Entitlements(Settings, rateLimiter, http);
            Games = new Games(Settings, rateLimiter, http);
            Moderation = new Moderation(Settings, rateLimiter, http);
            Streams = new Streams(Settings, rateLimiter, http);
            Subscriptions = new Subscriptions(Settings, rateLimiter, http);
            Tags = new Tags(Settings, rateLimiter, http);
            Users = new Users(Settings, rateLimiter, http);
            Videos = new Videos(Settings, rateLimiter, http);
            Webhooks = new Webhooks(Settings, rateLimiter, http);
        }
    }
}
