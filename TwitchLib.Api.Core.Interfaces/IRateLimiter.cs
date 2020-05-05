using System;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchLib.Api.Core.Interfaces
{
    public interface IRateLimiter
    {
        Task PerformAsync(Func<Task> perform, CancellationToken cancellationToken);

        Task PerformAsync(Func<Task> perform);

        Task<T> PerformAsync<T>(Func<Task<T>> perform);

        Task<T> PerformAsync<T>(Func<Task<T>> perform, CancellationToken cancellationToken);

        Task PerformAsync(Action perform, CancellationToken cancellationToken);

        Task PerformAsync(Action perform);

        Task<T> PerformAsync<T>(Func<T> perform);

        Task<T> PerformAsync<T>(Func<T> perform, CancellationToken cancellationToken);
    }
}
