using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Helix
{
    public class Whispers : ApiBase
    {
        public Whispers(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        { }

        #region SendWhisper

        /// <summary>
        ///
        /// </summary>
        /// <param name="FromUserId">The ID of the user sending the whisper. This user must have a verified phone number.</param>
        /// <param name="ToUserId">The ID of the user to receive the whisper.</param>
        /// <param name="message"> 	The whisper message to send. 500 characters for NewRecipient, otherwise 10,000 characters</param>
        /// <param name="NewRecipient">If this is a new recipient to adjust max characters allowed in the message.</param>
        public Task SendWhisperAsync(string FromUserId, string ToUserId, string message, bool NewRecipient, string accessToken = null)
        {
            if (string.IsNullOrEmpty(FromUserId))
                throw new BadParameterException("FromUserId must be set");
            if (string.IsNullOrEmpty(ToUserId))
                throw new BadParameterException("ToUserId must be set");
            if (message == null)
                throw new BadParameterException("message must be set");
            int msgLength = 500;
            if (!NewRecipient) msgLength = 10000;
            if (message.Length > msgLength)
                throw new BadParameterException($"message length must be less than or equal to {msgLength} characters");

            var getParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("from_user_id", FromUserId),
                new KeyValuePair<string, string>("to_user_id", ToUserId),
            };

            // This should be updated to have a Request Class in the future.
            JObject json = new JObject();
            json["message"] = message;

            return TwitchPostAsync("/chat/announcements", ApiVersion.Helix, json.ToString(), getParams, accessToken);
        }

        #endregion
    }
}