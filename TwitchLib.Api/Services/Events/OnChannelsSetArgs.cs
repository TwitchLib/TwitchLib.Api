using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace TwitchLib.Api.Services.Events.LiveStreamMonitor
{
    /// <inheritdoc />
    public class OnChannelsSetArgs : EventArgs
    {
        /// <summary>Event property representing channels the service is currently monitoring.</summary>
        public List<string> Channels;
    }
}