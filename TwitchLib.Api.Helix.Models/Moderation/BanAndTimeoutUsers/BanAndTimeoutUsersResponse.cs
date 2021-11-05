using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Moderation.BanUsers
{
    public class BanAndTimeoutUsersResponse
    {
        [JsonProperty(PropertyName = "data")]
        public BannedUser[] Data { get; protected set; }
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors { get; protected set; }

        /// <summary>
        /// Returns a list of user ids parsed from the Error messages (ie '2332123: user is already banned')
        /// </summary>
        /// <returns></returns>
        public List<string> ExtractUserIdsFromErrors()
        {
            var result = new List<string>();
            if (Errors == null || Errors.Length == 0)
                return result;

            foreach(var error in Errors)
                result.Add(error.Split(':')[0]);

            return result;
        }
    }
}
