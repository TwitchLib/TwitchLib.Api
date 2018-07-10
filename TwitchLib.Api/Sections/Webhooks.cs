using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;
using TwitchLib.Api.Interfaces;

namespace TwitchLib.Api.Sections
{
    public class Webhooks
    {
        public Webhooks(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
        {
            Helix = new HelixApi(settings, rateLimiter, http);
        }

        public HelixApi Helix { get; }

        public class HelixApi : ApiBase
        {
            public HelixApi(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
            {
            }

            #region UserFollowsSomeone

            public Task<bool> UserFollowsSomeoneAsync(string callbackUrl, WebhookCallMode mode, string userInitiatorId, TimeSpan? duration = null, string signingSecret = null, string accessToken = null)
            {
                var leaseSeconds = (int) ValidateTimespan(duration).TotalSeconds;

                return PerformWebhookRequestAsync(mode, $"https://api.twitch.tv/helix/users/follows?first=1&from_id={userInitiatorId}", callbackUrl, leaseSeconds, signingSecret, accessToken);
            }

            #endregion

            #region UserReceivesFollower

            public Task<bool> UserReceivesFollowerAsync(string callbackUrl, WebhookCallMode mode, string userReceiverId, TimeSpan? duration = null, string signingSecret = null, string accessToken = null)
            {
                var leaseSeconds = (int) ValidateTimespan(duration).TotalSeconds;

                return PerformWebhookRequestAsync(mode, $"https://api.twitch.tv/helix/users/follows?first=1&to_id={userReceiverId}", callbackUrl, leaseSeconds, signingSecret, accessToken);
            }

            #endregion

            #region UserFollowsUser

            public Task<bool> UserFollowsUserAsync(string callbackUrl, WebhookCallMode mode, string userInitiator, string userReceiverId, TimeSpan? duration = null, string signingSecret = null, string accessToken = null)
            {
                var leaseSeconds = (int) ValidateTimespan(duration).TotalSeconds;

                return PerformWebhookRequestAsync(mode, $"https://api.twitch.tv/helix/users/follows?first=1&from_id={userInitiator}&to_id={userReceiverId}", callbackUrl, leaseSeconds, signingSecret);
            }

            #endregion

            #region StreamUpDown

            public Task<bool> StreamUpDownAsync(string callbackUrl, WebhookCallMode mode, string userId, TimeSpan? duration = null, string signingSecret = null, string accessToken = null)
            {
                var leaseSeconds = (int) ValidateTimespan(duration).TotalSeconds;

                return PerformWebhookRequestAsync(mode, $"https://api.twitch.tv/helix/streams?user_id={userId}", callbackUrl, leaseSeconds, signingSecret, accessToken);
            }

            #endregion

            #region UserChanged

            public Task<bool> UserChangedAsync(string callbackUrl, WebhookCallMode mode, int id, TimeSpan? duration = null, string signingSecret = null)
            {
                var leaseSeconds = (int) ValidateTimespan(duration).TotalSeconds;

                return PerformWebhookRequestAsync(mode, $"https://api.twitch.tv/helix/users?id={id}", callbackUrl, leaseSeconds, signingSecret);
            }

            #endregion

            #region GameAnalytics

            public Task<bool> GameAnalyticsAsync(string callbackUrl, WebhookCallMode mode, string gameId, TimeSpan? duration = null, string signingSecret = null, string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.Helix_Analytics_Read_Games, authToken);
                var leaseSeconds = (int) ValidateTimespan(duration).TotalSeconds;

                return PerformWebhookRequestAsync(mode, $"https://api.twitch.tv/helix/analytics/games?game_id={gameId}", callbackUrl, leaseSeconds, signingSecret);
            }

            #endregion

            private TimeSpan ValidateTimespan(TimeSpan? duration)
            {
                if (duration != null && duration.Value > TimeSpan.FromDays(10))
                    throw new BadParameterException("Maximum timespan allowed for webhook subscription duration is 10 days.");

                return duration ?? TimeSpan.FromDays(10);
            }

            private async Task<bool> PerformWebhookRequestAsync(WebhookCallMode mode, string topicUrl, string callbackUrl, int leaseSeconds, string signingSecret = null, string accessToken = null)
            {
                var getParams = new List<KeyValuePair<string, string>>
                {
                    mode == WebhookCallMode.Subscribe ? new KeyValuePair<string, string>("hub.mode", "subscribe") : new KeyValuePair<string, string>("hub.mode", "unsubscribe"),
                    new KeyValuePair<string, string>("hub.topic", topicUrl),
                    new KeyValuePair<string, string>("hub.callback", callbackUrl),
                    new KeyValuePair<string, string>("hub.lease_seconds", leaseSeconds.ToString())
                };


                if (signingSecret != null)
                    getParams.Add(new KeyValuePair<string, string>("hub.secret", signingSecret));
                var resp = await TwitchPostAsync("/webhooks/hub", ApiVersion.Helix, null, getParams, accessToken).ConfigureAwait(false);

                return resp.Key == 202;
            }
        }
    }
}