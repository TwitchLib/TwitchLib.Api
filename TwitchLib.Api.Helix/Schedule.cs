using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Schedule.CreateChannelStreamSegment;
using TwitchLib.Api.Helix.Models.Schedule.GetChannelStreamSchedule;
using TwitchLib.Api.Helix.Models.Schedule.UpdateChannelStreamSegment;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Schedule related APIs
    /// </summary>
    public class Schedule : ApiBase
    {
        public Schedule(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        { }

        /// <summary>
        /// Gets all scheduled broadcasts or specific scheduled broadcasts from a channel’s stream schedule.
        /// <para>Scheduled broadcasts are defined as “stream segments” in the API.</para>
        /// </summary>
        /// <param name="broadcasterId">User ID of the broadcaster who owns the channel streaming schedule.</param>
        /// <param name="segmentIds">
        /// The IDs of the stream segment to return.
        /// <para>Maximum: 100.</para>
        /// </param>
        /// <param name="startTime">
        /// A timestamp in RFC3339 format to start returning stream segments from.
        /// <para>If not specified, the current date and time is used.</para>
        /// </param>
        /// <param name="utcOffset">
        /// A timezone offset for the requester specified in minutes.
        /// <para>This is recommended to ensure stream segments are returned for the correct week.</para>
        /// <para>For example, a timezone that is +4 hours from GMT would be “240.” </para>
        /// <para>If not specified, “0” is used for GMT.</para>
        /// </param>
        /// <param name="first">
        /// Maximum number of stream segments to return.
        /// <para>Maximum: 25. Default: 20.</para>
        /// </param>
        /// <param name="after">Cursor for forward pagination: tells the server where to start fetching the next set of results in a multi-page response.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetChannelStreamScheduleResponse"></returns>
        public Task<GetChannelStreamScheduleResponse> GetChannelStreamScheduleAsync(string broadcasterId, List<string> segmentIds = null, string startTime = null, string utcOffset = null,
            int first = 20, string after = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if (segmentIds != null && segmentIds.Count > 0)
            {
                getParams.AddRange(segmentIds.Select(segmentId => new KeyValuePair<string, string>("id", segmentId)));
            }

            if (!string.IsNullOrWhiteSpace(startTime))
                getParams.Add(new KeyValuePair<string, string>("start_time", startTime));

            if (!string.IsNullOrWhiteSpace(utcOffset))
                getParams.Add(new KeyValuePair<string, string>("utc_offset", utcOffset));

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetChannelStreamScheduleResponse>("/schedule", ApiVersion.Helix, getParams, accessToken);
        }

        /// <summary>
        /// Update the settings for a channel’s stream schedule.
        /// <para>This can be used for setting vacation details.</para>
        /// <para>Provided broadcasterId must match the user_id in the user OAuth token.</para>
        /// <para>Required scope: channel:manage:schedule</para>
        /// </summary>
        /// <param name="broadcasterId">User ID of the broadcaster who owns the channel streaming schedule. </param>
        /// <param name="isVacationEnabled">
        /// Indicates if Vacation Mode is enabled.
        /// <para>Set to true to add a vacation or false to remove vacation from the channel streaming schedule.</para>
        /// </param>
        /// <param name="vacationStartTime">
        /// Start time for vacation specified in RFC3339 format.
        /// <para>Required if isVacationEnabled is set to true.</para>
        /// </param>
        /// <param name="vacationEndTime">
        /// End time for vacation specified in RFC3339 format.
        /// <para>Required if isVacationEnabled is set to true.</para>
        /// </param>
        /// <param name="timezone">
        /// The timezone for when the vacation is being scheduled using the IANA time zone database format.
        /// <para>Required if isVacationEnabled is set to true.</para>
        /// </param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns></returns>
        public Task UpdateChannelStreamScheduleAsync(string broadcasterId, bool? isVacationEnabled = null, DateTime? vacationStartTime = null, DateTime? vacationEndTime = null,
            string timezone = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            if (isVacationEnabled.HasValue)
                getParams.Add(new KeyValuePair<string, string>("is_vacation_enabled", isVacationEnabled.Value.ToString()));

            if (vacationStartTime.HasValue)
                getParams.Add(new KeyValuePair<string, string>("vacation_start_time", vacationStartTime.Value.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz", DateTimeFormatInfo.InvariantInfo)));

            if (vacationEndTime.HasValue)
                getParams.Add(new KeyValuePair<string, string>("vacation_end_time", vacationEndTime.Value.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz", DateTimeFormatInfo.InvariantInfo)));

            if (!string.IsNullOrWhiteSpace(timezone))
                getParams.Add(new KeyValuePair<string, string>("timezone", timezone));

            return TwitchPatchAsync("/schedule/settings", ApiVersion.Helix, null, getParams, accessToken);
        }

        /// <summary>
        /// Create a single scheduled broadcast or a recurring scheduled broadcast for a channel’s stream schedule.
        /// <para>Provided broadcasterId must match the user_id in the user OAuth token.</para>
        /// <para>Required scope: channel:manage:schedule</para>
        /// </summary>
        /// <param name="broadcasterId">User ID of the broadcaster who owns the channel streaming schedule. </param>
        /// <param name="payload" cref="CreateChannelStreamSegmentRequest"></param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="CreateChannelStreamSegmentResponse"></returns>
        public Task<CreateChannelStreamSegmentResponse> CreateChannelStreamScheduleSegmentAsync(string broadcasterId, CreateChannelStreamSegmentRequest payload, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchPostGenericAsync<CreateChannelStreamSegmentResponse>("/schedule/segment", ApiVersion.Helix, JsonConvert.SerializeObject(payload), getParams, accessToken);
        }

        /// <summary>
        /// Update a single scheduled broadcast or a recurring scheduled broadcast for a channel’s stream schedule.
        /// <para>Provided broadcasterId must match the user_id in the user OAuth token.</para>
        /// <para>Required scope: channel:manage:schedule</para>
        /// </summary>
        /// <param name="broadcasterId">User ID of the broadcaster who owns the channel streaming schedule.</param>
        /// <param name="segmentId">The ID of the streaming segment to update.</param>
        /// <param name="payload" cref="UpdateChannelStreamSegmentRequest"></param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="UpdateChannelStreamSegmentResponse"></returns>
        public Task<UpdateChannelStreamSegmentResponse> UpdateChannelStreamScheduleSegmentAsync(string broadcasterId, string segmentId, UpdateChannelStreamSegmentRequest payload,
            string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("id", segmentId)
            };

            return TwitchPatchGenericAsync<UpdateChannelStreamSegmentResponse>("/schedule/segment", ApiVersion.Helix, JsonConvert.SerializeObject(payload), getParams, accessToken);
        }

        /// <summary>
        /// Delete a single scheduled broadcast or a recurring scheduled broadcast for a channel’s stream schedule.
        /// <para>Provided broadcasterId must match the user_id in the user OAuth token.</para>
        /// <para>Required scope: channel:manage:schedule</para>
        /// </summary>
        /// <param name="broadcasterId">User ID of the broadcaster who owns the channel streaming schedule.</param>
        /// <param name="segmentId">The ID of the streaming segment to delete.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns></returns>
        public Task DeleteChannelStreamScheduleSegmentAsync(string broadcasterId, string segmentId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("id", segmentId)
            };

            return TwitchDeleteAsync("/schedule/segment", ApiVersion.Helix, getParams, accessToken);
        }

        /// <summary>
        /// Gets all scheduled broadcasts from a channel’s stream schedule as an iCalendar.
        /// </summary>
        /// <param name="broadcasterId">User ID of the broadcaster who owns the channel streaming schedule.</param>
        /// <returns></returns>
        public Task GetChanneliCalendarAsync(string broadcasterId)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetAsync("/schedule/icalendar", ApiVersion.Helix, getParams);
        }
    }
}