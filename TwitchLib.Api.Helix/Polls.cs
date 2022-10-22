using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Get information about all polls or specific polls for a Twitch channel. Poll information is available for 90 days.
        /// <para>Required scope: channel:read:polls</para>
        /// </summary>
        /// <param name="broadcasterId">The broadcaster running polls. Provided broadcaster_id must match the user_id in the user OAuth token.</param>
        /// <param name="ids">
        /// IDs polls. Filters results to one or more specific polls.
        /// <para>Not providing one or more IDs will return the full list of polls for the authenticated channel.</para>
        /// <para>Maximum: 100</para>
        /// </param>
        /// <param name="after">Cursor for forward pagination: tells the server where to start fetching the next set of results in a multi-page response.</param>
        /// <param name="first">Maximum number of objects to return. Maximum: 20. Default: 20.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns></returns>
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

        /// <summary>
        /// Create a poll for a specific Twitch channel.
        /// <para>Required scope: channel:manage:polls</para>
        /// </summary>
        /// <param name="request" cref="CreatePollRequest"></param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="CreatePollResponse"></returns>
        public Task<CreatePollResponse> CreatePollAsync(CreatePollRequest request, string accessToken = null)
        {
            return TwitchPostGenericAsync<CreatePollResponse>("/polls", ApiVersion.Helix, JsonConvert.SerializeObject(request), accessToken: accessToken);
        }

        /// <summary>
        /// End a poll that is currently active.
        /// <para>Required scope: channel:manage:polls</para>
        /// </summary>
        /// <param name="broadcasterId">The broadcaster running polls. Provided broadcaster_id must match the user_id in the user OAuth token.</param>
        /// <param name="id">ID of the poll to end.</param>
        /// <param name="status" cref="PollStatusEnum">
        /// The poll status to be set. Valid values:
        /// <para>TERMINATED: End the poll manually, but allow it to be viewed publicly.</para>
        /// <para>ARCHIVED: End the poll manually and do not allow it to be viewed publicly.</para>
        /// </param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns></returns>
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
