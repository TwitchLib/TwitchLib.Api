using Microsoft.Extensions.Logging;
using TwitchLib.Api.Core.HttpCallHandlers;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.RateLimiter;

namespace TwitchLib.Api.V5
{
    public class V5 
    {
        private readonly ILogger<V5> _logger;
        public IApiSettings Settings { get; }
        public Auth Auth { get; }
        public Badges Badges { get; }
        public Bits Bits { get; }
        public Channels Channels { get; }
        public Chat Chat { get; }
        public Clips Clips { get; }
        public Collections Collections { get; }
        public Communities Communities { get; }
        public Games Games { get; }
        public Ingests Ingests { get; }
        public Search Search { get; }
        public Streams Streams { get; }
        public Teams Teams { get; }
        public Videos Videos { get; }
        public Users Users { get; }


        /// <summary>
        /// Creates an Instance of the V5 Class.
        /// </summary>
        /// <param name="loggerFactory">Instance Of LoggerFactory, otherwise no logging is used, </param>
        /// <param name="rateLimiter">Instance Of RateLimiter, otherwise no ratelimiter is used.</param>
        /// <param name="settings">Instance of ApiSettings, otherwise defaults used, can be changed later</param>
        /// <param name="http">Instance of HttpCallHandler, otherwise default handler used</param>
        public V5(ILoggerFactory loggerFactory = null, IRateLimiter rateLimiter = null, IApiSettings settings = null, IHttpCallHandler http = null)
        {
            _logger = loggerFactory?.CreateLogger<V5>();
            var _rateLimiter = rateLimiter ?? BypassLimiter.CreateLimiterBypassInstance();
            var _http = http ?? new TwitchHttpClient(loggerFactory?.CreateLogger<TwitchHttpClient>());
            Settings = settings ?? new ApiSettings();
            
            Auth = new Auth(Settings, _rateLimiter, _http);
            Badges = new Badges(Settings, _rateLimiter, _http);
            Bits = new Bits(Settings, _rateLimiter, _http);
            Channels = new Channels(Settings, _rateLimiter, _http);
            Chat = new Chat(Settings, _rateLimiter, _http);
            Clips = new Clips(Settings, _rateLimiter, _http);
            Collections = new Collections(Settings, _rateLimiter, _http);
            Communities = new Communities(Settings, _rateLimiter, _http);
            Games = new Games(Settings, _rateLimiter, _http);
            Ingests = new Ingests(Settings, _rateLimiter, _http);
            Search = new Search(Settings, _rateLimiter, _http);
            Streams = new Streams(Settings, _rateLimiter, _http);
            Teams = new Teams(Settings, _rateLimiter, _http);
            Users = new Users(Settings, _rateLimiter, _http);
            Videos = new Videos(Settings, _rateLimiter, _http);
        }
    }
}
