using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreams
{
    /// <summary>
    /// Stream type to select when using GetStreamsAsync
    /// </summary>
    public static class GetStreamsType
    {
        /// <summary>
        /// Filters for all stream types.
        /// </summary>
        public const string All = "all";

        /// <summary>
        /// Filters for actual live streams.
        /// </summary>
        public const string Live = "live";
    }
}
