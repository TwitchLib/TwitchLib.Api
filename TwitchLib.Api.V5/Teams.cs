using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.V5.Models.Teams;

namespace TwitchLib.Api.V5
{
    [Obsolete("This is a v5 class, please use a Helix class. All v5 calls will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
    public class Teams : ApiBase
    {
        public Teams(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetAllTeams
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task<AllTeams> GetAllTeamsAsync(int? limit = null, int? offset = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();
            if (limit.HasValue)
                getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
            if (offset.HasValue)
                getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));

            return TwitchGetGenericAsync<AllTeams>("/teams", ApiVersion.V5, getParams);
        }

        #endregion

        #region GetTeam
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task<Team> GetTeamAsync(string teamName)
        {
            if (string.IsNullOrWhiteSpace(teamName))
                throw new BadParameterException("The team name is not valid for fetching teams. It is not allowed to be null, empty or filled with whitespaces.");

            return TwitchGetGenericAsync<Team>($"/teams/{teamName}", ApiVersion.V5);
        }

        #endregion
    }

}