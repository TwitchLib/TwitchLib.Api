using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.V5.Models.Communities;

namespace TwitchLib.Api.V5
{
    public class Communities : ApiBase
    {
        public Communities(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetCommunityByName

        public Task<Community> GetCommunityByNameAsync(string communityName)
        {
            if (string.IsNullOrWhiteSpace(communityName))
                throw new BadParameterException("The community name is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("name", communityName) };
            return TwitchGetGenericAsync<Community>("/communities", ApiVersion.V5, getParams);
        }

        #endregion

        #region GetCommunityByID

        public Task<Community> GetCommunityByIDAsync(string communityId)
        {
            if (string.IsNullOrWhiteSpace(communityId))
                throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            return TwitchGetGenericAsync<Community>($"/communities/{communityId}", ApiVersion.V5);
        }

        #endregion

        #region UpdateCommunity

        public async Task UpdateCommunityAsync(string communityId, string summary = null, string description = null, string rules = null, string email = null, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Communities_Edit, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(communityId))
                throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            var data = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrEmpty(summary))
                data.Add(new KeyValuePair<string, string>("status", "\"" + summary + "\""));
            if (!string.IsNullOrEmpty(description))
                data.Add(new KeyValuePair<string, string>("description", "\"" + description + "\""));
            if (!string.IsNullOrEmpty(rules))
                data.Add(new KeyValuePair<string, string>("rules", "\"" + rules + "\""));
            if (!string.IsNullOrEmpty(email))
                data.Add(new KeyValuePair<string, string>("email", "\"" + email + "\""));

            var payload = "";
            switch (data.Count)
            {
                case 0:
                    throw new BadParameterException("At least one parameter must be specified: summary, description, rules, email.");
                case 1:
                    payload = $"\"{data[0].Key}\": {data[0].Value}";
                    break;
                default:
                    for (var i = 0; i < data.Count; i++) payload = data.Count - i > 1 ? $"{payload}\"{data[i].Key}\": {data[i].Value}," : $"{payload}\"{data[i].Key}\": {data[i].Value}";

                    break;
            }

            payload = "{" + payload + "}";

            await TwitchPutAsync($"/communities/{communityId}", ApiVersion.V5, payload, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region GetTopCommunities

        public Task<TopCommunities> GetTopCommunitiesAsync(long? limit = null, string cursor = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();
            if (limit.HasValue)
                getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
            if (!string.IsNullOrEmpty(cursor))
                getParams.Add(new KeyValuePair<string, string>("cursor", cursor));

            return TwitchGetGenericAsync<TopCommunities>("/communities/top", ApiVersion.V5, getParams);
        }

        #endregion

        #region GetCommunityBannedUsers

        public async Task<BannedUsers> GetCommunityBannedUsersAsync(string communityId, long? limit = null, string cursor = null, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Communities_Moderate, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(communityId))
                throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            var getParams = new List<KeyValuePair<string, string>>();
            if (limit.HasValue)
                getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
            if (!string.IsNullOrEmpty(cursor))
                getParams.Add(new KeyValuePair<string, string>("cursor", cursor));

            return await TwitchGetGenericAsync<BannedUsers>($"/communities/{communityId}/bans", ApiVersion.V5, getParams, authToken).ConfigureAwait(false);
        }

        #endregion

        #region BanCommunityUser

        public async Task BanCommunityUserAsync(string communityId, string userId, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Communities_Moderate, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(communityId))
                throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            await TwitchPutAsync($"/communities/{communityId}/bans/{userId}", ApiVersion.V5, null, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region UnBanCommunityUser

        public async Task UnBanCommunityUserAsync(string communityId, string userId, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Communities_Moderate, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(communityId))
                throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            await TwitchDeleteAsync($"/communities/{communityId}/bans/{userId}", ApiVersion.V5, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region CreateCommunityAvatarImage

        public async Task CreateCommunityAvatarImageAsync(string communityId, string avatarImage, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Communities_Edit, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(communityId))
                throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(avatarImage))
                throw new BadParameterException("The avatar image is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            var payload = "{\"avatar_image\": \"" + avatarImage + "\"}";
            await TwitchPostAsync($"/communities/{communityId}/images/avatar", ApiVersion.V5, payload, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region DeleteCommunityAvatarImage

        public async Task DeleteCommunityAvatarImageAsync(string communityId, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Communities_Edit, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(communityId))
                throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            await TwitchDeleteAsync($"/communities/{communityId}/images/avatar", ApiVersion.V5, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region CreateCommunityCoverImage

        public async Task CreateCommunityCoverImageAsync(string communityId, string coverImage, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Communities_Edit, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(communityId))
                throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(coverImage))
                throw new BadParameterException("The cover image is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            var payload = "{\"cover_image\": \"" + coverImage + "\"}";
            await TwitchPostAsync($"/communities/{communityId}/images/cover", ApiVersion.V5, payload, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region DeleteCommunityCoverImage

        public async Task DeleteCommunityCoverImageAsync(string communityId, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Communities_Edit, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(communityId))
                throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            await TwitchDeleteAsync($"/communities/{communityId}/images/cover", ApiVersion.V5, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region GetCommunityModerators

        public async Task<Moderators> GetCommunityModeratorsAsync(string communityId, string authToken)
        {
            await DynamicScopeValidationAsync(AuthScopes.Communities_Edit, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(communityId))
                throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            return await TwitchGetGenericAsync<Moderators>($"/communities/{communityId}/moderators", ApiVersion.V5, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region AddCommunityModerator

        public async Task AddCommunityModeratorAsync(string communityId, string userId, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Communities_Edit, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(communityId))
                throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            await TwitchPutAsync($"/communities/{communityId}/moderators/{userId}", ApiVersion.V5, null, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region DeleteCommunityModerator

        public async Task DeleteCommunityModeratorAsync(string communityId, string userId, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Communities_Edit, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(communityId))
                throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            await TwitchDeleteAsync($"/communities/{communityId}/moderators/{userId}", ApiVersion.V5, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region GetCommunityPermissions

        public async Task<Dictionary<string, bool>> GetCommunityPermissionsAsync(string communityId, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Any, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(communityId))
                throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            return await TwitchGetGenericAsync<Dictionary<string, bool>>($"/communities/{communityId}/permissions", ApiVersion.V5, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region ReportCommunityViolation

        public async Task ReportCommunityViolationAsync(string communityId, string channelId)
        {
            if (string.IsNullOrWhiteSpace(communityId))
                throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(channelId))
                throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            var payload = "{\"channel_id\": \"" + channelId + "\"}";
            await TwitchPostAsync($"/communities/{communityId}/report_channel", ApiVersion.V5, payload).ConfigureAwait(false);
        }

        #endregion

        #region GetCommunityTimedOutUsers

        public async Task<TimedOutUsers> GetCommunityTimedOutUsersAsync(string communityId, long? limit = null, string cursor = null, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Communities_Moderate, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(communityId))
                throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            var getParams = new List<KeyValuePair<string, string>>();
            if (limit.HasValue)
                getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
            if (!string.IsNullOrEmpty(cursor))
                getParams.Add(new KeyValuePair<string, string>("cursor", cursor));

            return await TwitchGetGenericAsync<TimedOutUsers>($"/communities/{communityId}/timeouts", ApiVersion.V5, getParams, authToken).ConfigureAwait(false);
        }

        #endregion

        #region AddCommunityTimedOutUser

        public async Task AddCommunityTimedOutUserAsync(string communityId, string userId, int duration, string reason = null, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Communities_Moderate, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(communityId))
                throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            var payload = "{\"duration\": \"" + duration + "\"" + (!string.IsNullOrWhiteSpace(reason) ? ", \"reason\": \"" + reason + "\"}" : "}");
            await TwitchPutAsync($"/communities/{communityId}/timeouts/{userId}", ApiVersion.V5, payload, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region DeleteCommunityTimedOutUser

        public async Task DeleteCommunityTimedOutUserAsync(string communityId, string userId, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Communities_Moderate, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(communityId))
                throw new BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            await TwitchDeleteAsync($"/communities/{communityId}/timeouts/{userId}", ApiVersion.V5, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion
    }
}
