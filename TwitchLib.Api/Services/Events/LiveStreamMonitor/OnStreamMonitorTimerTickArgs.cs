using System;

namespace TwitchLib.Api.Services.Events.LiveStreamMonitor
{
    public class OnStreamMonitorTimerTickArgs : EventArgs
    {
        /// <summary>Event property representing channel Id that has gone online.</summary>
        public int CheckIntervalSeconds;
    }
}
