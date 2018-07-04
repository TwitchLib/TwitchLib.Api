using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;

namespace TwitchLib.Api.Sections
{
    public class Clips
    {
        public Clips(TwitchAPI api)
        {
            v5 = new V5Api(api);
            helix = new HelixApi(api);
        }

        public V5Api v5 { get; }
        public HelixApi helix { get; }


        public class V5Api : ApiSection
        {
            public V5Api(TwitchAPI api) : base(api)
            {
            }

            #region GetClip

            public Task<Models.v5.Clips.Clip> GetClipAsync(string slug)
            {
                return Api.TwitchGetGenericAsync<Models.v5.Clips.Clip>($"/clips/{slug}", ApiVersion.v5);
            }

            #endregion

            #region GetTopClips

            public Task<Models.v5.Clips.TopClipsResponse> GetTopClipsAsync(string channel = null, string cursor = null, string game = null, long limit = 10, Models.v5.Clips.Period period = Models.v5.Clips.Period.Week, bool trending = false)
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
                    case Models.v5.Clips.Period.All:
                        getParams.Add(new KeyValuePair<string, string>("period", "all"));
                        break;
                    case Models.v5.Clips.Period.Month:
                        getParams.Add(new KeyValuePair<string, string>("period", "month"));
                        break;
                    case Models.v5.Clips.Period.Week:
                        getParams.Add(new KeyValuePair<string, string>("period", "week"));
                        break;
                    case Models.v5.Clips.Period.Day:
                        getParams.Add(new KeyValuePair<string, string>("period", "day"));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(period), period, null);
                }

                return Api.TwitchGetGenericAsync<Models.v5.Clips.TopClipsResponse>("/clips/top", ApiVersion.v5, getParams);
            }

            #endregion

            #region GetFollowedClips

            public Task<Models.v5.Clips.FollowClipsResponse> GetFollowedClipsAsync(long limit = 10, string cursor = null, bool trending = false, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("limit", limit.ToString())
                };
                if (cursor != null)
                    getParams.Add(new KeyValuePair<string, string>("cursor", cursor));
                getParams.Add(trending ? new KeyValuePair<string, string>("trending", "true") : new KeyValuePair<string, string>("trending", "false"));

                return Api.TwitchGetGenericAsync<Models.v5.Clips.FollowClipsResponse>("/clips/followed", ApiVersion.v5, getParams, accessToken: authToken);
            }

            #endregion
        }

        public class HelixApi : ApiSection
        {
            public HelixApi(TwitchAPI api) : base(api)
            {
            }

            #region GetClip

            public Task<Models.Helix.Clips.GetClip.GetClipResponse> GetClipAsync(string clipId = null, string gameId = null, string broadcasterId = null, string before = null, string after = null, int first = 20)
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

                return Api.TwitchGetGenericAsync<Models.Helix.Clips.GetClip.GetClipResponse>("/clips", ApiVersion.Helix, getParams);
            }

            #endregion

            #region CreateClip

            public Task<Models.Helix.Clips.CreateClip.CreatedClipResponse> CreateClipAsync(string broadcasterId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Helix_Clips_Edit);
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
                };
                return Api.TwitchPostGenericAsync<Models.Helix.Clips.CreateClip.CreatedClipResponse>("/clips", ApiVersion.Helix, null, getParams, accessToken: authToken);
            }

            #endregion
        }
    }
}