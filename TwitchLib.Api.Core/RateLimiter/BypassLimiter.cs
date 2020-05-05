using System;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.RateLimiter
{
    public class BypassLimiter : IRateLimiter
    {
        public Task PerformAsync(Func<Task> perform)
        {
            return PerformAsync(perform, CancellationToken.None);
        }

        public Task<T> PerformAsync<T>(Func<Task<T>> perform)
        {
            return PerformAsync(perform, CancellationToken.None);
        }

        public Task PerformAsync(Func<Task> perform, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return perform();
        }

        public Task<T> PerformAsync<T>(Func<Task<T>> perform, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return perform();
        }

        private static Func<Task> Transform(Action act)
        {
            return () => { act(); return Task.FromResult(0); };
        }

        private static Func<Task<T>> Transform<T>(Func<T> compute)
        {
            return () => Task.FromResult(compute());
        }

        public Task PerformAsync(Action perform, CancellationToken cancellationToken)
        {
            var transformed = Transform(perform);
            return PerformAsync(transformed, cancellationToken);
        }

        public Task PerformAsync(Action perform)
        {
            var transformed = Transform(perform);
            return PerformAsync(transformed);
        }

        public Task<T> PerformAsync<T>(Func<T> perform)
        {
            var transformed = Transform(perform);
            return PerformAsync(transformed);
        }

        public Task<T> PerformAsync<T>(Func<T> perform, CancellationToken cancellationToken)
        {
            var transformed = Transform(perform);
            return PerformAsync(transformed, cancellationToken);
        }

        public static BypassLimiter CreateLimiterBypassInstance()
        {
            return new BypassLimiter();
        }
    }
}
