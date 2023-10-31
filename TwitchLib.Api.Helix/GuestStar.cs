using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Helix;

public class GuestStar : ApiBase
{
    public GuestStar(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
    {
    }
    
    
}