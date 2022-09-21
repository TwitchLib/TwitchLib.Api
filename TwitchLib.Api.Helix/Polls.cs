using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Polls.CreatePoll;
using TwitchLib.Api.Helix.Models.Polls.EndPoll;
using TwitchLib.Api.Helix.Models.Polls.GetPolls;

namespace TwitchLib.Api.Helix
{
    public class Polls : ApiBase
    {
        public Polls(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        public Task<GetPollsResponse> GetPollsAsync(string broadcasterId, List<string> ids = null, string after = null, int first = 20, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            { 
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if (ids != null && ids.Count > 0)
            {
                getParams.AddRange(ids.Select(id => new KeyValuePair<string, string>("id", id)));
            }

            if (!string.IsNullOrWhiteSpace(accessToken))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetPollsResponse>("/polls", ApiVersion.Helix, getParams, accessToken);
        }

        public Task<CreatePollResponse> CreatePollAsync(CreatePollRequest request, string accessToken = null)
        {
            return TwitchPostGenericAsync<CreatePollResponse>("/polls", ApiVersion.Helix, JsonConvert.SerializeObject(request), accessToken: accessToken);
        }

        public Task<EndPollResponse> EndPollAsync(string broadcasterId, string id, PollStatusEnum status, string accessToken = null)
        {
            var json = new JObject
            {
                ["broadcaster_id"] = broadcasterId,
                ["id"] = id,
                ["status"] = status.ToString()
            };

            return TwitchPatchGenericAsync<EndPollResponse>("/polls", ApiVersion.Helix, json.ToString(), accessToken: accessToken);
        }
    }
}
