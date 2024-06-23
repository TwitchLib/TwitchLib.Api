using System;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchLib.Api.Core.Interfaces
{
    /// <summary>
    /// Interface for Awaitable Constraint
    /// </summary>
    public interface IAwaitableConstraint
    {
        Task<IDisposable> WaitForReadiness(CancellationToken cancellationToken);
    }
}
