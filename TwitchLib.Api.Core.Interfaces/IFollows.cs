namespace TwitchLib.Api.Core.Interfaces
{
    /// <summary>
    /// Interface for follows
    /// </summary>
    public interface IFollows
    {
        /// <summary>
        /// Total follows
        /// </summary>
        int Total { get; }

        /// <summary>
        /// Cursor
        /// </summary>
        string Cursor { get; }

        /// <summary>
        /// Follows interface
        /// </summary>
        IFollow[] Follows { get; }
    }
}
