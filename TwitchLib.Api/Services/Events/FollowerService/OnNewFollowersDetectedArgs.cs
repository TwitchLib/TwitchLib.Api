using System;
using System.Collections.Generic;
using TwitchLib.Api.Interfaces;

namespace TwitchLib.Api.Services.Events.FollowerService
{
    /// <inheritdoc />
    /// <summary>Class representing event args for OnNewFollowersDetected event.</summary>
    public class OnNewFollowersDetectedArgs : EventArgs
    {
        /// <summary>Event property representing channel the service is currently monitoring.</summary>
        public string ChannelId;
        /// <summary>Event property representing all new followers detected.</summary>
        public List<IFollow> NewFollowers;
    }
}
