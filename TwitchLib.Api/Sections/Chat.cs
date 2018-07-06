using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;
using TwitchLib.Api.Models.v5.Chat;

namespace TwitchLib.Api.Sections
{
    public class Chat
    {
        public Chat(TwitchAPI api)
        {
            v5 = new V5Api(api);
        }

        public V5Api v5 { get; }

        public class V5Api : ApiSection
        {
            public V5Api(TwitchAPI api) : base(api)
            {
            }

            #region GetChatBadgesByChannel

            public Task<ChannelBadges> GetChatBadgesByChannelAsync(string channelId)
            {
                if (string.IsNullOrWhiteSpace(channelId))
                    throw new BadParameterException("The channel id is not valid for catching the channel badges. It is not allowed to be null, empty or filled with whitespaces.");

                return Api.TwitchGetGenericAsync<ChannelBadges>($"/chat/{channelId}/badges", ApiVersion.v5);
            }

            #endregion

            #region GetChatEmoticonsBySet

            public Task<EmoteSet> GetChatEmoticonsBySetAsync(List<int> emotesets = null)
            {
                List<KeyValuePair<string, string>> getParams = null;
                if (emotesets != null && emotesets.Count > 0)
                {
                    getParams = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("emotesets", string.Join(",", emotesets))
                    };
                }

                return Api.TwitchGetGenericAsync<EmoteSet>("/chat/emoticon_images", ApiVersion.v5, getParams);
            }

            #endregion

            #region GetAllChatEmoticons

            public Task<AllChatEmotes> GetAllChatEmoticonsAsync()
            {
                return Api.TwitchGetGenericAsync<AllChatEmotes>("/chat/emoticons", ApiVersion.v5);
            }

            #endregion

            #region GetChatRoomsByChannel 

            public Task<ChatRoomsByChannelResponse> GetChatRoomsByChannelAsync(string channelId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Any, authToken);
                return Api.TwitchGetGenericAsync<ChatRoomsByChannelResponse>($"/chat/{channelId}/rooms", ApiVersion.v5, accessToken: authToken);
            }

            #endregion
        }
    }
}