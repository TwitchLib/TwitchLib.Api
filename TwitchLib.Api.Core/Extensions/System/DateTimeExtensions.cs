using System;
using System.Globalization;

namespace TwitchLib.Api.Core.Extensions.System
{
    /// <summary>
    /// Extension methods for the DateTime type
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts a .NET DateTime type to a RFC3339 string
        /// </summary>
        /// <param name="dateTime">Time as .NET DateTime</param>
        /// <returns>Time as RFC3339 string</returns>
        public static string ToRfc3339String(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz", DateTimeFormatInfo.InvariantInfo);
        }
    }
}