using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Models.V5.Teams;

namespace TwitchLib.Api.Sections
{
    public class Teams
    {
        public Teams(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
        {
            V5 = new V5Api(settings, rateLimiter, http);
        }

        public V5Api V5 { get; }

        public class V5Api : ApiBase
        {
            public V5Api(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
            {
            }

            #region GetAllTeams

            public Task<AllTeams> GetAllTeamsAsync(int? limit = null, int? offset = null)
            {
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));

                return TwitchGetGenericAsync<AllTeams>("/teams", ApiVersion.v5, getParams);
            }

            #endregion

            #region GetTeam

            public Task<Team> GetTeamAsync(string teamName)
            {
                if (string.IsNullOrWhiteSpace(teamName))
                    throw new BadParameterException("The team name is not valid for fetching teams. It is not allowed to be null, empty or filled with whitespaces.");

                return TwitchGetGenericAsync<Team>($"/teams/{teamName}", ApiVersion.v5);
            }

            #endregion
        }
    }
}