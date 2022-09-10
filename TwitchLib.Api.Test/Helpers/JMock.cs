using System;
using System.Linq.Expressions;
using Moq;
using Newtonsoft.Json;

namespace TwitchLib.Api.Test.Helpers
{
    public class JMock
    {
        public static string Of<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return JsonConvert.SerializeObject(Mock.Of(predicate), new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}