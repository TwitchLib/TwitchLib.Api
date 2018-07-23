using System;
using TwitchLib.Api.Helix.Models.Streams;

namespace TwitchLib.Api.Services.Events.LiveStreamMonitor
{
    /// <inheritdoc />
    /// <summary>Class representing event args for OnChannelOnline event.</summary>
    public class OnStreamUpdateArgs : EventArgs
    {
        /// <summary>Event property representing channel that has gone online.</summary>
        public string Channel;
        /// <summary>Event property representing live stream information.</summary>
        public Stream Stream;
    }
}
