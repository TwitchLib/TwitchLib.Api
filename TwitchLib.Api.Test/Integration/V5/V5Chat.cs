using Xunit;

namespace TwitchLib.Api.Test.Integration.V5
{
    public class V5Chat
    {

        [Fact]
        public async void TestGetChatRoomsByChannel_Integration()
        {
            var api = new TwitchAPI();
            api.Settings.ClientId = "ivx64a782uvoej252um88xkvxqzq2o";

            var result = await api.V5.Chat.GetChatRoomsByChannelAsync("188854137", "bmiu6z4qjhvfnecn1xwwefo754bd5c");

            Assert.True(result.Total == 2);
            Assert.Contains(result.Rooms, x => x.OwnerId == "188854137");
        }

        private readonly string GetChatRoomsByChannelResponse = @"{
    ""_total"": 3,
    ""rooms"": [
        {
            ""_id"": ""7596b8d6-3eca-4d73-8d0d-c82cc562b8dc"",
            ""owner_id"": ""44322889"",
            ""name"": ""subscribers"",
            ""topic"": ""subscribers only chat"",
            ""is_previewable"": true,
            ""minimum_allowed_role"": ""SUBSCRIBER""
        },
        {
            ""_id"": ""04e762ec-ce8f-4cbc-b6a3-ffc871ab53da"",
            ""owner_id"": ""44322889"",
            ""name"": ""general-chat"",
            ""topic"": ""general chat"",
            ""is_previewable"": true,
            ""minimum_allowed_role"": ""EVERYONE""
        },
        {
            ""_id"": ""7b039890-12ed-4102-b460-16638b3dc0f9"",
            ""owner_id"": ""44322889"",
            ""name"": ""mods-only"",
            ""topic"": ""private room for mods"",
            ""is_previewable"": false,
            ""minimum_allowed_role"": ""MODERATOR""
        }
    ]
}";
    }
}
