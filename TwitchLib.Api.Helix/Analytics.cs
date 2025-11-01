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
      /// <summary>
      /// Analytics related APIs
      /// </summary>
      public Analytics(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
      {
      }

      #region GetGameAnalytics

      /// <summary>
      /// <para><see href="https://dev.twitch.tv/docs/api/reference/#get-game-analytics">
      ///   Twitch Docs: Get Game Analytics</see></para>
      /// <para>Gets a URL that game developers can use to download analytics reports (CSV files) for their games. The URL is valid for 5 minutes.</para>
      /// <para><b>!! The user access token used to call this API must be from a member of the organization the game is registered to !!</b></para>
      /// <para>Requires a user access token that includes the scope:<br/>
      ///   analytics:read:games</para>
      /// </summary>
      /// 
      /// <param name="gameId">
      /// <para>The game’s client ID.</para>
      /// <para>If specified, the response contains an analytics report for just the specified game.</para>
      /// <para>If not specified, the response includes multiple URLs (paginated), pointing to separate analytics reports for each of the authenticated user’s games.</para>
      /// </param>
      /// 
      /// <param name="startedAt">
      /// <para>The starting date for the analytic report time period.<br/>
      ///   (This is automtically converted to RFC3339 format)</para>
      /// <para><b>If you specify a start date, you must also specify an end date in endedAt.</b></para>
      /// <para>The start date must be within one year of today’s date.<br/>
      ///   If you specify an earlier date, the API ignores it and uses a date that’s one year prior to today’s date.<br/>
      ///   If you don’t specify a start and end date, the report includes all available data for the last 365 days from today.</para>
      /// <para>The report contains one row of data for each day in the reporting window.</para>
      /// </param>
      /// 
      /// <param name="endedAt">
      /// <para>The ending date for the analytic report time period.<br/>
      ///   (This is automatically converted to RFC3339 format)</para>
      /// <para><b>Only specify an end date if you specified a start date.</b></para>
      /// <para>Because it can take up to two days for the data to be available, you must specify an end date that’s earlier than today minus one to two days.
      ///   If not, the API ignores your end date and uses an end date that is today minus one to two days.</para>
      /// </param>
      /// 
      /// <param name="first">
      /// <para>The maximum number of report URLs to return per page in the response.</para>
      ///  <para>Minimum page size: 1 URL per page<br/>
      ///   Maximum page size: 100 URLs per page.<br/>
      ///   Default page size: 20 URLs per page.</para>
      /// <para>While you may specify a maximum value of 100, the response will contain at most 20 URLs per page.</para>
      /// </param>
      /// 
      /// <param name="after">
      /// <para>Cursor for forward pagination: tells the server where to start fetching the next set of results, in a multi-page response.</para>
      /// <para>This parameter is ignored if the game_id parameter is set.</para>
      /// </param>
      /// 
      /// <param name="type">
      /// <para>Type of analytics report that is returned.</para>
      /// <para>Currently, this field has no affect on the response as there is only one report type.</para>
      /// <para>If additional types were added, using this field would return only the URL for the specified report.</para>
      /// <para>Valid values: "overview_v2".</para>
      /// </param>
      /// 
      /// <param name="accessToken">
      /// <para>Optional access token to override the use of the stored one in the TwitchAPI instance.</para>
      /// </param>
      /// <returns cref="GetGameAnalyticsResponse"></returns>
      public Task<GetGameAnalyticsResponse> GetGameAnalyticsAsync(string gameId = null, DateTime? startedAt = null, DateTime? endedAt = null, int first = 20, string after = null, string type = null, string accessToken = null)
      {
         var getParams = new List<KeyValuePair<string, string>>
         {
               new("first", first.ToString())
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
      /// <para><see href="https://dev.twitch.tv/docs/api/reference/#get-extension-analytics">
      /// Twitch Docs: Get Extension Analytics</see></para>
      /// <para>Gets a URL that Extension developers can use to download analytics reports (CSV files) for their Extensions. The URL is valid for 5 minutes.</para>
      /// <para><b>!! The user access token used to call this API must be from a member of the organzation the extension is registered to or the Extension Developer !!</b></para>
      /// <para>Requires a user access token that includes the analytics:read:extensions scope.</para>
      /// </summary> 
      /// 
      /// <param name="extensionId">
      /// <para>Client ID value assigned to the extension when it is created.</para>
      /// <para>If this is specified, the returned URL points to an analytics report for just the specified extension.</para>
      /// <para>If this is not specified, the response includes multiple URLs (paginated), pointing to separate analytics reports for each of the authenticated user’s Extensions.</para>
      /// </param>
      /// 
      /// <param name="startedAt">
      /// <para>The starting date for the analytic report time period.<br/>
      /// (This is automtically converted to RFC3339 format)</para>
      /// <para><b>If you specify a start date, you must also specify an end date in endedAt.</b></para>
      /// <para>The start date must be on or after January 31, 2018.<br/>
      ///   If you specify an earlier date, the API ignores it and uses January 31, 2018.<br/> 
      ///   If you specify a start date, you must specify an end date.<br/> 
      ///   If you don’t specify a start and end date, the report includes all available data since January 31, 2018.</para>
      /// <para>The report contains one row of data for each day in the reporting window.</para>
      /// </param>
      /// 
      /// <param name="endedAt">
      /// <para>The ending date for the analytic report time period.<br/>
      /// (This is automatically converted to RFC3339 format)</para>
      /// <para><b>Only specify an end date if you specified a start date.</b></para>
      /// <para>Because it can take up to two days for the data to be available, you must specify an end date that’s earlier than today minus one to two days.
      ///   If not, the API ignores your end date and uses an end date that is today minus one to two days.</para>
      /// </param>
      /// 
      /// <param name="first">
      /// <para>The maximum number of report URLs to return per page in the response.</para>
      /// <para>Minimum page size: 1 URL per page<br/>
      ///  Maximum page size: 100 URLs per page.<br/>
      ///  Default page size: 20 URLs per page.</para>
      /// <para>Note: While you may specify a maximum value of 100, the response will contain at most 20 URLs per page.</para>
      /// </param>
      /// 
      /// <param name="after">
      /// <para>Cursor for forward pagination: tells the server where to start fetching the next set of results, in a multi-page response.</para>
      /// <para>This parameter is ignored if the extension_id parameter is set.</para>
      /// </param>
      /// 
      /// <param name="type">
      /// <para>Type of analytics report that is returned.</para>
      /// <para>Currently, this field has no affect on the response as there is only one report type.</para>
      /// <para>If additional types were added, using this field would return only the URL for the specified report.</para>
      /// <para>Valid values: "overview_v2".</para>
      /// </param>
      /// 
      /// <param name="accessToken">
      /// <para>Optional access token to override the use of the stored one in the TwitchAPI instance.</para>
      /// </param>
      /// <returns cref="GetExtensionAnalyticsResponse"></returns>
      public Task<GetExtensionAnalyticsResponse> GetExtensionAnalyticsAsync(string extensionId, DateTime? startedAt = null, DateTime? endedAt = null, int first = 20, string after = null, string type = null, string accessToken = null)
      {
         var getParams = new List<KeyValuePair<string, string>>
         {
               new("first", first.ToString())
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