﻿using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.GuestStar.GetGuestStarInvites;

public class GetGuestStarInvitesResponse
{
    /// <summary>
    /// <para>A list of invite objects describing the invited user as well as their ready status.</para>
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public GuestStarInvite[] Data { get; protected set; }
}