using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Extensions.System;

namespace TwitchLib.Api.Sections
{
    public class Bits
    {
        public Bits(TwitchAPI api)
        {
            v5 = new V5(api);
            helix = new Helix(api);
        }

        public V5 v5 { get; }
        public Helix helix { get; }

        public class Helix : ApiSection
        {
            public Helix(TwitchAPI api) : base(api)
            {
            }

            #region GetBitsLeaderboard
            public async Task<Models.Helix.Bits.GetBitsLeaderboardResponse> GetBitsLeaderboardAsync(int count = 10, BitsLeaderboardPeriodEnum period = BitsLeaderboardPeriodEnum.All, DateTime? startedAt = null, string userid = null, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Helix_Bits_Read, accessToken);
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>();
                getParams.Add(new KeyValuePair<string, string>("count", count.ToString()));
                switch(period)
                {
                    case BitsLeaderboardPeriodEnum.Day:
                        getParams.Add(new KeyValuePair<string, string>("period", "day"));
                        break;
                    case BitsLeaderboardPeriodEnum.Week:
                        getParams.Add(new KeyValuePair<string, string>("period", "week"));
                        break;
                    case BitsLeaderboardPeriodEnum.Month:
                        getParams.Add(new KeyValuePair<string, string>("period", "month"));
                        break;
                    case BitsLeaderboardPeriodEnum.Year:
                        getParams.Add(new KeyValuePair<string, string>("period", "year"));
                        break;
                    case BitsLeaderboardPeriodEnum.All:
                        getParams.Add(new KeyValuePair<string, string>("period", "all"));
                        break;
                }
                if (startedAt != null)
                    getParams.Add(new KeyValuePair<string, string>("started_at", startedAt.Value.ToRfc3339String()));
                if (userid != null)
                    getParams.Add(new KeyValuePair<string, string>("user_id", userid));

                return await Api.GetGenericAsync<Models.Helix.Bits.GetBitsLeaderboardResponse>("https://api.twitch.tv/helix/bits/leaderboard", getParams, accessToken, ApiVersion.Helix);
            }
            #endregion 
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }

            #region GetCheermotes
            public async Task<Models.v5.Bits.Cheermotes> GetCheermotesAsync(string channelId = null)
            {
                List<KeyValuePair<string, string>> getParams = null;
                if (channelId != null)
                    getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("channel_id", channelId) };
                return await Api.GetGenericAsync<Models.v5.Bits.Cheermotes>("https://api.twitch.tv/kraken/bits/actions", getParams).ConfigureAwait(false);
            }
            #endregion
        }
    }
}