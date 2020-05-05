﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Extensions.System;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Clips.CreateClip;
using TwitchLib.Api.Helix.Models.Clips.GetClip;

namespace TwitchLib.Api.Helix
{
    public class Clips : ApiBase
    {
        public Clips(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        { }

        #region GetClip

        public Task<GetClipResponse> GetClipAsync(string clipId = null, string gameId = null, string broadcasterId = null, string before = null, string after = null, DateTime? startedAt = null, DateTime? endedAt = null, int first = 20)
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

            if (startedAt == null && endedAt != null)
                throw new BadParameterException("The ended_at parameter cannot be used without the started_at parameter. Please include both parameters!");

            if (startedAt != null)
                getParams.Add(new KeyValuePair<string, string>("started_at", startedAt.Value.ToRfc3339String()));
            if (endedAt != null)
                getParams.Add(new KeyValuePair<string, string>("ended_at", endedAt.Value.ToRfc3339String()));
            if (before != null)
                getParams.Add(new KeyValuePair<string, string>("before", before));
            if (after != null)
                getParams.Add(new KeyValuePair<string, string>("after", after));
            getParams.Add(new KeyValuePair<string, string>("first", first.ToString()));

            return TwitchGetGenericAsync<GetClipResponse>("/clips", ApiVersion.Helix, getParams);
        }

        #endregion

        #region CreateClip

        public async Task<CreatedClipResponse> CreateClipAsync(string broadcasterId, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Helix_Clips_Edit).ConfigureAwait(false);
            var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
                };
            return await TwitchPostGenericAsync<CreatedClipResponse>("/clips", ApiVersion.Helix, null, getParams, authToken).ConfigureAwait(false);
        }

        #endregion

    }
}