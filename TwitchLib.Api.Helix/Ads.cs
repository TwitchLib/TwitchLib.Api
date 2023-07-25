using Newtonsoft.Json;
using System.Threading.Tasks;
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
      /// <para><see href="https://dev.twitch.tv/docs/api/reference/#start-commercial">
      /// Twitch Docs: Start Commercial</see></para>
      /// <para>Starts a commercial on the specified channel.</para>
      /// <para>Only partners and affiliates may run commercials and they must be streaming live at the time.
      /// Only the broadcaster may start a commercial - the broadcaster’s editors and moderators may not start commercials on behalf of the broadcaster.</para>
      /// <para><b>Requires a user access token that includes the channel:edit:commercial scope.</b></para>
      /// </summary>
      /// <param name="request" cref="StartCommercialRequest"></param>
      /// <param name="accessToken">Optional access token to override the use of the stored one in the TwitchAPI instance.</param>
      /// <returns cref="StartCommercialResponse"></returns>
      public Task<StartCommercialResponse> StartCommercialAsync(StartCommercialRequest request, string accessToken = null)
      {
         return TwitchPostGenericAsync<StartCommercialResponse>("/channels/commercial", ApiVersion.Helix, JsonConvert.SerializeObject(request), null, accessToken);
      }

      #endregion
   }
}
