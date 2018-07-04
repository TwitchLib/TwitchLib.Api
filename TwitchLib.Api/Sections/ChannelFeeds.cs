using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;
using TwitchLib.Api.Models.v5.ChannelFeed;

namespace TwitchLib.Api.Sections
{
    public class ChannelFeeds
    {
        public ChannelFeeds(TwitchAPI api)
        {
            v5 = new V5Api(api);
        }

        public V5Api v5 { get; }

        public class V5Api : ApiSection
        {
            public V5Api(TwitchAPI api) : base(api)
            {
            }

            #region GetMultipleFeedPosts

            public Task<MultipleFeedPosts> GetMultipleFeedPostsAsync(string channelId, long? limit = null, string cursor = null, long? comments = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Read, authToken);
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (limit.HasValue && !(limit.Value > 0 && limit.Value < 101))
                {
                    throw new BadParameterException("The specified limit is not valid. It must be a value between 1 and 100.");
                }

                if (comments.HasValue && !(comments.Value >= 0 && comments.Value < 6))
                {
                    throw new BadParameterException("The specified comment limit is not valid. It must be a value between 0 and 5");
                }

                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    getParams.Add(new KeyValuePair<string, string>("cursor", cursor));
                if (comments != null && comments < 6 && comments >= 0)
                    getParams.Add(new KeyValuePair<string, string>("comments", comments.Value.ToString()));

                return Api.TwitchGetGenericAsync<Models.v5.ChannelFeed.MultipleFeedPosts>($"/feed/{channelId}/posts", ApiVersion.v5, getParams, accessToken: authToken);
            }

            #endregion

            #region GetFeedPosts

            public Task<FeedPost> GetFeedPostAsync(string channelId, string postId, long? comments = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Read, authToken);
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(postId))
                {
                    throw new BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (comments.HasValue && !(comments.Value >= 0 && comments.Value < 6))
                {
                    throw new BadParameterException("The specified comment limit is not valid. It must be a value between 0 and 5");
                }

                var getParams = new List<KeyValuePair<string, string>>();
                if (comments != null && comments < 6 && comments >= 0)
                    getParams.Add(new KeyValuePair<string, string>("comments", comments.Value.ToString()));
                return Api.TwitchGetGenericAsync<FeedPost>($"/feed/{channelId}/posts/{postId}", ApiVersion.v5, getParams, accessToken: authToken);
            }

            #endregion

            #region CreateFeedPost

            public Task<FeedPostCreation> CreateFeedPostAsync(string channelId, string content, bool? share = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(content))
                {
                    throw new BadParameterException("The content is not valid for creating channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                var getParams = new List<KeyValuePair<string, string>>();
                if (share.HasValue)
                {
                    getParams.Add(new KeyValuePair<string, string>("share", share.Value.ToString()));
                }

                string payload = "{\"content\": \"" + content + "\"}";
                return Api.TwitchPostGenericAsync<FeedPostCreation>($"/feed/{channelId}/posts", ApiVersion.v5, payload, getParams, authToken);
            }

            #endregion

            #region DeleteFeedPost

            public Task<FeedPost> DeleteFeedPostAsync(string channelId, string postId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(postId))
                {
                    throw new BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                return Api.TwitchDeleteGenericAsync<FeedPost>($"/feed/{channelId}/posts/{postId}", ApiVersion.v5, authToken);
            }

            #endregion

            #region CreateReactionToFeedPost

            public Task<FeedPostReactionPost> CreateReactionToFeedPostAsync(string channelId, string postId, string emoteId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(postId))
                {
                    throw new BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(emoteId))
                {
                    throw new BadParameterException("The emote id is not valid for posting a channel feed post reaction. It is not allowed to be null, empty or filled with whitespaces.");
                }

                var getParams = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("emote_id", emoteId)};
                return Api.TwitchPostGenericAsync<Models.v5.ChannelFeed.FeedPostReactionPost>($"/feed/{channelId}/posts/{postId}/reactions", ApiVersion.v5, null, getParams, authToken);
            }

            #endregion

            #region DeleteReactionToFeedPost

            public Task DeleteReactionToFeedPostAsync(string channelId, string postId, string emoteId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(postId))
                {
                    throw new BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(emoteId))
                {
                    throw new BadParameterException("The emote id is not valid for posting a channel reaction. It is not allowed to be null, empty or filled with whitespaces.");
                }

                var getParams = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("emote_id", emoteId)};
                return Api.TwitchDeleteAsync($"/feed/{channelId}/posts/{postId}/reactions", ApiVersion.v5, getParams, accessToken: authToken);
            }

            #endregion

            #region GetFeedComments

            public Task<FeedPostComments> GetFeedCommentsAsync(string channelId, string postId, long? limit = null, string cursor = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Read, authToken);
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(postId))
                {
                    throw new BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (limit.HasValue && !(limit.Value > 0 && limit.Value < 101))
                {
                    throw new BadParameterException("The specified limit is not valid. It must be a value between 1 and 100.");
                }

                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    getParams.Add(new KeyValuePair<string, string>("cursor", cursor));
                return Api.TwitchGetGenericAsync<Models.v5.ChannelFeed.FeedPostComments>($"/feed/{channelId}/posts/{postId}/comments", ApiVersion.v5, getParams, authToken);
            }

            #endregion

            #region CreateFeedComment

            public Task<FeedPostComment> CreateFeedCommentAsync(string channelId, string postId, string content, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(postId))
                {
                    throw new BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(content))
                {
                    throw new BadParameterException("The content is not valid for commenting channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                string payload = "{\"content\": \"" + content + "\"}";
                return Api.TwitchPostGenericAsync<FeedPostComment>($"/feed/{channelId}/posts/{postId}/comments", ApiVersion.v5, payload, accessToken: authToken);
            }

            #endregion

            #region DeleteFeedComment

            public Task<Models.v5.ChannelFeed.FeedPostComment> DeleteFeedCommentAsync(string channelId, string postId, string commentId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(postId))
                {
                    throw new BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(commentId))
                {
                    throw new BadParameterException("The comment id is not valid for fetching channel feed post comments. It is not allowed to be null, empty or filled with whitespaces.");
                }

                return Api.TwitchDeleteGenericAsync<Models.v5.ChannelFeed.FeedPostComment>($"/feed/{channelId}/posts/{postId}/comments/{commentId}", ApiVersion.v5, authToken);
            }

            #endregion

            #region CreateReactionToFeedComments

            public Task<Models.v5.ChannelFeed.FeedPostReactionPost> CreateReactionToFeedCommentAsync(string channelId, string postId, string commentId, string emoteId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(postId))
                {
                    throw new BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(commentId))
                {
                    throw new BadParameterException("The comment id is not valid for fetching channel feed post comments. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(emoteId))
                {
                    throw new BadParameterException("The emote id is not valid for posting a channel feed post comment reaction. It is not allowed to be null, empty or filled with whitespaces.");
                }

                var getParams = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("emote_id", emoteId)};
                return Api.TwitchPostGenericAsync<Models.v5.ChannelFeed.FeedPostReactionPost>($"/feed/{channelId}/posts/{postId}/comments/{commentId}/reactions", ApiVersion.v5, null, getParams, authToken);
            }

            #endregion

            #region DeleteReactionToFeedComments

            public Task DeleteReactionToFeedCommentAsync(string channelId, string postId, string commentId, string emoteId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId))
                {
                    throw new BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(postId))
                {
                    throw new BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(commentId))
                {
                    throw new BadParameterException( "The comment id is not valid for fetching channel feed post comments. It is not allowed to be null, empty or filled with whitespaces.");
                }

                if (string.IsNullOrWhiteSpace(emoteId))
                {
                    throw new BadParameterException(
                        "The emote id is not valid for posting a channel feed post comment reaction. It is not allowed to be null, empty or filled with whitespaces.");
                }

                var getParams = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("emote_id", emoteId)};
                return Api.TwitchDeleteAsync($"/feed/{channelId}/posts/{postId}/comments/{commentId}/reactions", ApiVersion.v5, getParams, authToken);
            }

            #endregion
        }
    }
}