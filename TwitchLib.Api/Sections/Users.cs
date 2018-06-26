using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;

namespace TwitchLib.Api.Sections
{
    public class Users
    {
        public Users(TwitchAPI api)
        {
            v5 = new V5Api(api);
            helix = new HelixApi(api);
        }

        public V5Api v5 { get; }
        public HelixApi helix { get; }

        public class V5Api : ApiSection
        {
            public V5Api(TwitchAPI api) : base(api)
            {
            }
            #region GetUsersByName
            public Task<Models.v5.Users.Users> GetUsersByNameAsync(List<string> usernames)
            {
                if (usernames == null || usernames.Count == 0) { throw new BadParameterException("The username list is not valid. It is not allowed to be null or empty."); }
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("login", string.Join(",", usernames)) };
                return Api.TwitchGetGenericAsync<Models.v5.Users.Users>("/users", ApiVersion.v5, getParams);
            }
            #endregion
            #region GetUser
            public Task<Models.v5.Users.UserAuthed> GetUserAsync(string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Read, authToken);
                return Api.TwitchGetGenericAsync<Models.v5.Users.UserAuthed>("/user", ApiVersion.v5, accessToken: authToken);
            }
            #endregion
            #region GetUserByID
            public Task<Models.v5.Users.User> GetUserByIDAsync(string userId)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Api.TwitchGetGenericAsync<Models.v5.Users.User>($"/users/{userId}", ApiVersion.v5);
            }
            #endregion
            #region GetUserByName
            public Task<Models.v5.Users.Users> GetUserByNameAsync(string username)
            {
                if (string.IsNullOrEmpty(username)) { throw new BadParameterException("The username is not valid."); }
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("login", username) };
                return Api.TwitchGetGenericAsync<Models.v5.Users.Users>("/users", ApiVersion.v5, getParams);
            }
            #endregion
            #region GetUserEmotes
            public Task<Models.v5.Users.UserEmotes> GetUserEmotesAsync(string userId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Subscriptions, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Api.TwitchGetGenericAsync<Models.v5.Users.UserEmotes>($"/users/{userId}/emotes", ApiVersion.v5, accessToken: authToken);
            }
            #endregion
            #region CheckUserSubscriptionByChannel
            public Task<Models.v5.Subscriptions.Subscription> CheckUserSubscriptionByChannelAsync(string userId, string channelId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Subscriptions, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Api.TwitchGetGenericAsync<Models.v5.Subscriptions.Subscription>($"/users/{userId}/subscriptions/{channelId}", ApiVersion.v5, accessToken: authToken);
            }
            #endregion
            #region GetUserFollows
            public Task<Models.v5.Users.UserFollows> GetUserFollowsAsync(string userId, int? limit = null, int? offset = null, string direction = null, string sortby = null)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));
                if (!string.IsNullOrEmpty(direction) && (direction == "asc" || direction == "desc"))
                    getParams.Add(new KeyValuePair<string, string>("direction", direction));
                if (!string.IsNullOrEmpty(sortby) && (sortby == "created_at" || sortby == "last_broadcast" || sortby == "login"))
                    getParams.Add(new KeyValuePair<string, string>("sortby", sortby));

                return Api.TwitchGetGenericAsync < Models.v5.Users.UserFollows>($"/users/{userId}/follows/channels", ApiVersion.v5, getParams);
            }
            #endregion
            #region CheckUserFollowsByChannel
            public Task<Models.v5.Users.UserFollow> CheckUserFollowsByChannelAsync(string userId, string channelId)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Api.TwitchGetGenericAsync<Models.v5.Users.UserFollow>($"/users/{userId}/follows/channels/{channelId}", ApiVersion.v5);
            }
            #endregion
            #region UserFollowsChannel
            public async Task<bool> UserFollowsChannelAsync(string userId, string channelId)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                try
                {
                    await Api.TwitchGetGenericAsync<Models.v5.Users.UserFollow>($"/users/{userId}/follows/channels/{channelId}", ApiVersion.v5).ConfigureAwait(false);
                    return true;
                }
                catch (BadResourceException)
                {
                    return false;
                }
            }
            #endregion
            #region FollowChannel
            public Task<Models.v5.Users.UserFollow> FollowChannelAsync(string userId, string channelId, bool? notifications = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Follows_Edit, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                var optionalRequestBody = notifications.HasValue ? "{\"notifications\": " + notifications.Value + "}" : null;
                return Api.TwitchPutGenericAsync<Models.v5.Users.UserFollow>($"/users/{userId}/follows/channels/{channelId}", ApiVersion.v5, optionalRequestBody, accessToken: authToken);
            }
            #endregion
            #region UnfollowChannel
            public async Task UnfollowChannelAsync(string userId, string channelId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Follows_Edit, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.TwitchDeleteAsync($"/users/{userId}/follows/channels/{channelId}", ApiVersion.v5, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region GetUserBlockList
            public Task<Models.v5.Users.UserBlocks> GetUserBlockListAsync(string userId, int? limit = null, int? offset = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Blocks_Read, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));

                return Api.TwitchGetGenericAsync<Models.v5.Users.UserBlocks>($"/users/{userId}/blocks", ApiVersion.v5, getParams, authToken);
            }
            #endregion
            #region BlockUser
            public Task<Models.v5.Users.UserBlock> BlockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Blocks_Edit, authToken);
                if (string.IsNullOrWhiteSpace(sourceUserId)) { throw new BadParameterException("The source user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(targetUserId)) { throw new BadParameterException("The target user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Api.TwitchPutGenericAsync<Models.v5.Users.UserBlock>($"/users/{sourceUserId}/blocks/{targetUserId}", ApiVersion.v5, null, accessToken: authToken);
            }
            #endregion
            #region UnblockUser
            public async Task UnblockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Blocks_Edit, authToken);
                if (string.IsNullOrWhiteSpace(sourceUserId)) { throw new BadParameterException("The source user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(targetUserId)) { throw new BadParameterException("The target user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.TwitchDeleteAsync($"/users/{sourceUserId}/blocks/{targetUserId}", ApiVersion.v5, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region ViewerHeartbeatService
            #region CreateUserConnectionToViewerHeartbeatService
            public async Task CreateUserConnectionToViewerHeartbeatServiceAsync(string identifier, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Viewing_Activity_Read, authToken);
                if (string.IsNullOrWhiteSpace(identifier)) { throw new BadParameterException("The identifier is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                string payload = "{\"identifier\": \"" + identifier + "\"}";
                await Api.TwitchPutAsync("/user/vhs", ApiVersion.v5, payload, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region CheckUserConnectionToViewerHeartbeatService
            public Task<Models.v5.ViewerHeartbeatService.VHSConnectionCheck> CheckUserConnectionToViewerHeartbeatServiceAsync(string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Read, authToken);
                return Api.TwitchGetGenericAsync<Models.v5.ViewerHeartbeatService.VHSConnectionCheck>("/user/vhs", ApiVersion.v5, accessToken: authToken);
            }
            #endregion
            #region DeleteUserConnectionToViewerHeartbeatService

            public async Task DeleteUserConnectionToViewerHeartbeatServicechStreamsAsync(string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Viewing_Activity_Read, authToken);
                await Api.TwitchDeleteAsync("/user/vhs", ApiVersion.v5, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #endregion
        }

        public class HelixApi : ApiSection
        {
            public HelixApi(TwitchAPI api) : base(api)
            {
            }

            public Task<Models.Helix.Users.GetUsers.GetUsersResponse> GetUsersAsync(List<string> ids = null, List<string> logins = null, string accessToken = null)
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
                return Api.TwitchGetGenericAsync<Models.Helix.Users.GetUsers.GetUsersResponse>("/users", ApiVersion.Helix, getParams, accessToken);
            }

            public Task<Models.Helix.Users.GetUsersFollows.GetUsersFollowsResponse> GetUsersFollowsAsync(string after = null, string before = null, int first = 20, string fromId = null, string toId = null)
            {
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("first", first.ToString()) };
                if (after != null)
                    getParams.Add(new KeyValuePair<string, string>("after", after));
                if (before != null)
                    getParams.Add(new KeyValuePair<string, string>("before", before));
                if (fromId != null)
                    getParams.Add(new KeyValuePair<string, string>("from_id", fromId));
                if (toId != null)
                    getParams.Add(new KeyValuePair<string, string>("to_id", toId));

                return Api.TwitchGetGenericAsync<Models.Helix.Users.GetUsersFollows.GetUsersFollowsResponse>("/users/follows", ApiVersion.Helix, getParams);
            }

            public async Task PutUsersAsync(string description, string accessToken = null)
            {
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("description", description) };
                await Api.TwitchPutAsync("/users", ApiVersion.Helix, null, getParams, accessToken).ConfigureAwait(false);
            }

            public async Task<Models.Helix.Users.GetUserExtensions.GetUserExtensionsResponse> GetUserExtensionsAsync(string authToken = null)
            {
                return await Api.TwitchGetGenericAsync<Models.Helix.Users.GetUserExtensions.GetUserExtensionsResponse>("/users/extensions/list", ApiVersion.Helix, accessToken: authToken);
            }

            public async Task<Models.Helix.Users.GetUserActiveExtensions.GetUserActiveExtensionsResponse> GetUserActiveExtensionsAsync(string authToken = null)
            {
                return await Api.TwitchGetGenericAsync<Models.Helix.Users.GetUserActiveExtensions.GetUserActiveExtensionsResponse>("/users/extensions", ApiVersion.Helix, accessToken: authToken);
            }

            public async Task<Models.Helix.Users.GetUserActiveExtensions.GetUserActiveExtensionsResponse> UpdateUserExtensionsAsync(List<Models.Helix.Users.UpdateUserExtensions.ExtensionSlot> userExtensionStates, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Editor, authToken);

                Dictionary<string, Models.Helix.Users.UpdateUserExtensions.UserExtensionState> panels = new Dictionary<string, Models.Helix.Users.UpdateUserExtensions.UserExtensionState>();
                Dictionary<string, Models.Helix.Users.UpdateUserExtensions.UserExtensionState> overlays = new Dictionary<string, Models.Helix.Users.UpdateUserExtensions.UserExtensionState>();
                Dictionary<string, Models.Helix.Users.UpdateUserExtensions.UserExtensionState> components = new Dictionary<string, Models.Helix.Users.UpdateUserExtensions.UserExtensionState>();

                foreach(var extension in userExtensionStates)
                {
                    switch(extension.Type)
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
                    }
                }

                JObject json = new JObject();
                Models.Helix.Users.UpdateUserExtensions.Payload p = new Models.Helix.Users.UpdateUserExtensions.Payload();
                if(panels.Count > 0)
                {
                    p.panel = panels;
                }
                if(overlays.Count > 0)
                {
                    p.overlay = overlays;
                }
                if(components.Count > 0)
                {
                    p.component = components;
                }

                json.Add(new JProperty("data", JObject.FromObject(p)));
                string payload = json.ToString();
                return await Api.TwitchPutGenericAsync<Models.Helix.Users.GetUserActiveExtensions.GetUserActiveExtensionsResponse>("/users/extensions", ApiVersion.Helix, payload, accessToken: authToken);
            }
        }
    }
}
