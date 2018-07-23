using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Api.Services.Events;

namespace TwitchLib.Api.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Core.LiveStreamMonitor;
    using Events.LiveStreamMonitor;
    using Helix.Models.Streams;
    using Interfaces;

    public class ApiService
    {
        protected readonly ITwitchAPI _api;
        protected readonly ServiceTimer _serviceTimer;

        public List<string> ChannelsToMonitor { get; private set; }
        public int IntervalInSeconds => _serviceTimer.IntervalInSeconds;
        public bool Enabled => _serviceTimer.Enabled;

        public event EventHandler<OnServiceStartedArgs> OnServiceStarted;
        public event EventHandler<OnServiceStoppedArgs> OnServiceStopped;
        public event EventHandler<OnServiceTickArgs> OnServiceTick;

        public ApiService(ITwitchAPI api, int checkIntervalInSeconds)
        {
            if (api == null)
                throw new ArgumentNullException(nameof(api));

            if (checkIntervalInSeconds < 1)
                throw new ArgumentException("The interval must be 1 second or more.", nameof(checkIntervalInSeconds));

            _api = api;
            _serviceTimer = new ServiceTimer(OnServiceTimerTick, checkIntervalInSeconds);
        }

        public virtual void Start()
        {
            if (ChannelsToMonitor == null)
                throw new InvalidOperationException("You must atleast add 1 channel to monitor before starting the Service.");

            if (_serviceTimer.Enabled)
                throw new InvalidOperationException("The service has already been started.");

            _serviceTimer.Start();

            OnServiceStarted?.Invoke(this, new OnServiceStartedArgs());
        }

        public virtual void Stop()
        {
            if (!_serviceTimer.Enabled)
                throw new InvalidOperationException("The service hasn't started yet, or has already been stopped.");

            _serviceTimer.Stop();

            OnServiceStopped?.Invoke(this, new OnServiceStoppedArgs());
        }
        
        protected virtual void SetChannels(List<string> channelsToMonitor)
        {
            if (channelsToMonitor == null)
                throw new ArgumentNullException(nameof(channelsToMonitor));

            if (channelsToMonitor.Count == 0)
                throw new ArgumentException("The provided list is empty.", nameof(channelsToMonitor));

            ChannelsToMonitor = channelsToMonitor;
        }
        
        protected virtual Task OnServiceTimerTick(bool callEvents = true)
        {
            OnServiceTick?.Invoke(this, new OnServiceTickArgs());
            return Task.CompletedTask;
        }
    }
}
