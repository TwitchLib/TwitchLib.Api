using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Tags;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Tags related APIs
    /// </summary>
    public class Tags : ApiBase
    {
        public Tags(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        /// <summary>
        /// Gets the list of all stream tags that Twitch defines. You can also filter the list by one or more tag IDs.
        /// </summary>
        /// <param name="after">The cursor used to get the next page of results.</param>
        /// <param name="first">The maximum number of tags to return per page. Maximum: 100. Default: 20.</param>
        /// <param name="tagIds">List of tag IDs to query. You may specify a maximum of 100 IDs.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetAllStreamTagsResponse"></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Task<GetAllStreamTagsResponse> GetAllStreamTagsAsync(string after = null, int first = 20, List<string> tagIds = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrWhiteSpace(after)) {
                getParams.Add(new KeyValuePair<string, string>("after", after));
            }

            if (first >= 0 && first <= 100)
            {
                getParams.Add(new KeyValuePair<string, string>("first", first.ToString()));
            } else
            {
                throw new ArgumentOutOfRangeException(nameof(first), $"{nameof(first)} value cannot exceed 100 and cannot be less than 1");
            }

            if (tagIds != null && tagIds.Count > 0)
            {
                getParams.AddRange(tagIds.Select(tagId => new KeyValuePair<string, string>("tag_id", tagId)));
            }

            return TwitchGetGenericAsync<GetAllStreamTagsResponse>("/tags/streams", ApiVersion.Helix, getParams, accessToken);
        }
    }
}
