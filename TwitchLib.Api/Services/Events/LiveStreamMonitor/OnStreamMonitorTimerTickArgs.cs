using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Services.Events.LiveStreamMonitor
{
    public class OnStreamMonitorTimerTickArgs : EventArgs
    {
        /// <summary>Event property representing channel Id that has gone online.</summary>
        public int CheckIntervalSeconds;
    }
}
