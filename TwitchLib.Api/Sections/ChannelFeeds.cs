using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;

namespace TwitchLib.Api.Sections
{
    public class ChannelFeeds
    {
        public ChannelFeeds(TwitchAPI api)
        {
            v5 = new V5(api);
        }
        
        public V5 v5 { get; }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetMultipleFeedPosts
            public async Task<Models.v5.ChannelFeed.MultipleFeedPosts> GetMultipleFeedPostsAsync(string channelId, long? limit = null, string cursor = null, long? comments = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Read, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (limit.HasValue && !(limit.Value > 0 && limit.Value < 101)) { throw new BadParameterException("The specified limit is not valid. It must be a value between 1 and 100."); }
                if (comments.HasValue && !(comments.Value >= 0 && comments.Value < 6)) { throw new BadParameterException("The specified comment limit is not valid. It must be a value between 0 and 5"); }
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    getParams.Add(new KeyValuePair<string, string>("cursor", cursor));
                if (comments != null && comments < 6 && comments >= 0)
                    getParams.Add(new KeyValuePair<string, string>("comments", comments.Value.ToString()));

                return await Api.TwitchGetGenericAsync<Models.v5.ChannelFeed.MultipleFeedPosts>($"/feed/{channelId}/posts", ApiVersion.v5, getParams, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region GetFeedPosts
            public async Task<Models.v5.ChannelFeed.FeedPost> GetFeedPostAsync(string channelId, string postId, long? comments = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Read, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (comments.HasValue && !(comments.Value >= 0 && comments.Value < 6)) { throw new BadParameterException("The specified comment limit is not valid. It must be a value between 0 and 5"); }

                var getParams = new List<KeyValuePair<string, string>>();
                if (comments != null && comments < 6 && comments >= 0)
                    getParams.Add(new KeyValuePair<string, string>("comments", comments.Value.ToString()));
                return await Api.TwitchGetGenericAsync<Models.v5.ChannelFeed.FeedPost>($"/feed/{channelId}/posts/{postId}", ApiVersion.v5, getParams, accessToken: authToken).ConfigureAwait(false);
            }
        
            #endregion
            #region CreateFeedPost
            public async Task<Models.v5.ChannelFeed.FeedPostCreation> CreateFeedPostAsync(string channelId, string content, bool? share = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(content)) { throw new BadParameterException("The content is not valid for creating channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                var getParams = new List<KeyValuePair<string, string>>();
                if (share.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("share", share.Value.ToString()));
                string payload = "{\"content\": \"" + content + "\"}";
                return await Api.PostGenericAsync<Models.v5.ChannelFeed.FeedPostCreation>($"/feed/{channelId}/posts", payload, getParams, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region DeleteFeedPost
            public async Task<Models.v5.ChannelFeed.FeedPost> DeleteFeedPostAsync(string channelId, string postId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.TwitchDeleteGenericAsync<Models.v5.ChannelFeed.FeedPost>($"/feed/{channelId}/posts/{postId}", ApiVersion.v5, authToken).ConfigureAwait(false);
            }
            #endregion
            #region CreateReactionToFeedPost
            public async Task<Models.v5.ChannelFeed.FeedPostReactionPost> CreateReactionToFeedPostAsync(string channelId, string postId, string emoteId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(emoteId)) { throw new BadParameterException("The emote id is not valid for posting a channel feed post reaction. It is not allowed to be null, empty or filled with whitespaces."); }
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("emote_id", emoteId) };
                return await Api.TwitchPostGenericAsync<Models.v5.ChannelFeed.FeedPostReactionPost>($"/feed/{channelId}/posts/{postId}/reactions", ApiVersion.v5, null, getParams, authToken).ConfigureAwait(false);
            }
            #endregion
            #region DeleteReactionToFeedPost
            public async Task DeleteReactionToFeedPostAsync(string channelId, string postId, string emoteId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(emoteId)) { throw new BadParameterException("The emote id is not valid for posting a channel reaction. It is not allowed to be null, empty or filled with whitespaces."); }
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("emote_id", emoteId) };
                await Api.TwitchDeleteAsync($"/feed/{channelId}/posts/{postId}/reactions", ApiVersion.v5, getParams, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region GetFeedComments
            public async Task<Models.v5.ChannelFeed.FeedPostComments> GetFeedCommentsAsync(string channelId, string postId, long? limit = null, string cursor = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Read, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (limit.HasValue && !(limit.Value > 0 && limit.Value < 101)) { throw new BadParameterException("The specified limit is not valid. It must be a value between 1 and 100."); }
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    getParams.Add(new KeyValuePair<string, string>("cursor", cursor));
                return await Api.TwitchGetGenericAsync<Models.v5.ChannelFeed.FeedPostComments>($"/feed/{channelId}/posts/{postId}/comments", ApiVersion.v5, getParams, authToken).ConfigureAwait(false);
            }
            #endregion
            #region CreateFeedComment
            public async Task<Models.v5.ChannelFeed.FeedPostComment> CreateFeedCommentAsync(string channelId, string postId, string content, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(content)) { throw new BadParameterException("The content is not valid for commenting channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                string payload = "{\"content\": \"" + content + "\"}";
                return await Api.TwitchPostGenericAsync<Models.v5.ChannelFeed.FeedPostComment>($"/feed/{channelId}/posts/{postId}/comments", ApiVersion.v5, payload, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region DeleteFeedComment
            public async Task<Models.v5.ChannelFeed.FeedPostComment> DeleteFeedCommentAsync(string channelId, string postId, string commentId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(commentId)) { throw new BadParameterException("The comment id is not valid for fetching channel feed post comments. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.TwitchDeleteGenericAsync<Models.v5.ChannelFeed.FeedPostComment>($"/feed/{channelId}/posts/{postId}/comments/{commentId}", ApiVersion.v5, authToken).ConfigureAwait(false);
            }
            #endregion
            #region CreateReactionToFeedComments
            public async Task<Models.v5.ChannelFeed.FeedPostReactionPost> CreateReactionToFeedCommentAsync(string channelId, string postId, string commentId, string emoteId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(commentId)) { throw new BadParameterException("The comment id is not valid for fetching channel feed post comments. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(emoteId)) { throw new BadParameterException("The emote id is not valid for posting a channel feed post comment reaction. It is not allowed to be null, empty or filled with whitespaces."); }
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("emote_id", emoteId) };
                return await Api.TwitchPostGenericAsync<Models.v5.ChannelFeed.FeedPostReactionPost>($"/feed/{channelId}/posts/{postId}/comments/{commentId}/reactions", ApiVersion.v5, null, getParams, authToken).ConfigureAwait(false);
            }
            #endregion
            #region DeleteReactionToFeedComments
            public async Task DeleteReactionToFeedCommentAsync(string channelId, string postId, string commentId, string emoteId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(commentId)) { throw new BadParameterException("The comment id is not valid for fetching channel feed post comments. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(emoteId)) { throw new BadParameterException("The emote id is not valid for posting a channel feed post comment reaction. It is not allowed to be null, empty or filled with whitespaces."); }
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("emote_id", emoteId) };
                await Api.TwitchDeleteAsync($"/feed/{channelId}/posts/{postId}/comments/{commentId}/reactions", ApiVersion.v5, getParams, authToken).ConfigureAwait(false);
            }
            #endregion
        }
    }
}