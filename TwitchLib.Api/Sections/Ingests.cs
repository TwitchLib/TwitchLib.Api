using System.Threading.Tasks;
using TwitchLib.Api.Enums;

namespace TwitchLib.Api.Sections
{
    public class Ingests
    {
        public Ingests(TwitchAPI api)
        {
            v5 = new V5(api);
        }
        
        public V5 v5 { get; }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetIngestServerList
            public async Task<Models.v5.Ingests.Ingests> GetIngestServerListAsync()
            {
                return await Api.TwitchGetGenericAsync<Models.v5.Ingests.Ingests>("/ingests", ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
