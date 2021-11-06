using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Core.Enums
{
    public enum ExtensionState
    {
        InTest,
        InReview,
        Rejected,
        Approved,
        Released,
        Deprecated,
        PendingAction,
        AssetsUploaded,
        Deleted
    }
}
