using Newtonsoft.Json;
using System.Collections.Generic;
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
    public class Schedule : ApiBase
    {
        public Schedule(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        { }

        public Task<GetChannelStreamScheduleResponse> GetChannelStreamScheduleAsync(string broadcasterId, List<string> segmentIds = null, string startTime = null, string utcOffset = null,
            int first = 20, string after = null, string authToken = null)
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

            return TwitchGetGenericAsync<GetChannelStreamScheduleResponse>("/schedule", ApiVersion.Helix, getParams, authToken);
        }

        public Task UpdateChannelStreamScheduleAsync(string broadcasterId, bool? isVacationEnabled = null, string vacationStartTime = null, string vacationEndTime = null,
            string timezone = null, string authToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            if (isVacationEnabled.HasValue)
                getParams.Add(new KeyValuePair<string, string>("is_vacation_enabled", isVacationEnabled.Value.ToString()));

            if (!string.IsNullOrWhiteSpace(vacationStartTime))
                getParams.Add(new KeyValuePair<string, string>("vacation_start_time", vacationStartTime));
            if (!string.IsNullOrWhiteSpace(vacationEndTime))
                getParams.Add(new KeyValuePair<string, string>("vacation_end_time", vacationEndTime));
            if (!string.IsNullOrWhiteSpace(timezone))
                getParams.Add(new KeyValuePair<string, string>("timezone", timezone));

            return TwitchPatchAsync("/schedule/settings", ApiVersion.Helix, null, getParams, authToken);
        }

        public Task<CreateChannelStreamSegmentResponse> CreateChannelStreamScheduleSegmentAsync(string broadcasterId, CreateChannelStreamSegmentRequest payload, string authToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchPostGenericAsync<CreateChannelStreamSegmentResponse>("/schedule/segment", ApiVersion.Helix, JsonConvert.SerializeObject(payload), getParams, authToken);
        }

        public Task<UpdateChannelStreamSegmentResponse> UpdateChannelStreamScheduleSegmentAsync(string broadcasterId, string segmentId, UpdateChannelStreamSegmentRequest payload,
            string authToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("id", segmentId)
            };

            return TwitchPatchGenericAsync<UpdateChannelStreamSegmentResponse>("/schedule/segment", ApiVersion.Helix, JsonConvert.SerializeObject(payload), getParams, authToken);
        }

        public Task DeleteChannelStreamScheduleSegmentAsync(string broadcasterId, string segmentId, string authToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("id", segmentId)
            };

            return TwitchDeleteAsync("/schedule/segment", ApiVersion.Helix, getParams, authToken);
        }
    }
}