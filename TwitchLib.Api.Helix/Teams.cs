using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Teams;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Teams related APIs
    /// </summary>
    public class Teams : ApiBase
    {
        public Teams(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        /// <summary>
        /// Retrieves a list of Twitch Teams of which the specified channel/broadcaster is a member.
        /// </summary>
        /// <param name="broadcasterId">User ID for a Twitch user.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetChannelTeamsResponse"></returns>
        public Task<GetChannelTeamsResponse> GetChannelTeamsAsync(string broadcasterId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetGenericAsync<GetChannelTeamsResponse>("/teams/channel", ApiVersion.Helix, getParams, accessToken);
        }

        /// <summary>
        /// Gets information for a specific Twitch Team.
        /// </summary>
        /// <param name="teamId">Team ID to lookup.</param>
        /// <param name="teamName">Team name to lookup.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetTeamsResponse"></returns>
        public Task<GetTeamsResponse> GetTeamsAsync(string teamId = null, string teamName = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrWhiteSpace(teamId))
                getParams.Add(new KeyValuePair<string, string>("id", teamId));

            if (!string.IsNullOrWhiteSpace(teamName))
                getParams.Add(new KeyValuePair<string, string>("name", teamName));

            return TwitchGetGenericAsync<GetTeamsResponse>("/teams", ApiVersion.Helix, getParams, accessToken);
        }
    }
}