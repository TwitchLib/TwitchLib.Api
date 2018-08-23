using TwitchLib.Api.Core.Exceptions;
using Xunit;

namespace TwitchLib.Api.Test.Unit.Helix
{
    public class HelixWebhooks
    {
        [Fact]
        public async void TestGetWebhookSubscriptions()
        {
            var mockHandler = HelixSetup.GetMockHttpCallHandler(GetWebhookSubscriptionsResponseJson);
            var api = new TwitchAPI(http: mockHandler.Object);
            var result = await api.Helix.Webhooks.GetWebhookSubscriptionsAsync(accessToken: "RandomTokenThatDoesntMatter");

            Assert.True(result.Total == 12);
            Assert.Contains(result.Subscriptions, x => x.Callback == "http://example.com/your_callback");
        }

        [Fact]
        public async void TestGetWebhookSubscriptions_AccessTokenFailure()
        {
            var mockHandler = HelixSetup.GetMockHttpCallHandler(GetWebhookSubscriptionsResponseJson);
            var api = new TwitchAPI(http: mockHandler.Object);
           

            await Assert.ThrowsAsync<BadParameterException>(async () => await api.Helix.Webhooks.GetWebhookSubscriptionsAsync());
        }
        private readonly string GetWebhookSubscriptionsResponseJson = @"
            {
               ""total"": 12,
               ""data"": [
                   {
                       ""topic"": ""https://api.twitch.tv/helix/streams?user_id=123"",
                       ""callback"": ""http://example.com/your_callback"",
                       ""expires_at"": ""2018-07-30T20:00:00Z""
                   },
                   {
                       ""topic"": ""https://api.twitch.tv/helix/streams?user_id=345"",
                       ""callback"": ""http://example.com/your_callback"",
                       ""expires_at"": ""2018-07-30T20:03:00Z""
                   }
               ],
               ""pagination"": {
                   ""cursor"": ""eyJiIjpudWxsLCJhIjp7IkN1cnNvciI6IkFYc2laU0k2TVN3aWFTSTZNWDAifX0""
               }
            }";
    }
}
