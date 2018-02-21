using System;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchLib.Api.Interfaces
{
    public interface ITime
    {
        DateTime GetTimeNow();

        Task GetDelay(TimeSpan timespan, CancellationToken cancellationToken);
    }
}
