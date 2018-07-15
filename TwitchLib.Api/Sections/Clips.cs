using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Models.Helix.Clips.CreateClip;
using TwitchLib.Api.Models.Helix.Clips.GetClip;
using TwitchLib.Api.Models.V5.Clips;
using Clip = TwitchLib.Api.Models.V5.Clips.Clip;
using Period = TwitchLib.Api.Models.V5.Clips.Period;

namespace TwitchLib.Api.Sections
{
    public class Clips
    {
        public Clips(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
        {
            V5 = new V5Api(settings, rateLimiter, http);
            Helix = new HelixApi(settings, rateLimiter, http);
        }

        public V5Api V5 { get; }
        public HelixApi Helix { get; }
        
        public class V5Api : ApiBase
        {
            public V5Api(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
            {
            }

            #region GetClip

            public Task<Clip> GetClipAsync(string slug)
            {
                return TwitchGetGenericAsync<Clip>($"/clips/{slug}", ApiVersion.v5);
            }

            #endregion

            #region GetTopClips

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

                return TwitchGetGenericAsync<TopClipsResponse>("/clips/top", ApiVersion.v5, getParams);
            }

            #endregion

            #region GetFollowedClips

            public Task<FollowClipsResponse> GetFollowedClipsAsync(long limit = 10, string cursor = null, bool trending = false, string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.User_Read, authToken);
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("limit", limit.ToString())
                };
                if (cursor != null)
                    getParams.Add(new KeyValuePair<string, string>("cursor", cursor));
                getParams.Add(trending ? new KeyValuePair<string, string>("trending", "true") : new KeyValuePair<string, string>("trending", "false"));

                return TwitchGetGenericAsync<FollowClipsResponse>("/clips/followed", ApiVersion.v5, getParams, authToken);
            }

            #endregion
        }

        public class HelixApi : ApiBase
        {
            public HelixApi(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
            {
            }

            #region GetClip

            public Task<GetClipResponse> GetClipAsync(string clipId = null, string gameId = null, string broadcasterId = null, string before = null, string after = null, int first = 20)
            {
                if (first < 0 || first > 100)
                    throw new BadParameterException("'first' must between 0 (inclusive) and 100 (inclusive).");

                var getParams = new List<KeyValuePair<string, string>>();
                if (clipId != null)
                    getParams.Add(new KeyValuePair<string, string>("id", clipId));
                if (gameId != null)
                    getParams.Add(new KeyValuePair<string, string>("game_id", gameId));
                if (broadcasterId != null)
                    getParams.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));

                if (getParams.Count != 1)
                    throw new BadParameterException("One of the following parameters must be set: clipId, gameId, broadcasterId. Only one is allowed to be set.");

                if (before != null)
                    getParams.Add(new KeyValuePair<string, string>("before", before));
                if (after != null)
                    getParams.Add(new KeyValuePair<string, string>("after", after));
                getParams.Add(new KeyValuePair<string, string>("first", first.ToString()));

                return TwitchGetGenericAsync<GetClipResponse>("/clips", ApiVersion.Helix, getParams);
            }

            #endregion

            #region CreateClip

            public Task<CreatedClipResponse> CreateClipAsync(string broadcasterId, string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.Helix_Clips_Edit);
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
                };
                return TwitchPostGenericAsync<CreatedClipResponse>("/clips", ApiVersion.Helix, null, getParams, authToken);
            }

            #endregion
        }
    }
}