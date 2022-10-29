using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Extensions.System;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Bits;
using TwitchLib.Api.Helix.Models.Bits.ExtensionBitsProducts;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Bits related APIs
    /// </summary>
    public class Bits :ApiBase
    {
        public Bits(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        /// <summary>
        /// Retrieves the list of available Cheermotes, animated emotes to which viewers can assign Bits, to cheer in chat. Cheermotes returned are available throughout Twitch, in all Bits-enabled channels.
        /// </summary>
        /// <param name="broadcasterId">D for the broadcaster who might own specialized Cheermotes.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetCheermotesResponse"></returns>
        #region GetCheermotes
        public Task<GetCheermotesResponse> GetCheermotesAsync(string broadcasterId = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrWhiteSpace(broadcasterId))
            {
                getParams.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
            }

            return TwitchGetGenericAsync<GetCheermotesResponse>("/bits/cheermotes", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region GetBitsLeaderboard

        /// <summary>
        /// Gets a ranked list of Bits leaderboard information for an authorized broadcaster.
        /// <para>Required scope: bits:read</para>
        /// </summary>
        /// <param name="count">Number of results to be returned. Maximum: 100. Default: 10.</param>
        /// <param name="period">
        /// Time period over which data is aggregated (PST time zone). This parameter interacts with started_at. Default: "All".
        /// <para>"Day" – 00:00:00 on the day specified in started_at, through 00:00:00 on the following day.</para>
        /// <para>"Week" – 00:00:00 on Monday of the week specified in started_at, through 00:00:00 on the following Monday.</para>
        /// <para>"Month" – 00:00:00 on the first day of the month specified in started_at, through 00:00:00 on the first day of the following month.</para>
        /// <para>"Year" – 00:00:00 on the first day of the year specified in started_at, through 00:00:00 on the first day of the following year.</para>
        /// <para>"All" – The lifetime of the broadcaster's channel. If this is specified (or used by default), started_at is ignored.</para>
        /// </param>
        /// <param name="startedAt">Timestamp for the period over which the returned data is aggregated. If this is not provided, data is aggregated over the current period; e.g., the current day/week/month/year. This value is ignored if period is "All".</param>
        /// <param name="userid">
        /// ID of the user whose results are returned; i.e., the person who paid for the Bits.
        /// <para>As long as count is greater than 1, the returned data includes additional users, with Bits amounts above and below the user specified by user_id.</para>
        /// <para>If user_id is not provided, the endpoint returns the Bits leaderboard data across top users (subject to the value of count).</para>
        /// </param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetBitsLeaderboardResponse"></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Task<GetBitsLeaderboardResponse> GetBitsLeaderboardAsync(int count = 10, BitsLeaderboardPeriodEnum period = BitsLeaderboardPeriodEnum.All, DateTime? startedAt = null, string userid = null, string accessToken = null)
        {
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(period), period, null);
            }

            if (startedAt != null)
                getParams.Add(new KeyValuePair<string, string>("started_at", startedAt.Value.ToRfc3339String()));

            if (!string.IsNullOrWhiteSpace(userid))
                getParams.Add(new KeyValuePair<string, string>("user_id", userid));

            return TwitchGetGenericAsync<GetBitsLeaderboardResponse>("/bits/leaderboard", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region GetExtensionBitsProducts

        /// <summary>
        /// Gets a list of Bits products that belongs to an Extension
        /// <para>Requires App Access Token associated with the Extension client ID</para>
        /// </summary>
        /// <param name="shouldIncludeAll">Whether Bits products that are disabled/expired should be included in the response.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetExtensionBitsProductsResponse"></returns>
        public Task<GetExtensionBitsProductsResponse> GetExtensionBitsProductsAsync(bool shouldIncludeAll = false, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("should_include_all", shouldIncludeAll.ToString().ToLower())
            };

            return TwitchGetGenericAsync<GetExtensionBitsProductsResponse>("/bits/extensions", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region UpdateExtensionBitsProduct

        /// <summary>
        /// Add or update a Bits products that belongs to an Extension.
        /// <para>Requires App Access Token associated with the Extension client ID</para>
        /// </summary>
        /// <param name="extensionBitsProduct" cref="ExtensionBitsProduct">Bits product to add/update</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="UpdateExtensionBitsProductResponse"></returns>
        public Task<UpdateExtensionBitsProductResponse> UpdateExtensionBitsProductAsync(ExtensionBitsProduct extensionBitsProduct, string accessToken = null)
        {
            return TwitchPutGenericAsync<UpdateExtensionBitsProductResponse>("/bits/extensions", ApiVersion.Helix, extensionBitsProduct.ToString(), accessToken: accessToken);
        }

        #endregion
    }
}