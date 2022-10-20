using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
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
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetGameAnalyticsResponse"></returns>
        public Task<GetGameAnalyticsResponse> GetGameAnalyticsAsync(string gameId = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrWhiteSpace(gameId))
                getParams.Add(new KeyValuePair<string, string>("game_id", gameId));

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
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetExtensionAnalyticsResponse"></returns>
        public Task<GetExtensionAnalyticsResponse> GetExtensionAnalyticsAsync(string extensionId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrWhiteSpace(extensionId))
                getParams.Add(new KeyValuePair<string, string>("extension_id", extensionId));

            return TwitchGetGenericAsync<GetExtensionAnalyticsResponse>("/analytics/extensions", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

    }
}