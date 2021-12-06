using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.V5.Models.Clips;
using Clip = TwitchLib.Api.V5.Models.Clips.Clip;
using Period = TwitchLib.Api.V5.Models.Clips.Period;

namespace TwitchLib.Api.V5
{
    [Obsolete("This is a v5 class, please use a Helix class. All v5 calls will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
    public class Clips : ApiBase
    {
        public Clips(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetClip
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task<Clip> GetClipAsync(string slug)
        {
            return TwitchGetGenericAsync<Clip>($"/clips/{slug}", ApiVersion.V5);
        }

        #endregion

        #region GetTopClips
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task<TopClipsResponse> GetTopClipsAsync(string channel = null, string cursor = null, string game = null, long limit = 10, Period period = Period.Week, bool trending = false)
        {
            var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("limit", limit.ToString())
                };
            if (channel != null)
                getParams.Add(new KeyValuePair<string, string>("channel", channel));
            if (cursor != null)
                getParams.Add(new KeyValuePair<string, string>("cursor", cursor));
            if (game != null)
                getParams.Add(new KeyValuePair<string, string>("game", game));
            getParams.Add(trending ? new KeyValuePair<string, string>("trending", "true") : new KeyValuePair<string, string>("trending", "false"));
            switch (period)
            {
                case Period.All:
                    getParams.Add(new KeyValuePair<string, string>("period", "all"));
                    break;
                case Period.Month:
                    getParams.Add(new KeyValuePair<string, string>("period", "month"));
                    break;
                case Period.Week:
                    getParams.Add(new KeyValuePair<string, string>("period", "week"));
                    break;
                case Period.Day:
                    getParams.Add(new KeyValuePair<string, string>("period", "day"));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(period), period, null);
            }

            return TwitchGetGenericAsync<TopClipsResponse>("/clips/top", ApiVersion.V5, getParams);
        }

        #endregion

        #region GetFollowedClips
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task<FollowClipsResponse> GetFollowedClipsAsync(long limit = 10, string cursor = null, bool trending = false, string authToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("limit", limit.ToString())
                };
            if (cursor != null)
                getParams.Add(new KeyValuePair<string, string>("cursor", cursor));
            getParams.Add(trending ? new KeyValuePair<string, string>("trending", "true") : new KeyValuePair<string, string>("trending", "false"));

            return TwitchGetGenericAsync<FollowClipsResponse>("/clips/followed", ApiVersion.V5, getParams, authToken);
        }

        #endregion
    }
}