using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.ContentClassificationLabels;

namespace TwitchLib.Api.Helix
{
   /// <summary>
   /// <para>ContentClassificationLabels related APIs</para>
   /// </summary>
   public class ContentClassificationLabels : ApiBase
   {
      /// <summary>
      /// 
      /// </summary>
      public ContentClassificationLabels(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
      {}

      #region GetContentClassificationLabels

      /// <summary>
      /// <para><see href="https://dev.twitch.tv/docs/api/reference/#get-content-classification-labels">
      /// TwitchAPI Docs: Get Content Classification Labels </see></para>
      /// <para>Gets information about Twitch content classification labels.</para>
      /// <para><b>Requires an app access token or user access token.</b></para>
      /// </summary>
      /// <param name="locale"></param>
      /// <param name="accessToken"></param>
      /// <returns></returns>
      public Task<GetContentClassificationLabelsResponse> GetContentClassificationLabelsAsync(string locale = null, string accessToken = null)
      {
         var getParams = new List<KeyValuePair<string, string>>();

         if (!string.IsNullOrWhiteSpace(locale))
         {
            getParams.Add(new KeyValuePair<string, string>("locale", locale));
         }

         return TwitchGetGenericAsync<GetContentClassificationLabelsResponse>("/content_classification_labels", ApiVersion.Helix, getParams, accessToken);
      }
      #endregion
   }
}
