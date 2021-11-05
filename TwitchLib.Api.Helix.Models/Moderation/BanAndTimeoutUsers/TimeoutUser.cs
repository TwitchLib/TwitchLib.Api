using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Moderation.BanAndTimeoutUsers
{
    public class TimeoutUser
    {
        public string UserId;
        public string Reason;
        public TimeSpan Duration; 
    }
}
