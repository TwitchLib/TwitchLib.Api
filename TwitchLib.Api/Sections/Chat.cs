using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;

namespace TwitchLib.Api.Sections
{
    public class Chat
    {
        public Chat(TwitchAPI api)
        {
            v3 = new V3(api);
            v5 = new V5(api);
        }

        public V3 v3 { get; }
        public V5 v5 { get; }

        public class V3 : ApiSection
        {
            public V3(TwitchAPI api) : base(api)
            {
            }
            #region GetBadges
            public async Task<Models.v3.Chat.BadgesResponse> GetBadgesAsync(string channel)
            {
                return await Api.GetGenericAsync<Models.v3.Chat.BadgesResponse>($"{Api.baseV3}chat/{channel}/badges", null, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetAllEmoticons
            public async Task<Models.v3.Chat.AllEmoticonsResponse> GetAllEmoticonsAsync()
            {
                return await Api.GetGenericAsync<Models.v3.Chat.AllEmoticonsResponse>($"{Api.baseV3}chat/emoticons", null, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetEmoticonsBySets
            public async Task<Models.v3.Chat.EmoticonSetsResponse> GetEmoticonsBySetsAsync(IEnumerable<int> emotesets)
            {
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("emotesets", string.Join(",", emotesets)) };
                return await Api.GetGenericAsync<Models.v3.Chat.EmoticonSetsResponse>($"{Api.baseV3}chat/emoticon_images", getParams, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetChatBadgesByChannel
            public async Task<Models.v5.Chat.ChannelBadges> GetChatBadgesByChannelAsync(string channelId)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid for catching the channel badges. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Models.v5.Chat.ChannelBadges>($"{Api.baseV5}chat/{channelId}/badges").ConfigureAwait(false);
            }
            #endregion
            #region GetChatEmoticonsBySet
            public async Task<Models.v5.Chat.EmoteSet> GetChatEmoticonsBySetAsync(List<int> emotesets = null)
            {
                List<KeyValuePair<string, string>> getParams = null;
                if(emotesets != null && emotesets.Count > 0)
                    getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("emotesets", string.Join(",", emotesets)) };
                return await Api.GetGenericAsync<Models.v5.Chat.EmoteSet>($"{Api.baseV5}chat/emoticon_images", getParams).ConfigureAwait(false);
            }
            #endregion
            #region GetAllChatEmoticons
            public async Task<Models.v5.Chat.AllChatEmotes> GetAllChatEmoticonsAsync()
            {
                return await Api.GetGenericAsync<Models.v5.Chat.AllChatEmotes>($"{Api.baseV5}chat/emoticons").ConfigureAwait(false);
            }
            #endregion
            #region GetChatRoomsByChannel 
            public async Task<Models.v5.Chat.ChatRoomsByChannelResponse> GetChatRoomsByChannelAsync(string channelId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Any, authToken);
                return await Api.GetGenericAsync<Models.v5.Chat.ChatRoomsByChannelResponse>($"{Api.baseV5}chat/{channelId}/rooms", null, authToken).ConfigureAwait(false);
            }
            #endregion
        }
    }
}