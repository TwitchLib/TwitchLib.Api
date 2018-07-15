﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;
using TwitchLib.Api.Extensions.System;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Models.Helix.Videos;
using TwitchLib.Api.Models.V5.UploadVideo;
using TwitchLib.Api.Models.V5.Videos;
using Video = TwitchLib.Api.Models.V5.Videos.Video;

namespace TwitchLib.Api.Sections
{
    public class Videos
    {
        public Videos(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
        {
            V5 = new V5Api(settings, rateLimiter, http);
            Helix = new HelixApi(settings, rateLimiter, http);
        }

        public V5Api V5 { get; }
        public HelixApi Helix { get; }

        public class V5Api : ApiBase
        {
            private const long MAX_VIDEO_SIZE = 10737418240;

            public V5Api(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
            {
            }

            #region GetVideo

            public Task<Video> GetVideoAsync(string videoId)
            {
                if (string.IsNullOrWhiteSpace(videoId))
                    throw new BadParameterException("The video id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                return TwitchGetGenericAsync<Video>($"/videos/{videoId}", ApiVersion.v5);
            }

            #endregion

            #region GetTopVideos

            public Task<TopVideos> GetTopVideosAsync(int? limit = null, int? offset = null, string game = null, string period = null, List<string> broadcastType = null, List<string> language = null, string sort = null)
            {
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));
                if (!string.IsNullOrWhiteSpace(game))
                    getParams.Add(new KeyValuePair<string, string>("game", game));
                if (!string.IsNullOrWhiteSpace(period) && (period == "week" || period == "month" || period == "all"))
                    getParams.Add(new KeyValuePair<string, string>("period", period));
                if (broadcastType != null && broadcastType.Count > 0)
                {
                    var isCorrect = false;
                    foreach (var entry in broadcastType)
                        if (entry == "archive" || entry == "highlight" || entry == "upload")
                            isCorrect = true;
                        else
                        {
                            isCorrect = false;
                            break;
                        }

                    if (isCorrect)
                        getParams.Add(new KeyValuePair<string, string>("broadcast_type", string.Join(",", broadcastType)));
                }

                if (language != null && language.Count > 0)
                    getParams.Add(new KeyValuePair<string, string>("language", string.Join(",", language)));
                if (!string.IsNullOrWhiteSpace(sort) && (sort == "views" || sort == "time"))
                    getParams.Add(new KeyValuePair<string, string>("sort", sort));

                return TwitchGetGenericAsync<TopVideos>("/videos/top", ApiVersion.v5, getParams);
            }

            #endregion

            #region GetFollowedVideos

            public Task<FollowedVideos> GetFollowedVideosAsync(int? limit = null, int? offset = null, List<string> broadcastType = null, List<string> language = null, string sort = null, string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.User_Read, authToken);
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));
                if (broadcastType != null && broadcastType.Count > 0)
                {
                    var isCorrect = false;
                    foreach (var entry in broadcastType)
                        if (entry == "archive" || entry == "highlight" || entry == "upload")
                            isCorrect = true;
                        else
                        {
                            isCorrect = false;
                            break;
                        }

                    if (isCorrect)
                        getParams.Add(new KeyValuePair<string, string>("broadcast_type", string.Join(",", broadcastType)));
                }

                if (language != null && language.Count > 0)
                    getParams.Add(new KeyValuePair<string, string>("language", string.Join(",", language)));
                if (!string.IsNullOrWhiteSpace(sort) && (sort == "views" || sort == "time"))
                    getParams.Add(new KeyValuePair<string, string>("sort", sort));

                return TwitchGetGenericAsync<FollowedVideos>("/videos/followed", ApiVersion.v5, getParams, authToken);
            }

            #endregion

            #region UploadVideo

            public async Task<UploadedVideo> UploadVideoAsync(string channelId, string videoPath, string title, string description, string game, string language = "en", string tagList = "", Viewable viewable = Viewable.Public, DateTime? viewableAt = null, string accessToken = null)
            {
                DynamicScopeValidation(AuthScopes.Channel_Editor, accessToken);
                var listing = await CreateVideoAsync(channelId, title, description, game, language, tagList, viewable, viewableAt);
                UploadVideoParts(videoPath, listing.Upload);
                await CompleteVideoUploadAsync(listing.Upload, accessToken);

                return listing.Video;
            }

            #endregion

            #region UpdateVideo

            public Task<Video> UpdateVideoAsync(string videoId, string description = null, string game = null, string language = null, string tagList = null, string title = null, string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.Channel_Editor, authToken);
                if (string.IsNullOrWhiteSpace(videoId))
                    throw new BadParameterException("The video id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                var getParams = new List<KeyValuePair<string, string>>();
                if (!string.IsNullOrWhiteSpace(description))
                    getParams.Add(new KeyValuePair<string, string>("description", description));
                if (!string.IsNullOrWhiteSpace(game))
                    getParams.Add(new KeyValuePair<string, string>("game", game));
                if (!string.IsNullOrWhiteSpace(language))
                    getParams.Add(new KeyValuePair<string, string>("language", language));
                if (!string.IsNullOrWhiteSpace(tagList))
                    getParams.Add(new KeyValuePair<string, string>("tagList", tagList));
                if (!string.IsNullOrWhiteSpace(title))
                    getParams.Add(new KeyValuePair<string, string>("title", title));

                return TwitchPutGenericAsync<Video>($"/videos/{videoId}", ApiVersion.v5, null, getParams, authToken);
            }

            #endregion

            #region DeleteVideo

            public Task DeleteVideoAsync(string videoId, string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.Channel_Editor, authToken);
                if (string.IsNullOrWhiteSpace(videoId))
                    throw new BadParameterException("The video id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                return TwitchDeleteAsync($"/videos/{videoId}", ApiVersion.v5, accessToken: authToken);
            }

            #endregion


            private Task<UploadVideoListing> CreateVideoAsync(string channelId, string title, string description = null, string game = null, string language = "en", string tagList = "", Viewable viewable = Viewable.Public, DateTime? viewableAt = null, string accessToken = null)
            {
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("channel_id", channelId),
                    new KeyValuePair<string, string>("title", title)
                };
                if (!string.IsNullOrWhiteSpace(description))
                    getParams.Add(new KeyValuePair<string, string>("description", description));
                if (game != null)
                    getParams.Add(new KeyValuePair<string, string>("game", game));
                if (language != null)
                    getParams.Add(new KeyValuePair<string, string>("language", language));
                if (tagList != null)
                    getParams.Add(new KeyValuePair<string, string>("tag_list", tagList));
                getParams.Add(viewable == Viewable.Public ? new KeyValuePair<string, string>("viewable", "public") : new KeyValuePair<string, string>("viewable", "private"));

                if (viewableAt.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("viewable_at", viewableAt.Value.ToRfc3339String()));
                return TwitchPostGenericAsync<UploadVideoListing>("/videos", ApiVersion.v5, null, getParams, accessToken);
            }

            private void UploadVideoParts(string videoPath, Upload upload)
            {
                if (!File.Exists(videoPath))
                    throw new BadParameterException($"The provided path for a video upload does not appear to be value: {videoPath}");
                var videoInfo = new FileInfo(videoPath);
                if (videoInfo.Length >= MAX_VIDEO_SIZE)
                    throw new BadParameterException($"The provided file was too large (larger than 10gb). File size: {videoInfo.Length}");

                const long size24Mb = 25165824;
                var fileSize = videoInfo.Length;
                if (fileSize > size24Mb)
                {
                    // Split file into fragments if file size exceeds maximum fragment size
                    using (var fs = new FileStream(videoPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        var finalChunkSize = fileSize % size24Mb;
                        var parts = (fileSize - finalChunkSize) / size24Mb + 1;
                        for (var currentPart = 1; currentPart <= parts; currentPart++)
                        {
                            byte[] chunk;
                            if (currentPart == parts)
                            {
                                chunk = new byte[finalChunkSize];
                                fs.Read(chunk, 0, (int) finalChunkSize);
                            }
                            else
                            {
                                chunk = new byte[size24Mb];
                                fs.Read(chunk, 0, (int) size24Mb);
                            }

                            PutBytes($"{upload.Url}?part={currentPart}&upload_token={upload.Token}", chunk);
                            Thread.Sleep(1000);
                        }
                    }
                }
                else
                {
                    // Upload entire file at once if small enough
                    var file = File.ReadAllBytes(videoPath);
                    PutBytes($"{upload.Url}?part=1&upload_token={upload.Token}", file);
                }
            }

            private Task CompleteVideoUploadAsync(Upload upload, string accessToken)
            {
                return TwitchPostAsync(null, ApiVersion.v5, null, accessToken: accessToken, customBase: $"{upload.Url}/complete?upload_token={upload.Token}");
            }
        }

        public class HelixApi : ApiBase
        {
            public HelixApi(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
            {
            }

            public Task<GetVideosResponse> GetVideoAsync(List<string> videoIds = null, string userId = null, string gameId = null, string after = null, string before = null, int first = 20, string language = null, Period period = Period.All, VideoSort sort = VideoSort.Time, VideoType type = VideoType.All)
            {
                if ((videoIds == null || videoIds.Count == 0) && userId == null && gameId == null)
                    throw new BadParameterException("VideoIds, userId, and gameId cannot all be null/empty.");
                if (videoIds != null && videoIds.Count > 0 && userId != null || videoIds != null && videoIds.Count > 0 && gameId != null || userId != null && gameId != null)
                    throw new BadParameterException("If videoIds are present, you may not use userid or gameid. If gameid is present, you may not use videoIds or userid. If userid is present, you may not use videoids or gameids.");

                var getParams = new List<KeyValuePair<string, string>>();
                if (videoIds != null && videoIds.Count > 0)
                {
                    foreach (var videoId in videoIds)
                        getParams.Add(new KeyValuePair<string, string>("id", videoId));
                }

                if (userId != null)
                    getParams.Add(new KeyValuePair<string, string>("user_id", userId));
                if (gameId != null)
                    getParams.Add(new KeyValuePair<string, string>("game_id", gameId));

                if (userId != null || gameId != null)
                {
                    if (after != null)
                        getParams.Add(new KeyValuePair<string, string>("after", after));
                    if (before != null)
                        getParams.Add(new KeyValuePair<string, string>("before", before));
                    getParams.Add(new KeyValuePair<string, string>("first", first.ToString()));
                    if (language != null)
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

                return TwitchGetGenericAsync<GetVideosResponse>("/videos", ApiVersion.Helix, getParams);
            }
        }
    }
}