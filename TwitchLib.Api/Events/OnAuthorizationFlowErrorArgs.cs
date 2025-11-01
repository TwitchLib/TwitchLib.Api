namespace TwitchLib.Api.Events
{
    /// <summary>
    /// On Authorization Flow Error Args
    /// </summary>
    public class OnAuthorizationFlowErrorArgs
    {
        /// <summary>
        /// Error
        /// </summary>
        public int Error { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
    }
}
