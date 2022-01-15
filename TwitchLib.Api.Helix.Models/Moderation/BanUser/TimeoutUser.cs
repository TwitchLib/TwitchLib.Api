using System;

namespace TwitchLib.Api.Helix.Models.Moderation.BanUser
{
    public class TimeoutUser
    {
        public string UserId;
        public string Reason;
        public TimeSpan Duration; 
    }
}
