using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;

namespace TwitchLib.Api.Sections
{
    public class Communities
    {
        public Communities(TwitchAPI api)
        {
            v5 = new V5(api);
        }

        public V5 v5 { get; }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetCommunityByName
            public async Task<Models.v5.Communities.Community> GetCommunityByNameAsync(string communityName)
            {
                if (string.IsNullOrWhiteSpace(communityName)) { throw new BadParameterException("The community name is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("name", communityName) };
                return await Api.GetGenericAsync<Models.v5.Communities.Community>($"{Api.baseV5}communities", getParams).ConfigureAwait(false);
            }
            #endregion
            #region GetCommunityByID
            public async Task<Models.v5.Communities.Community> GetCommunityByIDAsync(string communityId)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Models.v5.Communities.Community>($"{Api.baseV5}communities/{communityId}").ConfigureAwait(false);
            }
            #endregion
            #region UpdateCommunity
            public async Task UpdateCommunityAsync(string communityId, string summary = null, string description = null, string rules = null, string email = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Communities_Edit, authToken);
                if (string.IsNullOrWhiteSpace(communityId)) { throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                var datas = new List<KeyValuePair<string, string>>();
                if (!string.IsNullOrEmpty(summary))
                    datas.Add(new KeyValuePair<string, string>("status", "\"" + summary + "\""));
                if (!string.IsNullOrEmpty(description))
                    datas.Add(new KeyValuePair<string, string>("description", "\"" + description + "\""));
                if (!string.IsNullOrEmpty(rules))
                    datas.Add(new KeyValuePair<string, string>("rules", "\"" + rules + "\""));
                if (!string.IsNullOrEmpty(email))
                    datas.Add(new KeyValuePair<string, string>("email", "\"" + email + "\""));

                var payload = "";
                switch (datas.Count)
                {
                    case 0:
                        throw new BadParameterException("At least one parameter must be specified: summary, description, rules, email.");
                    case 1:
                        payload = $"\"{datas[0].Key}\": {datas[0].Value}";
                        break;
                    default:
                        for (var i = 0; i < datas.Count; i++)
                        {
                            payload = (datas.Count - i) > 1 ? $"{payload}\"{datas[i].Key}\": {datas[i].Value}," : $"{payload}\"{datas[i].Key}\": {datas[i].Value}";
                        }
                        break;
                }

                payload = "{" + payload + "}";

                await Api.PutAsync($"{Api.baseV5}communities/{communityId}", payload, null, authToken).ConfigureAwait(false);
            }
            #endregion
            #region GetTopCommunities
            public async Task<Models.v5.Communities.TopCommunities> GetTopCommunitiesAsync(long? limit = null, string cursor = null)
            {
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    getParams.Add(new KeyValuePair<string, string>("cursor", cursor));

                return await Api.GetGenericAsync<Models.v5.Communities.TopCommunities>($"{Api.baseV5}communities/top", getParams).ConfigureAwait(false);
            }
            #endregion
            #region GetCommunityBannedUsers
            public async Task<Models.v5.Communities.BannedUsers> GetCommunityBannedUsersAsync(string communityId, long? limit = null, string cursor = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Communities_Moderate, authToken);
                if (string.IsNullOrWhiteSpace(communityId)) { throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    getParams.Add(new KeyValuePair<string, string>("cursor", cursor));

                return await Api.GetGenericAsync<Models.v5.Communities.BannedUsers>($"{Api.baseV5}communities/{communityId}/bans", getParams, authToken).ConfigureAwait(false);
            }
            #endregion
            #region BanCommunityUser
            public async Task BanCommunityUserAsync(string communityId, string userId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Communities_Moderate, authToken);
                if (string.IsNullOrWhiteSpace(communityId)) { throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.PutAsync($"{Api.baseV5}communities/{communityId}/bans/{userId}", null, null, authToken).ConfigureAwait(false);
            }
            #endregion
            #region UnBanCommunityUser
            public async Task UnBanCommunityUserAsync(string communityId, string userId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Communities_Moderate, authToken);
                if (string.IsNullOrWhiteSpace(communityId)) { throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.DeleteAsync($"{Api.baseV5}communities/{communityId}/bans/{userId}", null, authToken).ConfigureAwait(false);
            }
            #endregion
            #region CreateCommunityAvatarImage
            public async Task CreateCommunityAvatarImageAsync(string communityId, string avatarImage, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Communities_Edit, authToken);
                if (string.IsNullOrWhiteSpace(communityId)) { throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(avatarImage)) { throw new BadParameterException("The avatar image is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.PostAsync($"{Api.baseV5}communities/{communityId}/images/avatar", "{\"avatar_image\": \"" + @avatarImage + "\"}", null, authToken);
            }
            #endregion
            #region DeleteCommunityAvatarImage
            public async Task DeleteCommunityAvatarImageAsync(string communityId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Communities_Edit, authToken);
                if (string.IsNullOrWhiteSpace(communityId)) { throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.DeleteAsync($"{Api.baseV5}communities/{communityId}/images/avatar", null, authToken).ConfigureAwait(false);
            }
            #endregion
            #region CreateCommunityCoverImage
            public async Task CreateCommunityCoverImageAsync(string communityId, string coverImage, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Communities_Edit, authToken);
                if (string.IsNullOrWhiteSpace(communityId)) { throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(coverImage)) { throw new BadParameterException("The cover image is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.PostAsync($"{Api.baseV5}communities/{communityId}/images/cover", "{\"cover_image\": \"" + @coverImage + "\"}", null, authToken).ConfigureAwait(false);
            }
            #endregion
            #region DeleteCommunityCoverImage
            public async Task DeleteCommunityCoverImageAsync(string communityId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Communities_Edit, authToken);
                if (string.IsNullOrWhiteSpace(communityId)) { throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.DeleteAsync($"{Api.baseV5}communities/{communityId}/images/cover", null, authToken).ConfigureAwait(false);
            }
            #endregion
            #region GetCommunityModerators
            public async Task<Models.v5.Communities.Moderators> GetCommunityModeratorsAsync(string communityId, string authToken)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Communities_Edit, authToken);
                if (string.IsNullOrWhiteSpace(communityId)) { throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Models.v5.Communities.Moderators>($"{Api.baseV5}communities/{communityId}/moderators", null, authToken).ConfigureAwait(false);
            }
            #endregion
            #region AddCommunityModerator
            public async Task AddCommunityModeratorAsync(string communityId, string userId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Communities_Edit, authToken);
                if (string.IsNullOrWhiteSpace(communityId)) { throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.PutAsync($"{Api.baseV5}communities/{communityId}/moderators/{userId}", null, null, authToken).ConfigureAwait(false);
            }
            #endregion
            #region DeleteCommunityModerator
            public async Task DeleteCommunityModeratorAsync(string communityId, string userId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Communities_Edit, authToken);
                if (string.IsNullOrWhiteSpace(communityId)) { throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.DeleteAsync($"{Api.baseV5}communities/{communityId}/moderators/{userId}", null, authToken).ConfigureAwait(false);
            }
            #endregion
            #region GetCommunityPermissions
            public async Task<Dictionary<string, bool>> GetCommunityPermissionsAsync(string communityId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Any, authToken);
                if (string.IsNullOrWhiteSpace(communityId)) { throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Dictionary<string, bool>>($"{Api.baseV5}communities/{communityId}/permissions", null, authToken).ConfigureAwait(false);
            }
            #endregion
            #region ReportCommunityViolation
            public async Task ReportCommunityViolationAsync(string communityId, string channelId)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.PostAsync($"{Api.baseV5}communities/{communityId}/report_channel", "{\"channel_id\": \"" + channelId + "\"}").ConfigureAwait(false);
            }
            #endregion
            #region GetCommunityTimedOutUsers
            public async Task<Models.v5.Communities.TimedOutUsers> GetCommunityTimedOutUsersAsync(string communityId, long? limit = null, string cursor = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Communities_Moderate, authToken);
                if (string.IsNullOrWhiteSpace(communityId)) { throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    getParams.Add(new KeyValuePair<string, string>("cursor", cursor));

                return await Api.GetGenericAsync<Models.v5.Communities.TimedOutUsers>($"{Api.baseV5}communities/{communityId}/timeouts", getParams, authToken).ConfigureAwait(false);
            }
            #endregion
            #region AddCommunityTimedOutUser
            public async Task AddCommunityTimedOutUserAsync(string communityId, string userId, int duration, string reason = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Communities_Moderate, authToken);
                if (string.IsNullOrWhiteSpace(communityId)) { throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                var payload = "{\"duration\": \"" + duration + "\"" + ((!string.IsNullOrWhiteSpace(reason)) ? ", \"reason\": \"" + reason + "\"}" : "}");
                await Api.PutAsync($"{Api.baseV5}communities/{communityId}/timeouts/{userId}", payload, null, authToken).ConfigureAwait(false);
            }
            #endregion
            #region DeleteCommunityTimedOutUser
            public async Task DeleteCommunityTimedOutUserAsync(string communityId, string userId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Communities_Moderate, authToken);
                if (string.IsNullOrWhiteSpace(communityId)) { throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.DeleteAsync($"{Api.baseV5}communities/{communityId}/timeouts/{userId}", null, authToken).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
