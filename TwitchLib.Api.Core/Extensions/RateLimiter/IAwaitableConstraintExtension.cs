using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.RateLimiter;

namespace TwitchLib.Api.Core.Extensions.RateLimiter;

/// <summary>
/// IAwaitable Constraint Extension
/// </summary>
public static class IAwaitableConstraintExtension
{
    public static IAwaitableConstraint Compose(this IAwaitableConstraint ac1, IAwaitableConstraint ac2)
    {
        return ac1 == ac2 ? ac1 : new ComposedAwaitableConstraint(ac1, ac2);
    }
}
