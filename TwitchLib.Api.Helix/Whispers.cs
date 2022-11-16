using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Whispers related APIs
    /// </summary>
    public class Whispers : ApiBase
    {
        public Whispers(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        { }

        #region SendWhisper

        /// <summary>
        /// Sends a whisper message to the specified user.
        /// <para>The ID in the fromUserId parameter must match the user ID in the access token.</para>
        /// <para>Requires an user access token that includes the user:manage:whispers scope.</para>
        /// </summary>
        /// <param name="fromUserId">The ID of the user sending the whisper. This user must have a verified phone number.</param>
        /// <param name="toUserId">The ID of the user to receive the whisper.</param>
        /// <param name="message">The whisper message to send. 500 characters for new recipient, otherwise 10,000 characters</param>
        /// <param name="newRecipient">If this is a new recipient to adjust max characters allowed in the message.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        public Task SendWhisperAsync(string fromUserId, string toUserId, string message, bool newRecipient, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(fromUserId))
                throw new BadParameterException("FromUserId must be set");

            if (string.IsNullOrWhiteSpace(toUserId))
                throw new BadParameterException("ToUserId must be set");

            if (message == null)
                throw new BadParameterException("message must be set");

            var msgLength = 500;
            if (!newRecipient) msgLength = 10000;
            if (message.Length > msgLength)
                throw new BadParameterException($"message length must be less than or equal to {msgLength} characters");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("from_user_id", fromUserId),
                new KeyValuePair<string, string>("to_user_id", toUserId),
            };

            // This should be updated to have a Request Class in the future.
            var json = new JObject
            {
                ["message"] = message
            };

            return TwitchPostAsync("/whispers", ApiVersion.Helix, json.ToString(), getParams, accessToken);
        }

        #endregion
    }
}
