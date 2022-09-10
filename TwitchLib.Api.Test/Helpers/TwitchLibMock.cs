using System;
using System.Collections.Generic;
using Moq;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Test.Helpers
{
    public class TwitchLibMock
    {
        public static TwitchAPI TwitchApi(params (string url, string response)[] urlResponses)
        {
            return TwitchApi(HttpCallHandler(urlResponses));
        }

        public static TwitchAPI TwitchApi(Mock<IHttpCallHandler> mockHandler)
        {
            var api = new TwitchAPI(http: mockHandler.Object)
            {
                Settings =
                {
                    ClientId = Guid.NewGuid().ToString(),
                    AccessToken = Guid.NewGuid().ToString()
                }
            };
            return api;
        }

        public static void ResetHttpCallHandlerResponses(Mock<IHttpCallHandler> mockHandler, params (string url, string response)[] urlResponses)
        {
            mockHandler.Reset();

            foreach (var (url, response) in urlResponses)
            {
                mockHandler
                    .Setup(x => x.GeneralRequestAsync(It.Is<string>(y => new Uri(y).GetLeftPart(UriPartial.Path) == url), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ApiVersion>(), It.IsAny<string>(), It.IsAny<string>()))
                    .ReturnsAsync(new KeyValuePair<int, string>(200, response));
            }
        }

        public static Mock<IHttpCallHandler> HttpCallHandler(params (string url, string response)[] urlResponses)
        {
            var mockHandler = new Mock<IHttpCallHandler>();

            foreach (var (url, response) in urlResponses)
            {
                mockHandler
                    .Setup(x => x.GeneralRequestAsync(It.Is<string>(y => new Uri(y).GetLeftPart(UriPartial.Path) == url), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ApiVersion>(), It.IsAny<string>(), It.IsAny<string>()))
                    .ReturnsAsync(new KeyValuePair<int, string>(200, response));
            }
            
            return mockHandler;
        }
    }
}