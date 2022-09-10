
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Core.Models.Undocumented.Chatters
{
    public class ChatterFormatted
    {
        public string Username { get; protected set; }
        public UserType UserType { get;  set; }

        public ChatterFormatted(string username, UserType userType)
        {
            Username = username;
            UserType = userType;
        }

        
    }
}
