using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Channels.GetChannelEditors;
using TwitchLib.Api.Helix.Models.Channels.GetChannelInformation;
using TwitchLib.Api.Helix.Models.Channels.GetChannelVIPs;
using TwitchLib.Api.Helix.Models.Channels.ModifyChannelInformation;

namespace TwitchLib.Api.Helix
{
    public class Channels : ApiBase
    {
        public Channels(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetChannelInformation
        public Task<GetChannelInformationResponse> GetChannelInformationAsync(string broadcasterId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetGenericAsync<GetChannelInformationResponse>("/channels", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion

        #region ModifyChannelInformation
        public Task ModifyChannelInformationAsync(string broadcasterId, ModifyChannelInformationRequest request, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchPatchAsync("/channels", ApiVersion.Helix, JsonConvert.SerializeObject(request), getParams, accessToken);
        }
        #endregion

        #region GetChannelEditors
        public Task<GetChannelEditorsResponse> GetChannelEditorsAsync(string broadcasterId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetGenericAsync<GetChannelEditorsResponse>("/channels/editors", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion

        #region GetChannelVIPs

        /// <summary>
        /// BETA - Gets a list of the channel’s VIPs.
        /// Requires a user access token that includes the channel:read:vips scope.
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster whose list of VIPs you want to get.</param>
        /// <param name="userIds">Filters the list for specific VIPs. </param>
        /// <param name="first">The maximum number of items to return per page in the response. Max 100</param>
        /// <param name="after">The cursor used to get the next page of results.</param>
        /// <returns></returns>
        public Task<GetChannelVIPsResponse> GetVIPsAsync(string broadcasterId, List<string> userIds = null, int first = 20, string after = null, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId must be set");
            if (first > 100 & first <= 0)
                throw new BadParameterException("first must be greater than 0 and less then 101");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if (userIds != null)
            {
                if (userIds.Count == 0)
                    throw new BadParameterException("userIds must contain at least 1 userId if a list is included in the call");

                foreach (var userId in userIds)
                    getParams.Add(new KeyValuePair<string, string>("userId", userId));
            }

            if (after != null)
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetChannelVIPsResponse>("/channels/vips", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion
    }
}
