using Microsoft.Extensions.Logging;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.HttpCallHandlers;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.RateLimiter;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Contains APIs under the /helix API namespace
    /// </summary>
    public class Helix
    {
        private readonly ILogger<Helix> _logger;
        /// <summary>
        /// API Settings like the ClientId, Client Secret and so on
        /// </summary>
        public IApiSettings Settings { get; }
        /// <summary>
        /// Analytics related Helix APIs
        /// </summary>
        public Analytics Analytics { get; }
        /// <summary>
        /// Ads related Helix APIs
        /// </summary>
        public Ads Ads { get; }
        /// <summary>
        /// Bits related Helix APIs
        /// </summary>
        public Bits Bits { get; }
        /// <summary>
        /// Chat related Helix APIs
        /// </summary>
        public Chat Chat { get; }
        /// <summary>
        /// Channel related Helix APIs
        /// </summary>
        public Channels Channels { get; }
        /// <summary>
        /// Channel Points related Helix APIs
        /// </summary>
        public ChannelPoints ChannelPoints { get; }
        /// <summary>
        /// Charity related Helix APIs
        /// </summary>
        public Charity Charity { get; }
        /// <summary>
        /// Clips related Helix APIs
        /// </summary>
        public Clips Clips { get; }
        /// <summary>
        /// Entitlements related Helix APIs
        /// </summary>
        public Entitlements Entitlements { get; }
        /// <summary>
        /// EventSub related Helix APIs
        /// </summary>
        public EventSub EventSub { get; }
        /// <summary>
        /// Extensions related Helix APIs
        /// </summary>
        public Extensions Extensions { get; }
        /// <summary>
        /// Games related Helix APIs
        /// </summary>
        public Games Games { get; }
        /// <summary>
        /// Creator Goals related Helix APIs
        /// </summary>
        public Goals Goals { get; }
        /// <summary>
        /// HypeTrain related Helix APIs
        /// </summary>
        public HypeTrain HypeTrain { get; }
        /// <summary>
        /// Moderation related Helix APIs
        /// </summary>
        public Moderation Moderation { get; }
        /// <summary>
        /// Polls related Helix APIs
        /// </summary>
        public Polls Polls { get; }
        /// <summary>
        /// Prediction related Helix APIs
        /// </summary>
        public Predictions Predictions { get; }
        /// <summary>
        /// Raids related Helix APIs
        /// </summary>
        public Raids Raids { get; }
        /// <summary>
        /// Schedule related Helix APIs
        /// </summary>
        public Schedule Schedule { get; }
        /// <summary>
        /// Search related Helix APIs
        /// </summary>
        public Search Search { get; }
        /// <summary>
        /// Soundtrack related Helix APIs
        /// </summary>
        public Soundtrack Soundtrack { get; }
        /// <summary>
        /// Stream related Helix APIs
        /// </summary>
        public Streams Streams { get; }
        /// <summary>
        /// Subscription related Helix APIs
        /// </summary>
        public Subscriptions Subscriptions { get; }
        /// <summary>
        /// Tag related Helix APIs
        /// </summary>
        public Tags Tags { get; }
        /// <summary>
        /// Stream Team related Helix APIs
        /// </summary>
        public Teams Teams { get; }
        /// <summary>
        /// Video/VOD related Helix APIs
        /// </summary>
        public Videos Videos { get; }
        /// <summary>
        /// User related Helix APIs
        /// </summary>
        public Users Users { get; }
        /// <summary>
        /// Whisper related Helix APIs
        /// </summary>
        public Whispers Whispers { get; }

        /// <summary>
        /// Creates an Instance of the Helix Class.
        /// </summary>
        /// <param name="loggerFactory">Instance Of LoggerFactory, otherwise no logging is used, </param>
        /// <param name="rateLimiter">Instance Of RateLimiter, otherwise no ratelimiter is used.</param>
        /// <param name="settings">Instance of ApiSettings, otherwise defaults used, can be changed later</param>
        /// <param name="http">Instance of HttpCallHandler, otherwise default handler used</param>
        public Helix(ILoggerFactory loggerFactory = null, IRateLimiter rateLimiter = null, IApiSettings settings = null, IHttpCallHandler http = null, IUserAccessTokenManager userAccessTokenManager = null)
        {
            _logger = loggerFactory?.CreateLogger<Helix>();
            rateLimiter = rateLimiter ?? BypassLimiter.CreateLimiterBypassInstance();
            http = http ?? new TwitchHttpClient(loggerFactory?.CreateLogger<TwitchHttpClient>());
            Settings = settings ?? new ApiSettings();

            Analytics = new Analytics(Settings, rateLimiter, http, userAccessTokenManager);
            Ads = new Ads(Settings, rateLimiter, http, userAccessTokenManager);
            Bits = new Bits(Settings, rateLimiter, http, userAccessTokenManager);
            Chat = new Chat(Settings, rateLimiter, http, userAccessTokenManager);
            Channels = new Channels(Settings, rateLimiter, http, userAccessTokenManager);
            ChannelPoints = new ChannelPoints(Settings, rateLimiter, http, userAccessTokenManager);
            Charity = new Charity(Settings, rateLimiter, http, userAccessTokenManager);
            Clips = new Clips(Settings, rateLimiter, http, userAccessTokenManager);
            Entitlements = new Entitlements(Settings, rateLimiter, http, userAccessTokenManager);
            EventSub = new EventSub(Settings, rateLimiter, http, userAccessTokenManager);
            Extensions = new Extensions(Settings, rateLimiter, http, userAccessTokenManager);
            Games = new Games(Settings, rateLimiter, http, userAccessTokenManager);
            Goals = new Goals(settings, rateLimiter, http, userAccessTokenManager);
            HypeTrain = new HypeTrain(Settings, rateLimiter, http, userAccessTokenManager);
            Moderation = new Moderation(Settings, rateLimiter, http, userAccessTokenManager);
            Polls = new Polls(Settings, rateLimiter, http, userAccessTokenManager);
            Predictions = new Predictions(Settings, rateLimiter, http, userAccessTokenManager);
            Raids = new Raids(settings, rateLimiter, http, userAccessTokenManager);
            Schedule = new Schedule(Settings, rateLimiter, http, userAccessTokenManager);
            Search = new Search(Settings, rateLimiter, http, userAccessTokenManager);
            Soundtrack = new Soundtrack(Settings, rateLimiter, http, userAccessTokenManager);
            Streams = new Streams(Settings, rateLimiter, http, userAccessTokenManager);
            Subscriptions = new Subscriptions(Settings, rateLimiter, http, userAccessTokenManager);
            Tags = new Tags(Settings, rateLimiter, http, userAccessTokenManager);
            Teams = new Teams(Settings, rateLimiter, http, userAccessTokenManager);
            Users = new Users(Settings, rateLimiter, http, userAccessTokenManager);
            Videos = new Videos(Settings, rateLimiter, http, userAccessTokenManager);
            Whispers = new Whispers(Settings, rateLimiter, http, userAccessTokenManager);
        }
    }
}
