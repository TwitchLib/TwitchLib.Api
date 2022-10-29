using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Extensions.System;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Analytics;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Analytics related APIs
    /// </summary>
    public class Analytics : ApiBase
    {
        public Analytics(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetGameAnalytics

        /// <summary>
        /// Gets a URL that game developers can use to download analytics reports (CSV files) for their games. The URL is valid for 5 minutes. 
        /// <para>!! the Token used to call this API has to be from a member of the Org the Game is registered to !!</para>
        /// <para>Required scope: analytics:read:games</para>
        /// </summary>
        /// <param name="gameId">
        /// Game ID. If this is specified, the returned URL points to an analytics report for just the specified game.
        /// <para>If this is not specified, the response includes multiple URLs (paginated), pointing to separate analytics reports for each of the authenticated user’s games.</para>
        /// </param>
        /// <param name="startedAt">
        /// Starting date/time for returned reports, in RFC3339 format with the hours, minutes, and seconds zeroed out and the UTC timezone: YYYY-MM-DDT00:00:00Z.
        /// <para>If this is provided, endedAt also must be specified.</para>
        /// <para>If startedAt is earlier than the default start date, the default date is used.</para>
        /// <para>Default: 365 days before the report was issued. The file contains one row of data per day.</para>
        /// </param>
        /// <param name="endedAt">
        /// Ending date/time for returned reports, in RFC3339 format with the hours, minutes, and seconds zeroed out and the UTC timezone: YYYY-MM-DDT00:00:00Z.
        /// <para>The report covers the entire ending date; e.g., if 2018-05-01T00:00:00Z is specified, the report covers up to 2018-05-01T23:59:59Z.</para>
        /// <para>If this is provided, startedAt also must be specified.</para>
        /// <para>If endedAt is later than the default end date, the default date is used. </para>
        /// <para>Default: 1-2 days before the request was issued (depending on report availability).</para>
        /// </param>
        /// <param name="first">Maximum number of objects to return. Maximum: 100. Default: 20.</param>
        /// <param name="after">
        /// Cursor for forward pagination: tells the server where to start fetching the next set of results, in a multi-page response.
        /// <para>This applies only to queries without gameId.</para>
        /// </param>
        /// <param name="type">
        /// Type of analytics report that is returned.
        /// <para> Currently, this field has no affect on the response as there is only one report type.</para>
        /// <para>If additional types were added, using this field would return only the URL for the specified report.</para>
        /// <para>Valid values: "overview_v2".</para>
        /// </param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetGameAnalyticsResponse"></returns>
        public Task<GetGameAnalyticsResponse> GetGameAnalyticsAsync(string gameId = null, DateTime? startedAt = null, DateTime? endedAt = null, int first = 20, string after = null, string type = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if (!string.IsNullOrWhiteSpace(gameId))
                getParams.Add(new KeyValuePair<string, string>("game_id", gameId));

            if (startedAt != null && endedAt != null)
            {
                getParams.Add(new KeyValuePair<string, string>("started_at", startedAt.Value.ToRfc3339String()));
                getParams.Add(new KeyValuePair<string, string>("ended_at", endedAt.Value.ToRfc3339String()));
            }

            if (!string.IsNullOrWhiteSpace(type))
                getParams.Add(new KeyValuePair<string, string>("type", type));

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetGameAnalyticsResponse>("/analytics/games", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region GetExtensionAnalytics

        /// <summary>
        /// Gets a URL that Extension developers can use to download analytics reports (CSV files) for their Extensions. The URL is valid for 5 minutes.
        /// <para>!! the Token used to call this API has to be from a member of the Org the Extension is registered to, or if not part of an Org, the Extension Developer !!</para>
        /// <para>Required scope: analytics:read:extensions</para>
        /// </summary> 
        /// <param name="extensionId">
        /// Client ID value assigned to the extension when it is created. If this is specified, the returned URL points to an analytics report for just the specified extension.
        /// <para>If this is not specified, the response includes multiple URLs (paginated), pointing to separate analytics reports for each of the authenticated user’s Extensions.</para>
        /// </param>
        /// <param name="startedAt">
        /// Starting date/time for returned reports, in RFC3339 format with the hours, minutes, and seconds zeroed out and the UTC timezone: YYYY-MM-DDT00:00:00Z.
        /// <para>If this is provided, endedAt also must be specified.</para>
        /// <para>If startedAt is earlier than the default start date, the default date is used.</para>
        /// <para>Default: 365 days before the report was issued. The file contains one row of data per day.</para>
        /// </param>
        /// <param name="endedAt">
        /// Ending date/time for returned reports, in RFC3339 format with the hours, minutes, and seconds zeroed out and the UTC timezone: YYYY-MM-DDT00:00:00Z.
        /// <para>The report covers the entire ending date; e.g., if 2018-05-01T00:00:00Z is specified, the report covers up to 2018-05-01T23:59:59Z.</para>
        /// <para>If this is provided, startedAt also must be specified.</para>
        /// <para>If endedAt is later than the default end date, the default date is used. </para>
        /// <para>Default: 1-2 days before the request was issued (depending on report availability).</para>
        /// </param>
        /// <param name="first">Maximum number of objects to return. Maximum: 100. Default: 20.</param>
        /// <param name="after">
        /// Cursor for forward pagination: tells the server where to start fetching the next set of results, in a multi-page response.
        /// <para>This applies only to queries without gameId.</para>
        /// </param>
        /// <param name="type">
        /// Type of analytics report that is returned.
        /// <para> Currently, this field has no affect on the response as there is only one report type.</para>
        /// <para>If additional types were added, using this field would return only the URL for the specified report.</para>
        /// <para>Valid values: "overview_v2".</para>
        /// </param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetExtensionAnalyticsResponse"></returns>
        public Task<GetExtensionAnalyticsResponse> GetExtensionAnalyticsAsync(string extensionId, DateTime? startedAt = null, DateTime? endedAt = null, int first = 20, string after = null, string type = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if (!string.IsNullOrWhiteSpace(extensionId))
                getParams.Add(new KeyValuePair<string, string>("extension_id", extensionId));

            if (startedAt != null && endedAt != null)
            {
                getParams.Add(new KeyValuePair<string, string>("started_at", startedAt.Value.ToRfc3339String()));
                getParams.Add(new KeyValuePair<string, string>("ended_at", endedAt.Value.ToRfc3339String()));
            }

            if (!string.IsNullOrWhiteSpace(type))
                getParams.Add(new KeyValuePair<string, string>("type", type));

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetExtensionAnalyticsResponse>("/analytics/extensions", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

    }
}