using System;
using System.Collections.Generic;

namespace TwitchLib.Api.Services.Events.FollowerService
{
    /// <inheritdoc />
    /// <summary>Class representing event args for OnServiceStarted event.</summary>
    public class OnServiceStartedArgs : EventArgs
    {
        /// <summary>Event property representing channel ids the service is currently monitoring.</summary>
        public ICollection<string> ChannelIds;
    }
}
