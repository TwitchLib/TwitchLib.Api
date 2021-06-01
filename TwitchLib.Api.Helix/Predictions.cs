﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Predictions.CreatePrediction;
using TwitchLib.Api.Helix.Models.Predictions.EndPrediction;
using TwitchLib.Api.Helix.Models.Predictions.GetPredictions;

namespace TwitchLib.Api.Helix
{
    public class Predictions : ApiBase
    {
        public Predictions(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        public Task<GetPredictionsResponse> GetPredictions(string broadcasterId, List<string> ids = null, string after = null, int first = 20, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if (ids != null && ids.Count > 0)
            {
                foreach (var id in ids)
                {
                    getParams.Add(new KeyValuePair<string, string>("id", id));
                }
            }
            if (after != null)
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetPredictionsResponse>("/predictions", ApiVersion.Helix, getParams, accessToken);
        }

        public Task<CreatePredictionResponse> CreatePrediction(CreatePredictionRequest request, string accessToken = null)
        {
            return TwitchPostGenericAsync<CreatePredictionResponse>("/predictions", ApiVersion.Helix, JsonConvert.SerializeObject(request), accessToken: accessToken);
        }

        public Task<EndPredictionResponse> EndPrediction(string broadcasterId, string id, PredictionStatusEnum status, string winningOutcomeId = null, string accessToken = null)
        {
            JObject json = new JObject();
            json["broadcaster_id"] = broadcasterId;
            json["id"] = id;
            json["status"] = status.ToString();
            if (winningOutcomeId != null)
                json["winning_outcome_id"] = winningOutcomeId;

            return TwitchPatchGenericAsync<EndPredictionResponse>("/predictions", ApiVersion.Helix, json.ToString(), accessToken: accessToken);
        }
    }
}
