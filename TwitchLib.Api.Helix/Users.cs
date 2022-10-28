using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Users.GetUserActiveExtensions;
using TwitchLib.Api.Helix.Models.Users.GetUserBlockList;
using TwitchLib.Api.Helix.Models.Users.GetUserExtensions;
using TwitchLib.Api.Helix.Models.Users.GetUserFollows;
using TwitchLib.Api.Helix.Models.Users.GetUsers;
using TwitchLib.Api.Helix.Models.Users.Internal;
using TwitchLib.Api.Helix.Models.Users.UpdateUserExtensions;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// User related APIs
    /// </summary>
    public class Users : ApiBase
    {
        public Users(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        /// <summary>
        /// Gets a specified user’s block list.
        /// <para>The list is sorted by when the block occurred in descending order (i.e. most recent block first).</para>
        /// <para>Required scope: user:read:blocked_users</para>
        /// </summary>
        /// <param name="broadcasterId">User ID for a Twitch user.</param>
        /// <param name="first">Maximum number of objects to return. Maximum: 100. Default: 20.</param>
        /// <param name="after">Cursor for forward pagination: tells the server where to start fetching the next set of results, in a multi-page response. </param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetUserBlockListResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetUserBlockListResponse> GetUserBlockListAsync(string broadcasterId, int first = 20, string after = null, string accessToken = null)
        {
            if (first > 100)
                throw new BadParameterException($"Maximum allowed objects is 100 (you passed {first})");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetUserBlockListResponse>("/users/blocks", ApiVersion.Helix, getParams, accessToken);
        }

        /// <summary>
        /// Blocks the specified user on behalf of the authenticated user.
        /// <para>Required scope: user:manage:blocked_users</para>
        /// </summary>
        /// <param name="targetUserId">User ID of the user to be blocked.</param>
        /// <param name="sourceContext">Source context for blocking the user.</param>
        /// <param name="reason">Reason for blocking the user. </param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns></returns>
        public Task BlockUserAsync(string targetUserId, BlockUserSourceContextEnum? sourceContext = null, BlockUserReasonEnum? reason = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("target_user_id", targetUserId)
            };

            if (sourceContext != null)
                getParams.Add(new KeyValuePair<string, string>("source_context", sourceContext.Value.ToString().ToLower()));

            if (reason != null)
                getParams.Add(new KeyValuePair<string, string>("reason", reason.Value.ToString().ToLower()));

            return TwitchPutAsync("/users/blocks", ApiVersion.Helix, null, getParams, accessToken);
        }

        /// <summary>
        /// Unblocks the specified user on behalf of the authenticated user.
        /// <para>Required scope: user:manage:blocked_users</para>
        /// </summary>
        /// <param name="targetUserId">User ID of the user to be unblocked.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns></returns>
        public Task UnblockUserAsync(string targetUserId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("target_user_id", targetUserId)
            };

            return TwitchDeleteAsync("/user/blocks", ApiVersion.Helix, getParams, accessToken);
        }

        /// <summary>
        /// Gets information about one or more specified Twitch users.
        /// <para>Users are identified by optional user IDs and/or login name.</para>
        /// <para>If neither a user ID nor a login name is specified, the user is looked up by Bearer token.</para>
        /// <para>OAuth token with user:read:email scope required to include the user’s verified email address in response.</para>
        /// </summary>
        /// <param name="ids">
        /// UserIds to query. Maximum: 100.
        /// <para>The limit of 100 IDs and login names is the total limit.</para>
        /// <para>You can request, for example, 50 of each or 100 of one of them. You cannot request 100 of both.</para>
        /// </param>
        /// <param name="logins">
        /// UserLogins to query. Maximum: 100.
        /// <para>The limit of 100 IDs and login names is the total limit.</para>
        /// <para>You can request, for example, 50 of each or 100 of one of them. You cannot request 100 of both.</para>
        /// </param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetUsersResponse"></returns>
        public Task<GetUsersResponse> GetUsersAsync(List<string> ids = null, List<string> logins = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();

            if (ids != null && ids.Count > 0)
            {
                getParams.AddRange(ids.Select(id => new KeyValuePair<string, string>("id", id)));
            }

            if (logins != null && logins.Count > 0)
            {
                getParams.AddRange(logins.Select(login => new KeyValuePair<string, string>("login", login)));
            }

            return TwitchGetGenericAsync<GetUsersResponse>("/users", ApiVersion.Helix, getParams, accessToken);
        }

        /// <summary>
        /// Gets information on follow relationships between two Twitch users.
        /// <para>This can return information like “who is X following,” “who is following X,” or “is user X following user Y.”</para>
        /// <para>Information returned is sorted in order, most recent follow first.</para>
        /// </summary>
        /// <param name="after">Cursor for forward pagination: tells the server where to start fetching the next set of results, in a multi-page response. </param>
        /// <param name="before"></param>
        /// <param name="first">Maximum number of objects to return. Maximum: 100. Default: 20.</param>
        /// <param name="fromId">User ID. The request returns information about users who are being followed by the from_id user.</param>
        /// <param name="toId">User ID. The request returns information about users who are following the to_id user.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetUsersFollowsResponse"></returns>
        public Task<GetUsersFollowsResponse> GetUsersFollowsAsync(string after = null, string before = null, int first = 20, string fromId = null, string toId = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            if (!string.IsNullOrWhiteSpace(before))
                getParams.Add(new KeyValuePair<string, string>("before", before));

            if (!string.IsNullOrWhiteSpace(fromId))
                getParams.Add(new KeyValuePair<string, string>("from_id", fromId));

            if (!string.IsNullOrWhiteSpace(toId))
                getParams.Add(new KeyValuePair<string, string>("to_id", toId));

            return TwitchGetGenericAsync<GetUsersFollowsResponse>("/users/follows", ApiVersion.Helix, getParams, accessToken);
        }

        /// <summary>
        /// Updates the description of a user specified by the bearer token.
        /// <para>Note that the description parameter is optional should other updatable parameters become available in the future.</para>
        /// <para> If the description parameter is not provided, no update will occur.</para>
        /// <para>Required scope: user:edit</para>
        /// </summary>
        /// <param name="description">User’s account description</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns></returns>
        public Task UpdateUserAsync(string description, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("description", description)
            };

            return TwitchPutAsync("/users", ApiVersion.Helix, null, getParams, accessToken);
        }

        /// <summary>
        /// Gets a list of all extensions (both active and inactive) for a specified user, identified by a Bearer token.
        /// <para>Required scope: user:read:broadcast or user:edit:broadcast</para>
        /// </summary>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetUserExtensionsResponse"></returns>
        public Task<GetUserExtensionsResponse> GetUserExtensionsAsync(string accessToken = null)
        {
            return TwitchGetGenericAsync<GetUserExtensionsResponse>("/users/extensions/list", ApiVersion.Helix, accessToken: accessToken);
        }

        /// <summary>
        /// Gets information about active extensions installed by a specified user, identified by a user ID or Bearer token.
        /// <para>Required scope: user:read:broadcast or user:edit:broadcast</para>
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns></returns>
        public Task<GetUserActiveExtensionsResponse> GetUserActiveExtensionsAsync(string userid = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrWhiteSpace(userid))
                getParams.Add(new KeyValuePair<string, string>("user_id", userid));

            return TwitchGetGenericAsync<GetUserActiveExtensionsResponse>("/users/extensions", ApiVersion.Helix, getParams, accessToken: accessToken);
        }

        /// <summary>
        /// Updates the activation state, extension ID, and/or version number of installed extensions for a specified user, identified by a Bearer token.
        /// <para>If you try to activate a given extension under multiple extension types, the last write wins (and there is no guarantee of write order).</para>
        /// <para>Required scope: user:edit:broadcast</para>
        /// </summary>
        /// <param name="userExtensionStates" cref="ExtensionSlot"></param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetUserActiveExtensionsResponse"></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Task<GetUserActiveExtensionsResponse> UpdateUserExtensionsAsync(IEnumerable<ExtensionSlot> userExtensionStates, string accessToken = null)
        {
            var panels = new Dictionary<string, UserExtensionState>();
            var overlays = new Dictionary<string, UserExtensionState>();
            var components = new Dictionary<string, UserExtensionState>();

            foreach (var extension in userExtensionStates)
                switch (extension.Type)
                {
                    case ExtensionType.Component:
                        components.Add(extension.Slot, extension.UserExtensionState);
                        break;
                    case ExtensionType.Overlay:
                        overlays.Add(extension.Slot, extension.UserExtensionState);
                        break;
                    case ExtensionType.Panel:
                        panels.Add(extension.Slot, extension.UserExtensionState);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(ExtensionType));
                }

            var json = new JObject();
            var p = new UpdateUserExtensionsRequest();

            if (panels.Count > 0)
                p.Panel = panels;

            if (overlays.Count > 0)
                p.Overlay = overlays;

            if (components.Count > 0)
                p.Component = components;

            json.Add(new JProperty("data", JObject.FromObject(p)));
            var payload = json.ToString();

            return TwitchPutGenericAsync<GetUserActiveExtensionsResponse>("/users/extensions", ApiVersion.Helix, payload, accessToken: accessToken);
        }
    }
}
