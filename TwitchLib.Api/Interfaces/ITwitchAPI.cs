using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.Root;
using TwitchLib.Api.Core.Undocumented;

namespace TwitchLib.Api.Interfaces
{
    public interface ITwitchAPI
    {
        IApiSettings Settings { get; }
        V5.V5 V5 { get; }
        Helix.Helix Helix { get; }
        ThirdParty.ThirdParty ThirdParty { get; }
        Undocumented Undocumented { get; }
        Root Root { get; }
    }
}