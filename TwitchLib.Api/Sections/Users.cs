﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Models.Helix.Users.GetUserActiveExtensions;
using TwitchLib.Api.Models.Helix.Users.GetUserExtensions;
using TwitchLib.Api.Models.Helix.Users.GetUsers;
using TwitchLib.Api.Models.Helix.Users.GetUsersFollows;
using TwitchLib.Api.Models.Helix.Users.UpdateUserExtensions;
using TwitchLib.Api.Models.V5.Subscriptions;
using TwitchLib.Api.Models.V5.Users;
using TwitchLib.Api.Models.V5.ViewerHeartbeatService;
using User = TwitchLib.Api.Models.V5.Users.User;

namespace TwitchLib.Api.Sections
{
    public class Users
    {
        public Users(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
        {
            V5 = new V5Api(settings, rateLimiter, http);
            Helix = new HelixApi(settings, rateLimiter, http);
        }

        public V5Api V5 { get; }
        public HelixApi Helix { get; }

        public class V5Api : ApiBase
        {
            public V5Api(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
            {
            }


            #region GetUsersByName

            public Task<Models.V5.Users.Users> GetUsersByNameAsync(List<string> usernames)
            {
                if (usernames == null || usernames.Count == 0)
                    throw new BadParameterException("The username list is not valid. It is not allowed to be null or empty.");

                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("login", string.Join(",", usernames))
                };
                return TwitchGetGenericAsync<Models.V5.Users.Users>("/users", ApiVersion.v5, getParams);
            }

            #endregion

            #region GetUser

            public Task<UserAuthed> GetUserAsync(string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.User_Read, authToken);

                return TwitchGetGenericAsync<UserAuthed>("/user", ApiVersion.v5, accessToken: authToken);
            }

            #endregion

            #region GetUserByID

            public Task<User> GetUserByIDAsync(string userId)
            {
                if (string.IsNullOrWhiteSpace(userId))
                    throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                return TwitchGetGenericAsync<User>($"/users/{userId}", ApiVersion.v5);
            }

            #endregion

            #region GetUserByName

            public Task<Models.V5.Users.Users> GetUserByNameAsync(string username)
            {
                if (string.IsNullOrEmpty(username))
                    throw new BadParameterException("The username is not valid.");

                var getParams = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("login", username)};
                return TwitchGetGenericAsync<Models.V5.Users.Users>("/users", ApiVersion.v5, getParams);
            }

            #endregion

            #region GetUserEmotes

            public Task<UserEmotes> GetUserEmotesAsync(string userId, string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.User_Subscriptions, authToken);
                if (string.IsNullOrWhiteSpace(userId))
                    throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                return TwitchGetGenericAsync<UserEmotes>($"/users/{userId}/emotes", ApiVersion.v5, accessToken: authToken);
            }

            #endregion

            #region CheckUserSubscriptionByChannel

            public Task<Subscription> CheckUserSubscriptionByChannelAsync(string userId, string channelId, string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.User_Subscriptions, authToken);
                if (string.IsNullOrWhiteSpace(userId))
                    throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                if (string.IsNullOrWhiteSpace(channelId))
                    throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                return TwitchGetGenericAsync<Subscription>($"/users/{userId}/subscriptions/{channelId}", ApiVersion.v5, accessToken: authToken);
            }

            #endregion

            #region GetUserFollows

            public Task<UserFollows> GetUserFollowsAsync(string userId, int? limit = null, int? offset = null, string direction = null, string sortby = null)
            {
                if (string.IsNullOrWhiteSpace(userId))
                    throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));
                if (!string.IsNullOrEmpty(direction) && (direction == "asc" || direction == "desc"))
                    getParams.Add(new KeyValuePair<string, string>("direction", direction));
                if (!string.IsNullOrEmpty(sortby) && (sortby == "created_at" || sortby == "last_broadcast" || sortby == "login"))
                    getParams.Add(new KeyValuePair<string, string>("sortby", sortby));

                return TwitchGetGenericAsync<UserFollows>($"/users/{userId}/follows/channels", ApiVersion.v5, getParams);
            }

            #endregion

            #region CheckUserFollowsByChannel

            public Task<UserFollow> CheckUserFollowsByChannelAsync(string userId, string channelId)
            {
                if (string.IsNullOrWhiteSpace(userId))
                    throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                if (string.IsNullOrWhiteSpace(channelId))
                    throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                return TwitchGetGenericAsync<UserFollow>($"/users/{userId}/follows/channels/{channelId}", ApiVersion.v5);
            }

            #endregion

            #region UserFollowsChannel

            public async Task<bool> UserFollowsChannelAsync(string userId, string channelId)
            {
                if (string.IsNullOrWhiteSpace(userId))
                    throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                if (string.IsNullOrWhiteSpace(channelId))
                    throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                try
                {
                    await TwitchGetGenericAsync<UserFollow>($"/users/{userId}/follows/channels/{channelId}", ApiVersion.v5);
                    return true;
                }
                catch (BadResourceException)
                {
                    return false;
                }
            }

            #endregion

            #region FollowChannel

            public Task<UserFollow> FollowChannelAsync(string userId, string channelId, bool? notifications = null, string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.User_Follows_Edit, authToken);
                if (string.IsNullOrWhiteSpace(userId))
                    throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                if (string.IsNullOrWhiteSpace(channelId))
                    throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                var optionalRequestBody = notifications.HasValue ? "{\"notifications\": " + notifications.Value.ToString().ToLower() + "}" : null;
                return TwitchPutGenericAsync<UserFollow>($"/users/{userId}/follows/channels/{channelId}", ApiVersion.v5, optionalRequestBody, accessToken: authToken);
            }

            #endregion

            #region UnfollowChannel

            public Task UnfollowChannelAsync(string userId, string channelId, string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.User_Follows_Edit, authToken);
                if (string.IsNullOrWhiteSpace(userId))
                    throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                if (string.IsNullOrWhiteSpace(channelId))
                    throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                return TwitchDeleteAsync($"/users/{userId}/follows/channels/{channelId}", ApiVersion.v5, accessToken: authToken);
            }

            #endregion

            #region GetUserBlockList

            public Task<UserBlocks> GetUserBlockListAsync(string userId, int? limit = null, int? offset = null, string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.User_Blocks_Read, authToken);
                if (string.IsNullOrWhiteSpace(userId))
                    throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));

                return TwitchGetGenericAsync<UserBlocks>($"/users/{userId}/blocks", ApiVersion.v5, getParams, authToken);
            }

            #endregion

            #region BlockUser

            public Task<UserBlock> BlockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.User_Blocks_Edit, authToken);
                if (string.IsNullOrWhiteSpace(sourceUserId))
                    throw new BadParameterException("The source user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                if (string.IsNullOrWhiteSpace(targetUserId))
                    throw new BadParameterException("The target user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                return TwitchPutGenericAsync<UserBlock>($"/users/{sourceUserId}/blocks/{targetUserId}", ApiVersion.v5, null, accessToken: authToken);
            }

            #endregion

            #region UnblockUser

            public Task UnblockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.User_Blocks_Edit, authToken);
                if (string.IsNullOrWhiteSpace(sourceUserId))
                    throw new BadParameterException("The source user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                if (string.IsNullOrWhiteSpace(targetUserId))
                    throw new BadParameterException("The target user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                return TwitchDeleteAsync($"/users/{sourceUserId}/blocks/{targetUserId}", ApiVersion.v5, accessToken: authToken);
            }

            #endregion

            #region ViewerHeartbeatService

            #region CreateUserConnectionToViewerHeartbeatService

            public Task CreateUserConnectionToViewerHeartbeatServiceAsync(string identifier, string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.Viewing_Activity_Read, authToken);
                if (string.IsNullOrWhiteSpace(identifier))
                    throw new BadParameterException("The identifier is not valid. It is not allowed to be null, empty or filled with whitespaces.");

                var payload = "{\"identifier\": \"" + identifier + "\"}";
                return TwitchPutAsync("/user/vhs", ApiVersion.v5, payload, accessToken: authToken);
            }

            #endregion

            #region CheckUserConnectionToViewerHeartbeatService

            public Task<VHSConnectionCheck> CheckUserConnectionToViewerHeartbeatServiceAsync(string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.User_Read, authToken);

                return TwitchGetGenericAsync<VHSConnectionCheck>("/user/vhs", ApiVersion.v5, accessToken: authToken);
            }

            #endregion

            #region DeleteUserConnectionToViewerHeartbeatService

            public Task DeleteUserConnectionToViewerHeartbeatServicechStreamsAsync(string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.Viewing_Activity_Read, authToken);

                return TwitchDeleteAsync("/user/vhs", ApiVersion.v5, accessToken: authToken);
            }

            #endregion

            #endregion
        }

        public class HelixApi : ApiBase
        {
            public HelixApi(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
            {
            }

            public Task<GetUsersResponse> GetUsersAsync(List<string> ids = null, List<string> logins = null, string accessToken = null)
            {
                var getParams = new List<KeyValuePair<string, string>>();
                if (ids != null && ids.Count > 0)
                {
                    foreach (var id in ids)
                        getParams.Add(new KeyValuePair<string, string>("id", id));
                }

                if (logins != null && logins.Count > 0)
                {
                    foreach (var login in logins)
                        getParams.Add(new KeyValuePair<string, string>("login", login));
                }

                return TwitchGetGenericAsync<GetUsersResponse>("/users", ApiVersion.Helix, getParams, accessToken);
            }

            public Task<GetUsersFollowsResponse> GetUsersFollowsAsync(string after = null, string before = null, int first = 20, string fromId = null, string toId = null)
            {
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("first", first.ToString())
                };
                if (after != null)
                    getParams.Add(new KeyValuePair<string, string>("after", after));
                if (before != null)
                    getParams.Add(new KeyValuePair<string, string>("before", before));
                if (fromId != null)
                    getParams.Add(new KeyValuePair<string, string>("from_id", fromId));
                if (toId != null)
                    getParams.Add(new KeyValuePair<string, string>("to_id", toId));

                return TwitchGetGenericAsync<GetUsersFollowsResponse>("/users/follows", ApiVersion.Helix, getParams);
            }

            public Task PutUsersAsync(string description, string accessToken = null)
            {
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("description", description)
                };

                return TwitchPutAsync("/users", ApiVersion.Helix, null, getParams, accessToken);
            }

            public Task<GetUserExtensionsResponse> GetUserExtensionsAsync(string authToken = null)
            {
                return TwitchGetGenericAsync<GetUserExtensionsResponse>("/users/extensions/list", ApiVersion.Helix, accessToken: authToken);
            }

            public Task<GetUserActiveExtensionsResponse> GetUserActiveExtensionsAsync(string authToken = null)
            {
                return TwitchGetGenericAsync<GetUserActiveExtensionsResponse>("/users/extensions", ApiVersion.Helix, accessToken: authToken);
            }

            public Task<GetUserActiveExtensionsResponse> UpdateUserExtensionsAsync(IEnumerable<ExtensionSlot> userExtensionStates, string authToken = null)
            {
                DynamicScopeValidation(AuthScopes.Channel_Editor, authToken);

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
                var p = new Payload();

                if (panels.Count > 0)
                    p.panel = panels;

                if (overlays.Count > 0)
                    p.overlay = overlays;

                if (components.Count > 0)
                    p.component = components;

                json.Add(new JProperty("data", JObject.FromObject(p)));
                var payload = json.ToString();

                return TwitchPutGenericAsync<GetUserActiveExtensionsResponse>("/users/extensions", ApiVersion.Helix, payload, accessToken: authToken);
            }
        }
    }
}