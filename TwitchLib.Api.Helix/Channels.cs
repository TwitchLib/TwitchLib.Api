using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Channels.GetChannelEditors;
using TwitchLib.Api.Helix.Models.Channels.GetChannelInformation;
using TwitchLib.Api.Helix.Models.Channels.ModifyChannelInformation;

namespace TwitchLib.Api.Helix
{
    public class Channels : ApiBase
    {
        public Channels(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetChannelInformation
        public Task<GetChannelInformationResponse> GetChannelInformation(string broadcasterId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetGenericAsync<GetChannelInformationResponse>("/channels", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion

        #region ModifyChannelInformation
        public Task ModifyChannelInformation(string broadcasterId, ModifyChannelInformationRequest request, string accessToken = null)
        {
            DynamicScopeValidation(AuthScopes.Helix_User_Edit_Broadcast, accessToken);
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchPatchAsync("/channels", ApiVersion.Helix, JsonConvert.SerializeObject(request), getParams, accessToken);
        }
        #endregion

        #region GetChannelEditors
        public Task<GetChannelEditorsResponse> GetChannelEditors(string broadcasterId, string accessToken = null)
        {
            DynamicScopeValidation(AuthScopes.Helix_Channel_Read_Editors, accessToken);
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetGenericAsync<GetChannelEditorsResponse>("/channels", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion
    }
}
