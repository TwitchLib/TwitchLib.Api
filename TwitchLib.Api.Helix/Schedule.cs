using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Helix
{
    public class Schedule : ApiBase
    {
        public Schedule(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        { }
    }
}