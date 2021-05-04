using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public Task<GetPollsResponse> GetPolls(string broadcasterId, List<string> ids = null, string after = null, int first = 20, string accessToken = null)
        {
            DynamicScopeValidation(AuthScopes.Helix_Channel_Read_Polls, accessToken);

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

            return TwitchGetGenericAsync<GetPollsResponse>("/polls", ApiVersion.Helix, getParams, accessToken);
        }

        public Task<CreatePollResponse> CreatePoll(CreatePollRequest request, string accessToken = null)
        {
            DynamicScopeValidation(AuthScopes.Helix_Channel_Manage_Polls, accessToken);

            return TwitchPostGenericAsync<CreatePollResponse>("/polls", ApiVersion.Helix, JsonConvert.SerializeObject(request), accessToken: accessToken);
        }

        public Task<EndPollResponse> EndPoll(string broadcasterId, string id, PollStatusEnum status, string accessToken = null)
        {
            DynamicScopeValidation(AuthScopes.Helix_Channel_Manage_Polls, accessToken);

            JObject json = new JObject();
            json["broadcaster_id"] = broadcasterId;
            json["id"] = id;
            json["status"] = status.ToString();

            return TwitchPostGenericAsync<EndPollResponse>("/polls", ApiVersion.Helix, json.ToString(), accessToken: accessToken);
        }
    }
}
