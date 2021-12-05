﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Streams.CreateStreamMarker;
using TwitchLib.Api.Helix.Models.Streams.GetFollowedStreams;
using TwitchLib.Api.Helix.Models.Streams.GetStreamKey;
using TwitchLib.Api.Helix.Models.Streams.GetStreamMarkers;
using TwitchLib.Api.Helix.Models.Streams.GetStreams;
using TwitchLib.Api.Helix.Models.Streams.GetStreamTags;

namespace TwitchLib.Api.Helix
{
    public class Streams : ApiBase
    {
        public Streams(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        public Task<GetStreamsResponse> GetStreamsAsync(string after = null, List<string> communityIds = null, int first = 20, List<string> gameIds = null, List<string> languages = null, string type = "all", List<string> userIds = null, List<string> userLogins = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("first", first.ToString()),
                    new KeyValuePair<string, string>("type", type)
                };
            if (after != null)
                getParams.Add(new KeyValuePair<string, string>("after", after));
            if (communityIds != null && communityIds.Count > 0)
            {
                foreach (var communityId in communityIds)
                    getParams.Add(new KeyValuePair<string, string>("community_id", communityId));
            }

            if (gameIds != null && gameIds.Count > 0)
            {
                foreach (var gameId in gameIds)
                    getParams.Add(new KeyValuePair<string, string>("game_id", gameId));
            }

            if (languages != null && languages.Count > 0)
            {
                foreach (var language in languages)
                    getParams.Add(new KeyValuePair<string, string>("language", language));
            }

            if (userIds != null && userIds.Count > 0)
            {
                foreach (var userId in userIds)
                    getParams.Add(new KeyValuePair<string, string>("user_id", userId));
            }

            if (userLogins != null && userLogins.Count > 0)
            {
                foreach (var userLogin in userLogins)
                    getParams.Add(new KeyValuePair<string, string>("user_login", userLogin));
            }

            return TwitchGetGenericAsync<GetStreamsResponse>($"/streams", ApiVersion.Helix, getParams, accessToken);
        }

        public Task<GetStreamTagsResponse> GetStreamTagsAsync(string broadcasterId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
            {
                throw new BadParameterException("BroadcasterId must be set");
            }

            var getParams = new List<KeyValuePair<string, string>>();
            getParams.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));

            return TwitchGetGenericAsync<GetStreamTagsResponse>("/streams/tags", ApiVersion.Helix, getParams, accessToken);
        }

        public Task ReplaceStreamTagsAsync(string broadcasterId, List<string> tagIds = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();
            getParams.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));

            string payload = null;
            if (tagIds != null && tagIds.Count > 0)
            {
                dynamic dynamicPayload = new JObject();
                dynamicPayload.tag_ids = new JArray(tagIds);
                payload = dynamicPayload.ToString();
            }

            return TwitchPutAsync("/streams/tags", ApiVersion.Helix, payload, getParams, accessToken);
        }

        public Task<GetStreamKeyResponse> GetStreamKeyAsync(string broadcasterId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetGenericAsync<GetStreamKeyResponse>("/streams/key", ApiVersion.Helix, getParams, accessToken);
        }

        public Task<CreateStreamMarkerResponse> CreateStreamMarkerAsync(CreateStreamMarkerRequest request, string accessToken = null)
        {
            return TwitchPostGenericAsync<CreateStreamMarkerResponse>("/streams/markers", ApiVersion.Helix, JsonConvert.SerializeObject(request), null, accessToken);
        }

        public Task<GetStreamMarkersResponse> GetStreamMarkersAsync(string userId, string videoId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("user_id", userId),
                new KeyValuePair<string, string>("video_id", videoId)
            };

            return TwitchGetGenericAsync<GetStreamMarkersResponse>("/stream/markers", ApiVersion.Helix, getParams, accessToken);
        }

        public Task<GetFollowedStreamsResponse> GetFollowedStreamsAsync(string userId, int first = 100, string after = null, string accessToken = null)
        {
            if (first < 1 || first > 100)
                throw new BadParameterException("first cannot be less than 1 or greater than 100");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("user_id", userId),
                new KeyValuePair<string, string>("first", first.ToString())
            };
            if (after != null)
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetFollowedStreamsResponse>("/streams/followed", ApiVersion.Helix, getParams, accessToken);
        }
    }

}