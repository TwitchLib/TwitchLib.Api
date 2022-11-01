using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Predictions.CreatePrediction;
using TwitchLib.Api.Helix.Models.Predictions.EndPrediction;
using TwitchLib.Api.Helix.Models.Predictions.GetPredictions;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Predictions related APIs
    /// </summary>
    public class Predictions : ApiBase
    {
        public Predictions(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        /// <summary>
        /// Get information about all Channel Points Predictions or specific Channel Points Predictions for a Twitch channel.
        /// <para>Results are ordered by most recent, so it can be assumed that the currently active or locked Prediction will be the first item.</para>
        /// <para>Required scope: channel:read:predictions</para>
        /// </summary>
        /// <param name="broadcasterId">The broadcaster running Predictions. Provided broadcaster_id must match the user_id in the user OAuth token.</param>
        /// <param name="ids">
        /// IDs of Predictions.
        /// <para>Filters results to one or more specific Predictions.</para>
        /// <para>Not providing one or more IDs will return the full list of Predictions for the authenticated channel.</para>
        /// <para>Maximum: 100</para>
        /// </param>
        /// <param name="after">Cursor for forward pagination: tells the server where to start fetching the next set of results in a multi-page response.</param>
        /// <param name="first">Maximum number of objects to return. Maximum: 20. Default: 20.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetPredictionsResponse"></returns>
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

        /// <summary>
        /// Create a Channel Points Prediction for a specific Twitch channel.
        /// <para>Required scope: channel:manage:predictions</para>
        /// </summary>
        /// <param name="request" cref="CreatePredictionRequest"></param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="CreatePredictionResponse"></returns>
        public Task<CreatePredictionResponse> CreatePredictionAsync(CreatePredictionRequest request, string accessToken = null)
        {
            return TwitchPostGenericAsync<CreatePredictionResponse>("/predictions", ApiVersion.Helix, JsonConvert.SerializeObject(request), accessToken: accessToken);
        }

        /// <summary>
        /// Lock, resolve, or cancel a Channel Points Prediction.
        /// <para>Active Predictions can be updated to be “locked,” “resolved,” or “canceled.” </para>
        /// <para>Locked Predictions can be updated to be “resolved” or “canceled.”</para>
        /// <para>Required scope: channel:manage:predictions</para>
        /// </summary>
        /// <param name="broadcasterId">The broadcaster running prediction events. Provided broadcaster_id must match the user_id in the user OAuth token.</param>
        /// <param name="id">ID of the Prediction to update/end.</param>
        /// <param name="status" cref="PredictionEndStatus">
        /// The Prediction status to be set. Valid values:
        /// <para>RESOLVED: A winning outcome has been chosen and the Channel Points have been distributed to the users who predicted the correct outcome.</para>
        /// <para>CANCELED: The Prediction has been canceled and the Channel Points have been refunded to participants.</para>
        /// <para>LOCKED: The Prediction has been locked and viewers can no longer make predictions.</para>
        /// </param>
        /// <param name="winningOutcomeId">
        /// ID of the winning outcome for the Prediction.
        /// <para>This parameter is required if status is being set to RESOLVED.</para>
        /// </param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="EndPredictionResponse"></returns>
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
