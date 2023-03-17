using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Channels.GetChannelFollowers;
using TwitchLib.Api.Helix.Models.Users.GetUserFollows;
using TwitchLib.Api.Interfaces;

namespace TwitchLib.Api.Services.Core.FollowerService
{
    internal class IdBasedMonitor : CoreMonitor
    {
        public IdBasedMonitor(ITwitchAPI api) : base(api) { }

        public override Task<GetChannelFollowersResponse> GetUsersFollowsAsync(string channel, int queryCount)
        {
            return _api.Helix.Channels.GetChannelFollowersAsync(channel, null, queryCount);
        }
    }
}
