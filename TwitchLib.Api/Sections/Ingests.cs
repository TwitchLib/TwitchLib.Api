using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Interfaces;

namespace TwitchLib.Api.Sections
{
    public class Ingests
    {
        public Ingests(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
        {
            V5 = new V5Api(settings, rateLimiter, http);
        }

        public V5Api V5 { get; }

        public class V5Api : ApiBase
        {
            public V5Api(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
            {
            }

            #region GetIngestServerList

            public Task<Models.v5.Ingests.Ingests> GetIngestServerListAsync()
            {
                return TwitchGetGenericAsync<Models.v5.Ingests.Ingests>("/ingests", ApiVersion.v5);
            }

            #endregion
        }
    }
}