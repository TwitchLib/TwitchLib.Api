using System;
using System.Collections.Generic;

namespace TwitchLib.Api.Services.Events.FollowerService
{
    /// <inheritdoc />
    /// <summary>Class representing event args for OnServiceStopped event.</summary>
    public class OnServiceStoppedArgs : EventArgs
    {
        /// <summary>Event property representing channel ids the service is currently monitoring.</summary>
        public ICollection<string> ChannelIds;
    }
}