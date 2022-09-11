using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public Task<GetPredictionsResponse> GetPredictionsAsync(string broadcasterId, List<string> ids = null, string after = null, int first = 20, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if (ids != null && ids.Count > 0)
                getParams.AddRange(ids.Select(id => new KeyValuePair<string, string>("id", id)));
            
            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetPredictionsResponse>("/predictions", ApiVersion.Helix, getParams, accessToken);
        }

        public Task<CreatePredictionResponse> CreatePredictionAsync(CreatePredictionRequest request, string accessToken = null)
        {
            return TwitchPostGenericAsync<CreatePredictionResponse>("/predictions", ApiVersion.Helix, JsonConvert.SerializeObject(request), accessToken: accessToken);
        }

        public Task<EndPredictionResponse> EndPredictionAsync(string broadcasterId, string id, PredictionEndStatus status, string winningOutcomeId = null, string accessToken = null)
        {
            var json = new JObject
            {
                ["broadcaster_id"] = broadcasterId,
                ["id"] = id,
                ["status"] = status.ToString()
            };

            if (!string.IsNullOrWhiteSpace(winningOutcomeId))
                json["winning_outcome_id"] = winningOutcomeId;

            return TwitchPatchGenericAsync<EndPredictionResponse>("/predictions", ApiVersion.Helix, json.ToString(), accessToken: accessToken);
        }
    }
}
