using System.Collections.Generic;

namespace TwitchLib.Api.Core.Extensions.System
{
    /// <summary>
    /// Enumerable Extension methods
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Adds an Enumerable of Type T to a List of Type T
        /// </summary>
        /// <typeparam name="T">Type of the Enumerable and List</typeparam>
        /// <param name="source">Source IEnumerable to be added to the destination list</param>
        /// <param name="destination">Destination List that the source IEnumerable shall be added to</param>
        public static void AddTo<T>(this IEnumerable<T> source, List<T> destination)
        {
            if (source != null) destination.AddRange(source);
        }
    }
}
