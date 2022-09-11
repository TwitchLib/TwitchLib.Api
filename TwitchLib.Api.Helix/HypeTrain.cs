using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.HypeTrain;

namespace TwitchLib.Api.Helix
{
    public class HypeTrain : ApiBase
    {
        public HypeTrain(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        // Requires Scope > channel:read:hype_train
        public Task<GetHypeTrainResponse> GetHypeTrainEventsAsync(string broadcasterId, int first = 1, string id = null, string cursor = null, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
            {
                throw new BadParameterException("BroadcasterId must be set");
            }

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if (!string.IsNullOrWhiteSpace(id))
                getParams.Add(new KeyValuePair<string, string>("id", id));

            if (!string.IsNullOrWhiteSpace(cursor))
                getParams.Add(new KeyValuePair<string, string>("cursor", cursor));

            return TwitchGetGenericAsync<GetHypeTrainResponse>("/hypetrain/events", ApiVersion.Helix, getParams, accessToken);
        }
    }
}
