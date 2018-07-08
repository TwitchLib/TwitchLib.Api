using Microsoft.Extensions.Logging;
using TwitchLib.Api.HttpCallHandlers;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.RateLimiter;
using TwitchLib.Api.Sections;

namespace TwitchLib.Api
{
    public class TwitchAPI : ITwitchAPI
    {
        private readonly ILogger<TwitchAPI> _logger;
        public IApiSettings Settings { get; }
        public Analytics Analytics { get; }
        public Auth Auth { get; }
        public Badges Badges { get; }
        public Bits Bits { get; }
        public ChannelFeeds ChannelFeeds { get; }
        public Channels Channels { get; }
        public Chat Chat { get; }
        public Clips Clips { get; }
        public Collections Collections { get; }
        public Communities Communities { get; }
        public Entitlements Entitlements { get; }
        public Games Games { get; }
        public Ingests Ingests { get; }
        public Search Search { get; }
        public Streams Streams { get; }
        public Teams Teams { get; }
        public Videos Videos { get; }
        public Users Users { get; }
        public Undocumented Undocumented { get; }
        public ThirdParty ThirdParty { get; }
        public Webhooks Webhooks { get; }


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

            Analytics = new Analytics(Settings, _rateLimiter, _http);
            Auth = new Auth(Settings, _rateLimiter, _http);
            Badges = new Badges(Settings, _rateLimiter, _http);
            Bits = new Bits(Settings, _rateLimiter, _http);
            ChannelFeeds = new ChannelFeeds(Settings, _rateLimiter, _http);
            Channels = new Channels(Settings, _rateLimiter, _http);
            Chat = new Chat(Settings, _rateLimiter, _http);
            Clips = new Clips(Settings, _rateLimiter, _http);
            Collections = new Collections(Settings, _rateLimiter, _http);
            Communities = new Communities(Settings, _rateLimiter, _http);
            Entitlements = new Entitlements(Settings, _rateLimiter, _http);
            Games = new Games(Settings, _rateLimiter, _http);
            Ingests = new Ingests(Settings, _rateLimiter, _http);
            Search = new Search(Settings, _rateLimiter, _http);
            Streams = new Streams(Settings, _rateLimiter, _http);
            Teams = new Teams(Settings, _rateLimiter, _http);
            ThirdParty = new ThirdParty(Settings, _rateLimiter, _http);
            Undocumented = new Undocumented(Settings, _rateLimiter, _http);
            Users = new Users(Settings, _rateLimiter, _http);
            Videos = new Videos(Settings, _rateLimiter, _http);
            Webhooks = new Webhooks(Settings, _rateLimiter, _http);
        }
    }
}
