using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Videos.DeleteVideos;
using TwitchLib.Api.Helix.Models.Videos.GetVideos;

namespace TwitchLib.Api.Helix
{
    public class Videos : ApiBase
    {
        public Videos(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        public Task<DeleteVideosResponse> DeleteVideosAsync(List<string> videoIds, string accessToken = null)
        {
            if (videoIds.Count > 5)
                throw new BadParameterException($"Maximum of 5 video ids allowed per request (you passed {videoIds.Count})");

            var getParams = videoIds.Select(videoId => new KeyValuePair<string, string>("id", videoId)).ToList();

            return TwitchDeleteGenericAsync<DeleteVideosResponse>("/videos", ApiVersion.Helix, getParams, accessToken);
        }

        public Task<GetVideosResponse> GetVideosAsync(List<string> videoIds = null, string userId = null, string gameId = null, string after = null, string before = null, int first = 20, string language = null, Period period = Period.All, VideoSort sort = VideoSort.Time, VideoType type = VideoType.All, string accessToken = null)
        {
            if ((videoIds == null || videoIds.Count == 0) && userId == null && gameId == null)
                throw new BadParameterException("VideoIds, userId, and gameId cannot all be null/empty.");

            if (videoIds != null && videoIds.Count > 0 && userId != null || videoIds != null && videoIds.Count > 0 && gameId != null || userId != null && gameId != null)
                throw new BadParameterException("If videoIds are present, you may not use userid or gameid. If gameid is present, you may not use videoIds or userid. If userid is present, you may not use videoids or gameids.");

            var getParams = new List<KeyValuePair<string, string>>();

            if (videoIds != null && videoIds.Count > 0)
            {
                getParams.AddRange(videoIds.Select(videoId => new KeyValuePair<string, string>("id", videoId)));
            }

            if (!string.IsNullOrWhiteSpace(userId))
                getParams.Add(new KeyValuePair<string, string>("user_id", userId));

            if (!string.IsNullOrWhiteSpace(gameId))
                getParams.Add(new KeyValuePair<string, string>("game_id", gameId));

            if (userId != null || gameId != null)
            {
                if (!string.IsNullOrWhiteSpace(after))
                    getParams.Add(new KeyValuePair<string, string>("after", after));

                if (!string.IsNullOrWhiteSpace(before))
                    getParams.Add(new KeyValuePair<string, string>("before", before));

                getParams.Add(new KeyValuePair<string, string>("first", first.ToString()));

                if (!string.IsNullOrWhiteSpace(language))
                    getParams.Add(new KeyValuePair<string, string>("language", language));

                switch (period)
                {
                    case Period.All:
                        getParams.Add(new KeyValuePair<string, string>("period", "all"));
                        break;
                    case Period.Day:
                        getParams.Add(new KeyValuePair<string, string>("period", "day"));
                        break;
                    case Period.Month:
                        getParams.Add(new KeyValuePair<string, string>("period", "month"));
                        break;
                    case Period.Week:
                        getParams.Add(new KeyValuePair<string, string>("period", "week"));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(period), period, null);
                }

                switch (sort)
                {
                    case VideoSort.Time:
                        getParams.Add(new KeyValuePair<string, string>("sort", "time"));
                        break;
                    case VideoSort.Trending:
                        getParams.Add(new KeyValuePair<string, string>("sort", "trending"));
                        break;
                    case VideoSort.Views:
                        getParams.Add(new KeyValuePair<string, string>("sort", "views"));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(sort), sort, null);
                }

                switch (type)
                {
                    case VideoType.All:
                        getParams.Add(new KeyValuePair<string, string>("type", "all"));
                        break;
                    case VideoType.Highlight:
                        getParams.Add(new KeyValuePair<string, string>("type", "highlight"));
                        break;
                    case VideoType.Archive:
                        getParams.Add(new KeyValuePair<string, string>("type", "archive"));
                        break;
                    case VideoType.Upload:
                        getParams.Add(new KeyValuePair<string, string>("type", "upload"));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }

            return TwitchGetGenericAsync<GetVideosResponse>("/videos", ApiVersion.Helix, getParams, accessToken);
        }
    }
}