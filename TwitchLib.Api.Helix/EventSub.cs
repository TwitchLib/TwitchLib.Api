using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.EventSub;

namespace TwitchLib.Api.Helix
{
    public class EventSub : ApiBase
    {
        public EventSub(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        /// <summary>
        /// Creates an EventSub subscription.
        /// </summary>
        /// <param name="type">The type of subscription to create.</param>
        /// <param name="version">The version of the subscription type used in this request.</param>
        /// <param name="condition">The parameter values that are specific to the specified subscription type.</param>
        /// <param name="method">The transport method. Supported values: Webhook, Websocket.</param>
        /// <param name="websocketSessionId">The session Id of a websocket connection that you want to subscribe to an event for. Only needed if method is Websocket</param>
        /// <param name="webhookCallback">The callback URL where the Webhook notification should be sent. Only needed if method is Webhook</param>
        /// <param name="webhookSecret">The secret used for verifying a Webhooks signature. Only needed if method is Webhook</param>
        /// <param name="clientId">optional Client ID to override the use of the stored one in the TwitchAPI instance</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="CreateEventSubSubscriptionResponse"></returns>
        public Task<CreateEventSubSubscriptionResponse> CreateEventSubSubscriptionAsync(string type, string version, Dictionary<string, string> condition, EventSubTransportMethod method, string websocketSessionId = null, string webhookCallback = null,
            string webhookSecret = null, string clientId = null, string accessToken = null)
        {
            if (string.IsNullOrEmpty(type))
                throw new BadParameterException("type must be set");

            if (string.IsNullOrEmpty(version))
                throw new BadParameterException("version must be set");

            if (condition == null || condition.Count == 0)
                throw new BadParameterException("condition must be set");

            switch (method)
            {
                case EventSubTransportMethod.Webhook:
                    if (string.IsNullOrWhiteSpace(webhookCallback))
                        throw new BadParameterException("webhookCallback must be set");

                    if (webhookSecret == null || webhookSecret.Length < 10 || webhookSecret.Length > 100)
                        throw new BadParameterException("webhookSecret must be set, and be between 10 (inclusive) and 100 (inclusive)");

                    var webhookBody = new
                    {
                        type,
                        version,
                        condition,
                        transport = new
                        {
                            method = method.ToString().ToLowerInvariant(),
                            callback = webhookCallback,
                            secret = webhookSecret
                        }
                    };
                    return TwitchPostGenericAsync<CreateEventSubSubscriptionResponse>("/eventsub/subscriptions", ApiVersion.Helix, JsonConvert.SerializeObject(webhookBody), null, accessToken, clientId);
                case EventSubTransportMethod.Websocket:
                    if (string.IsNullOrWhiteSpace(websocketSessionId))
                        throw new BadParameterException("websocketSessionId must be set");

                    var websocketBody = new
                    {
                        type,
                        version,
                        condition,
                        transport = new
                        {
                            method = method.ToString().ToLowerInvariant(),
                            session_id = websocketSessionId
                        }
                    };
                    return TwitchPostGenericAsync<CreateEventSubSubscriptionResponse>("/eventsub/subscriptions", ApiVersion.Helix, JsonConvert.SerializeObject(websocketBody), null, accessToken, clientId);
                default:
                    throw new ArgumentOutOfRangeException(nameof(method), method, null);
            }
        }

        /// <summary>
        /// Gets a list of your EventSub subscriptions. The list is paginated and ordered by the oldest subscription first.
        /// </summary>
        /// <param name="status">Filter subscriptions by its status.</param>
        /// <param name="type">Filter subscriptions by subscription type (e.g., channel.update).</param>
        /// <param name="userId">Filter subscriptions by user ID.</param>
        /// <param name="after">The cursor used to get the next page of results.</param>
        /// <param name="clientId">optional Client ID to override the use of the stored one in the TwitchAPI instance</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetEventSubSubscriptionsResponse">Returns a list of your EventSub subscriptions.</returns>
        public Task<GetEventSubSubscriptionsResponse> GetEventSubSubscriptionsAsync(string status = null, string type = null, string userId = null, string after = null, string clientId = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrWhiteSpace(status))
                getParams.Add(new KeyValuePair<string, string>("status", status));

            if (!string.IsNullOrWhiteSpace(type))
                getParams.Add(new KeyValuePair<string, string>("type", type));

            if (!string.IsNullOrWhiteSpace(userId))
                getParams.Add(new KeyValuePair<string, string>("user_id", userId));

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetEventSubSubscriptionsResponse>("/eventsub/subscriptions", ApiVersion.Helix, getParams, accessToken, clientId);
        }

        /// <summary>
        /// Deletes an EventSub subscription.
        /// </summary>
        /// <param name="id">The ID of the subscription to delete.</param>
        /// <param name="clientId">optional Client ID to override the use of the stored one in the TwitchAPI instance</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns>True: If successfully deleted; False: If delete failed</returns>
        public async Task<bool> DeleteEventSubSubscriptionAsync(string id, string clientId = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("id", id)
            };

            var response = await TwitchDeleteAsync("/eventsub/subscriptions", ApiVersion.Helix, getParams, accessToken, clientId);

            return response.Key == (int) HttpStatusCode.NoContent;
        }
    }
}
