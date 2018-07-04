using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;

namespace TwitchLib.Api.Sections
{
    public class Teams
    {
        public Teams(TwitchAPI api)
        {
            v5 = new V5Api(api);
        }

        public V5Api v5 { get; }

        public class V5Api : ApiSection
        {
            public V5Api(TwitchAPI api) : base(api)
            {
            }

            #region GetAllTeams

            public Task<Models.v5.Teams.AllTeams> GetAllTeamsAsync(int? limit = null, int? offset = null)
            {
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));

                return Api.TwitchGetGenericAsync<Models.v5.Teams.AllTeams>("/teams", ApiVersion.v5, getParams);
            }

            #endregion

            #region GetTeam

            public Task<Models.v5.Teams.Team> GetTeamAsync(string teamName)
            {
                if (string.IsNullOrWhiteSpace(teamName))
                {
                    throw new BadParameterException("The team name is not valid for fetching teams. It is not allowed to be null, empty or filled with whitespaces.");
                }

                return Api.TwitchGetGenericAsync<Models.v5.Teams.Team>($"/teams/{teamName}", ApiVersion.v5);
            }

            #endregion
        }
    }
}