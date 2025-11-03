using System;

namespace TwitchLib.Api.Core.Enums;

/// <summary>
/// Auth Scopes
/// </summary>
public enum AuthScopes
{
    /// <summary>
    /// Any scope.
    /// </summary>
    Any,

    /// <summary>
    /// View live stream chat messages.
    /// </summary>
    Chat_Read,

    /// <summary>
    /// Send live stream chat messages.
    /// </summary>
    Chat_Edit,

    /// <summary>
    /// Perform moderation actions in a channel. The user requesting the scope must be a moderator in the channel.
    /// </summary>
    Channel_Moderate,

    /// <summary>
    /// View your whisper messages.
    /// </summary>
    Whisper_Read,

    /// <summary>
    /// Send whisper messages.
    /// </summary>
    Whisper_Edit,

    /// <summary>
    /// Manage Clips as an editor.
    /// </summary>
    Editor_Manage_Clips,

    /// <summary>
    /// View analytics data for the Twitch Extensions owned by the authenticated account.
    /// </summary>
    Analytics_Read_Extensions,

    /// <summary>
    /// View analytics data for the games owned by the authenticated account.
    /// </summary>
    Analytics_Read_Games,

    /// <summary>
    /// View Bits information for a channel.
    /// </summary>
    Bits_Read,

    /// <summary>
    /// Allows the client’s bot users access to a channel.
    /// </summary>
    Channel_Bot,

    /// <summary>
    /// Run commercials on a channel.
    /// </summary>
    Channel_Edit_Commercial,

    /// <summary>
    /// Manage ads schedule on a channel.
    /// </summary>
    Channel_Manage_Ads,

    /// <summary>
    /// Manage a channel’s broadcast configuration, including updating channel configuration and managing stream markers and stream tags.
    /// </summary>
    Channel_Manage_Broadcast,

    /// <summary>
    /// Manage Clips for a channel.
    /// </summary>
    Channel_Manage_Clips,

    /// <summary>
    /// Manage a channel’s Extension configuration, including activating Extensions.
    /// </summary>
    Channel_Manage_Extensions,

    /// <summary>
    /// Add or remove the moderator role from users in your channel.
    /// </summary>
    Channel_Manage_Moderators,

    /// <summary>
    /// Manage a channel’s polls.
    /// </summary>
    Channel_Manage_Polls,

    /// <summary>
    /// Manage of channel’s Channel Points Predictions
    /// </summary>
    Channel_Manage_Predictions,

    /// <summary>
    /// Manage Channel Points custom rewards and their redemptions on a channel.
    /// </summary>
    Channel_Manage_Redemptions,

    /// <summary>
    /// Manage a channel’s stream schedule.
    /// </summary>
    Channel_Manage_Schedule,

    /// <summary>
    /// Manage Guest Star for your channel.
    /// </summary>
    Channel_Manage_Guest_Star,

    /// <summary>
    /// Manage a channel raiding another channel.
    /// </summary>
    Channel_Manage_Raids,

    /// <summary>
    /// Manage a channel’s videos, including deleting videos.
    /// </summary>
    Channel_Manage_Videos,

    /// <summary>
    /// Add or remove the VIP role from users in your channel.
    /// </summary>
    Channel_Manage_VIPs,

    /// <summary>
    /// Read the ads schedule and details on your channel.
    /// </summary>
    Channel_Read_Ads,

    /// <summary>
    /// Read charity campaign details and user donations on your channel.
    /// </summary>
    Channel_Read_Charity,

    /// <summary>
    /// View a list of users with the editor role for a channel.
    /// </summary>
    Channel_Read_Editors,

    /// <summary>
    /// View Creator Goals for a channel.
    /// </summary>
    Channel_Read_Goals,

    /// <summary>
    /// View Hype Train information for a channel.
    /// </summary>
    Channel_Read_Hype_Train,

    /// <summary>
    /// View a channel’s polls.
    /// </summary>
    Channel_Read_Polls,

    /// <summary>
    /// View a channel’s Channel Points Predictions.
    /// </summary>
    Channel_Read_Predictions,

    /// <summary>
    /// View Channel Points custom rewards and their redemptions on a channel.
    /// </summary>
    Channel_Read_Redemptions,

    /// <summary>
    /// Read Guest Star details for your channel.
    /// </summary>
    Channel_Read_Guest_Star,

    /// <summary>
    /// View an authorized user’s stream key.
    /// </summary>
    Channel_Read_Stream_Key,

    /// <summary>
    /// View a list of all subscribers to a channel and check if a user is subscribed to a channel.
    /// </summary>
    Channel_Read_Subscriptions,

    /// <summary>
    /// Read the list of VIPs in your channel.
    /// </summary>
    Channel_Read_VIPs,

    /// <summary>
    /// Manage Clips for a channel.
    /// </summary>
    Clips_Edit,

    /// <summary>
    /// View a channel’s moderation data including Moderators, Bans, Timeouts, and Automod settings.
    /// </summary>
    Moderation_Read,

    /// <summary>
    /// Send announcements in channels where you have the moderator role.
    /// </summary>
    Moderator_Manage_Announcements,

    /// <summary>
    /// Manage messages held for review by AutoMod in channels where you are a moderator.
    /// </summary>
    Moderator_Manage_Automod,

    /// <summary>
    /// Manage a broadcaster’s AutoMod settings.
    /// </summary>
    Moderator_Manage_Automod_Settings,

    /// <summary>
    /// Ban and unban users.
    /// </summary>
    Moderator_Manage_Banned_Users,

    /// <summary>
    /// Manage a broadcaster’s list of blocked terms.
    /// </summary>
    Moderator_Manage_Blocked_Terms,

    /// <summary>
    /// Delete chat messages in channels where you have the moderator role
    /// </summary>
    Moderator_Manage_Chat_Messages,

    /// <summary>
    /// Manage a broadcaster’s chat room settings.
    /// </summary>
    Moderator_Manage_Chat_Settings,

    /// <summary>
    /// Manage Guest Star for channels where you are a Guest Star moderator.
    /// </summary>
    Moderator_Manage_Guest_Star,

    /// <summary>
    /// Manage a broadcaster’s Shield Mode status.
    /// </summary>
    Moderator_Manage_Shield_Mode,

    /// <summary>
    /// Manage a broadcaster’s shoutouts.
    /// </summary>
    Moderator_Manage_Shoutouts,

    /// <summary>
    /// Manage a broadcaster’s unban requests.
    /// </summary>
    Moderator_Manage_Unban_Requests,

    /// <summary>
    /// Warn users in channels where you have the moderator role.
    /// </summary>
    Moderator_Manage_Warnings,

    /// <summary>
    /// View a broadcaster’s AutoMod settings.
    /// </summary>
    Moderator_Read_Automod_Settings,

    /// <summary>
    /// Read the list of bans or unbans in channels where you have the moderator role.
    /// </summary>
    Moderator_Read_Banned_Users,

    /// <summary>
    /// View a broadcaster’s list of blocked terms.
    /// </summary>
    Moderator_Read_Blocked_Terms,

    /// <summary>
    /// Read deleted chat messages in channels where you have the moderator role.
    /// </summary>
    Moderator_Read_Chat_Messages,

    /// <summary>
    /// Read the list of moderators in channels where you have the moderator role.
    /// </summary>
    Moderator_Read_Moderators,

    /// <summary>
    /// View a broadcaster’s chat room settings.
    /// </summary>
    Moderator_Read_Chat_Settings,

    /// <summary>
    /// View the chatters in a broadcaster’s chat room.
    /// </summary>
    Moderator_Read_Chatters,

    /// <summary>
    /// Read the followers of a broadcaster.
    /// </summary>
    Moderator_Read_Followers,

    /// <summary>
    /// Read Guest Star details for channels where you are a Guest Star moderator.
    /// </summary>
    Moderator_Read_Guest_Star,

    /// <summary>
    /// View a broadcaster’s Shield Mode status.
    /// </summary>
    Moderator_Read_Shield_Mode,

    /// <summary>
    /// View a broadcaster’s shoutouts.
    /// </summary>
    Moderator_Read_Shoutouts,

    /// <summary>
    /// Read chat messages from suspicious users and see users flagged as suspicious in channels where you have the moderator role.
    /// </summary>
    Moderator_Read_Suspicious_Users,

    /// <summary>
    /// View a broadcaster’s unban requests.
    /// </summary>
    Moderator_Read_Unban_Requests,

    /// <summary>
    /// Read the list of VIPs in channels where you have the moderator role.
    /// </summary>
    Moderator_Read_VIPs,

    /// <summary>
    /// Read warnings in channels where you have the moderator role.
    /// </summary>
    Moderator_Read_Warnings,

    /// <summary>
    /// Allows client’s bot to act as this user.
    /// </summary>
    User_Bot,

    /// <summary>
    /// Manage a user object.
    /// </summary>
    User_Edit,

    /// <summary>
    /// View and edit a user’s broadcasting configuration, including Extension configurations.
    /// </summary>
    User_Edit_Broadcast,

    /// <summary>
    /// Deprecated. Was previously used for “Create User Follows” and “Delete User Follows.”
    /// </summary>
    [Obsolete("Deprecated")]
    User_Edit_Follows,

    /// <summary>
    /// Manage the block list of a user.
    /// </summary>
    User_Manage_BlockedUsers,

    /// <summary>
    /// Update the color used for the user’s name in chat.
    /// </summary>
    User_Manage_Chat_Color,

    /// <summary>
    /// Read whispers that you send and receive, and send whispers on your behalf.
    /// </summary>
    User_Manage_Whispers,

    /// <summary>
    /// View the block list of a user.
    /// </summary>
    User_Read_BlockedUsers,

    /// <summary>
    /// View a user’s broadcasting configuration, including Extension configurations.
    /// </summary>
    User_Read_Broadcast,

    /// <summary>
    /// View live stream chat and room messages.
    /// </summary>
    User_Read_Chat,

    /// <summary>
    /// View a user’s email address.
    /// </summary>
    User_Read_Email,

    /// <summary>
    /// View emotes available to a user
    /// </summary>
    User_Read_Emotes,

    /// <summary>
    /// View the list of channels a user follows.
    /// </summary>
    User_Read_Follows,

    /// <summary>
    /// View the list of channels a user moderates.
    /// </summary>
    User_Read_Moderated_Channels,

    /// <summary>
    /// View if an authorized user is subscribed to specific channels.
    /// </summary>
    User_Read_Subscriptions,

    /// <summary>
    /// Receive whispers sent to your user.
    /// </summary>
    User_Read_Whispers,

    /// <summary>
    /// User write chat
    /// </summary>
    User_Write_Chat,

    /// <summary>
    /// No scope
    /// </summary>
    None
}
