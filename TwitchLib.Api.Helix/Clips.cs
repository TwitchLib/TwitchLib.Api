using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Extensions.System;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Clips.CreateClip;
using TwitchLib.Api.Helix.Models.Clips.GetClips;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Clips related APIs
    /// </summary>
    public class Clips : ApiBase
    {
        public Clips(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        { }

        #region GetClips
        /// <summary>
        /// Gets clip information by clip ID (one or more), broadcaster ID (one only), or game ID (one only).
        /// <para>Note: The clips service returns a maximum of 1000 clips.</para>
        /// </summary>
        /// <param name="clipIds">IDs of the clips to query. Limit: 100.</param>
        /// <param name="gameId">
        /// ID of the game for which clips are returned.
        /// <para>The number of clips returned is determined by the first query-string parameter (default: 20).</para>
        /// <para>Results are ordered by view count.</para>
        /// </param>
        /// <param name="broadcasterId">
        /// ID of the broadcaster for which clips are returned.
        /// <para>The number of clips returned is determined by the first query-string parameter (default: 20).</para>
        /// <para>Results are ordered by view count.</para>
        /// </param>
        /// <param name="before">
        /// Cursor for backward pagination: tells the server where to start fetching the next set of results, in a multi-page response.
        /// <para>This applies only to queries specifying broadcaster_id or game_id</para>
        /// </param>
        /// <param name="after">
        /// Cursor for forward pagination: tells the server where to start fetching the next set of results, in a multi-page response.
        /// <para>This applies only to queries specifying broadcaster_id or game_id</para>
        /// </param>
        /// <param name="startedAt">
        /// Starting date/time for returned clips, in RFC3339 format. (The seconds value is ignored.)
        /// <para>If this is specified, ended_at also should be specified; otherwise, the ended_at date/time will be 1 week after the started_at value.</para>
        /// </param>
        /// <param name="endedAt">
        /// Ending date/time for returned clips, in RFC3339 format. (Note that the seconds value is ignored.)
        /// <para>If this is specified, started_at also must be specified; otherwise, the time period is ignored.</para>
        /// </param>
        /// <param name="first">Maximum number of objects to return. Maximum: 100. Default: 20.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetClipsResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetClipsResponse> GetClipsAsync(List<string> clipIds = null, string gameId = null, string broadcasterId = null, string before = null, string after = null, DateTime? startedAt = null, DateTime? endedAt = null, int first = 20, string accessToken = null)
        {
            if (first < 0 || first > 100)
                throw new BadParameterException("'first' must between 0 (inclusive) and 100 (inclusive).");

            var getParams = new List<KeyValuePair<string, string>>();

            if (clipIds != null)
            {
                getParams.AddRange(clipIds.Select(clipId => new KeyValuePair<string, string>("id", clipId)));
            }

            if (!string.IsNullOrWhiteSpace(gameId))
                getParams.Add(new KeyValuePair<string, string>("game_id", gameId));

            if (!string.IsNullOrWhiteSpace(broadcasterId))
                getParams.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));

            if (getParams.Count == 0 || (getParams.Count > 1 && gameId != null && broadcasterId != null))
                throw new BadParameterException("One of the following parameters must be set: clipId, gameId, broadcasterId. Only one is allowed to be set.");

            if (startedAt == null && endedAt != null)
                throw new BadParameterException("The ended_at parameter cannot be used without the started_at parameter. Please include both parameters!");

            if (startedAt != null)
                getParams.Add(new KeyValuePair<string, string>("started_at", startedAt.Value.ToRfc3339String()));

            if (endedAt != null)
                getParams.Add(new KeyValuePair<string, string>("ended_at", endedAt.Value.ToRfc3339String()));

            if (!string.IsNullOrWhiteSpace(before))
                getParams.Add(new KeyValuePair<string, string>("before", before));

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            getParams.Add(new KeyValuePair<string, string>("first", first.ToString()));

            return TwitchGetGenericAsync<GetClipsResponse>("/clips", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region CreateClip

        /// <summary>
        /// Creates a clip programmatically. This returns both an ID and an edit URL for the new clip.
        /// <para>Clip creation takes time. We recommend that you query Get Clips, with the clip ID that is returned here.</para>
        /// <para>If Get Clips returns a valid clip, your clip creation was successful.</para>
        /// <para>If, after 15 seconds, you still have not gotten back a valid clip from Get Clips, assume that the clip was not created and retry Create Clip.</para>
        /// <para>This endpoint has a global rate limit, across all callers.</para>
        /// <para>Required scope: clips:edit</para>
        /// </summary>
        /// <param name="broadcasterId">ID of the stream from which the clip will be made.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="CreatedClipResponse"></returns>
        public Task<CreatedClipResponse> CreateClipAsync(string broadcasterId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchPostGenericAsync<CreatedClipResponse>("/clips", ApiVersion.Helix, null, getParams, accessToken);
        }

        #endregion

    }
}