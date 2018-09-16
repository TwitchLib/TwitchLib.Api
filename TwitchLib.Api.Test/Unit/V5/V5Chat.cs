using Xunit;

namespace TwitchLib.Api.Test.Unit.V5
{
    public class V5Chat
    {
        [Fact]
        public async void TestGetChatRoomsByChannel()
        {
            var mockHandler = V5Setup.GetMockHttpCallHandler(GetChatRoomsByChannelResponse);
            var api = new TwitchAPI(http: mockHandler.Object);
            var result = await api.V5.Chat.GetChatRoomsByChannelAsync("44322889", "RandomTokenThatDoesntMatter");

            Assert.True(result.Total == 3);
            Assert.Contains(result.Rooms, x => x.OwnerId == "44322889");
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
