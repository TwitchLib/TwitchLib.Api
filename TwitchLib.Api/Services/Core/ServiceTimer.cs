using System.Threading.Tasks;
using System.Timers;

namespace TwitchLib.Api.Services.Core
{
    internal class ServiceTimer : Timer
    {
        public int IntervalInSeconds { get; }

        public delegate Task ServiceTimerTick();

        private readonly ServiceTimerTick _serviceTimerTickAsyncCallback;

        public ServiceTimer(ServiceTimerTick serviceTimerTickAsyncCallback, int intervalInSeconds = 60)
        {
            _serviceTimerTickAsyncCallback = serviceTimerTickAsyncCallback;
            Interval = intervalInSeconds * 1000;
            IntervalInSeconds = intervalInSeconds;
            Elapsed += TimerElapsedAsync;
        }

        private async void TimerElapsedAsync(object sender, ElapsedEventArgs e)
        {
            await _serviceTimerTickAsyncCallback().ConfigureAwait(false);
        }
    }
}
