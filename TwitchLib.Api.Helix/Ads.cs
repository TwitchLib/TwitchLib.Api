using System.Threading.Tasks;
using Newtonsoft.Json;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Ads;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Ads related APIs
    /// </summary>
    public class Ads : ApiBase
    {
        public Ads(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region StartCommercial
        /// <summary>
        /// Starts a commercial on a specified channel.
        /// <para>Required scope: channel:edit:commercial</para>
        /// </summary>
        /// <param name="request" cref="StartCommercialRequest"></param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="StartCommercialResponse"></returns>
        public Task<StartCommercialResponse> StartCommercialAsync(StartCommercialRequest request, string accessToken = null)
        {
            return TwitchPostGenericAsync<StartCommercialResponse>("/channels/commercial", ApiVersion.Helix, JsonConvert.SerializeObject(request), null, accessToken);
        }
        #endregion
    }
}
