using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Extensions.System;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Models.Helix.Bits;
using TwitchLib.Api.Models.v5.Bits;

namespace TwitchLib.Api.Sections
{
    public class Bits
    {
        public Bits(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
        {
            V5 = new V5Api(settings, rateLimiter, http);
            Helix = new HelixApi(settings, rateLimiter, http);
        }

        public V5Api V5 { get; }
        public HelixApi Helix { get; }

        public class HelixApi : ApiBase
        {
            public HelixApi(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
            {
            }

            #region GetBitsLeaderboard

            public Task<GetBitsLeaderboardResponse> GetBitsLeaderboardAsync(int count = 10, BitsLeaderboardPeriodEnum period = BitsLeaderboardPeriodEnum.All, DateTime? startedAt = null, string userid = null, string accessToken = null)
            {
                DynamicScopeValidation(AuthScopes.Helix_Bits_Read, accessToken);

                var getParams = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("count", count.ToString())
                    };

                switch (period)
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

                return TwitchGetGenericAsync<GetBitsLeaderboardResponse>("/bits/leaderboard", ApiVersion.Helix, getParams);
            }

            #endregion
        }

        public class V5Api : ApiBase
        {
            public V5Api(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
            {
            }

            #region GetCheermotes

            public Task<Cheermotes> GetCheermotesAsync(string channelId = null)
            {
                List<KeyValuePair<string, string>> getParams = null;
                if (channelId != null)
                {
                    getParams = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("channel_id", channelId)
                    };
                }

                return TwitchGetGenericAsync<Cheermotes>("/bits/actions", ApiVersion.v5, getParams);
            }

            #endregion
        }
    }
}