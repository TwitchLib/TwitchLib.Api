<p align="center"> 
<img src="https://cdn.syzuna-programs.de/images/twitchlib.png" style="max-height: 300px;">
</p>

## Helix API
The Twitch Helix API is the main API layer for Twitch services. The Helix API has two levels of security gated by two distinct types of OAuth tokens. The first type is a App Access Token. This token type is directly granted by Twitch's server to your application and does not require a user to be logged in to grant scope level access. Several Twitch API methods can be accessed by this type of token *but not all of them*. Several APIs can only be used with a User Access Token. This token type is scoped to access specific APIs and you must be logged into a Twitch account to allow this token to acceed these scopes. TwitchLib.API now supports direct authentication of User Access tokens to make this easy for you. 

Please refer to the Twitch API to learn more about the two different token types and when to use them. 
https://dev.twitch.tv/docs/authentication/


You can decide which type of token you will need by determining which APIs you would like to use from the Helix API set. A list of all of the APIs can be found here:
https://dev.twitch.tv/docs/api/reference/

For example, the <a href="https://dev.twitch.tv/docs/api/reference/#get-channel-information" target="_top">Get Channel Information</a> API will work with either an App Acccess Token or a User Access Token because it works with information that is publically accessable without being logged in. Some Channel API methods however, like <a href="https://dev.twitch.tv/docs/api/reference/#start-commercial" target="_top">Start Commercial</a> will only allow the a User Accces Token with the channel:edit:commercial scope assigned to it. You can see what a Helix API method will require by looking at the Authorization section of any API method in the <a href="https://dev.twitch.tv/docs/api/reference/" target="_top">reference</a>.

One final word about scopes. Do not assign ALL of the available scopes to AppSettings.Scopes. Doing so may get your application banned. <a href="https://dev.twitch.tv/docs/authentication/scopes/">See the warning here</a>!

# How it works
To support User Access Tokens, TwitchLib.API has a small webserver integrated into it. When you authenticate your Client ID and Secret to the Twitch API to get a User Access Token, it will open a browser window that will require you to login with your Twitch account and then grant explicit permissions to your application to do specific actions with the Twitch API. Once you do, Twitch API will forward your browser back to this webserver which hands the credential securly back to TwitchLib.API. These credentials will then be used by all future calls to the Helix API. OAuth complexities like periodic validation and refresh tokens are handled for you by the library. You can also implement this yourself if you need more functionliaty than this built in solution provides. See the section on Settings for more information.  

> ***If you are using User Access Tokens, be sure to set AppSettings.UseUserTokenForHelixCalls to True!***

For App Access Tokens, simply provide your ClientId and Secret. There is no interactive authorization stage for this type of token so this can work better for a server application, or an application that will run as a daemon, service or scheduled task when interacting with a webbrowser is not viable. App Access Tokens are much more limited and much of the Helix API does not work with them, but some public data methods do. Review the Helix API reference to see what type of token you will need.

# Settings
Authentication is controlled by several settings in the AppSettings class that you pass to TwitchLib.API when your application starts up. 

| Setting Name | Description |
| --- | --- |
| AccessToken | The current access token that is being used to access the Helix API. You can specify this yourself to directly control the Access Token in use, or you can let the TwitchLib.API library control it for you. |
| Secret | This comes from the Twitch Developer console after you set up your application. Used for both App Access and User Access Tokens |
| ClientId | This comes from the Twitch Developer console after you set up your application. Used for both App Access  and User Access Tokens |
| SkipAutoServerTokenGeneration | If AccessToken is null, and this is set to true, then Helix API calls will not attempt to use the ClientID/Secret to generate a client_credential access token. Set this to true if you intend to implement your own token management. Defaults to False.|
| Scopes | Add scopes that your application will be using to this collection before calling any Helix APIs. A list of scopes can be found <a href="https://dev.twitch.tv/docs/authentication/scopes/">here</a>. See the TwitchAPI reference for the scopes specific to each API. Note: Do not add ALL the scopes, or your account may be banned! <a href="https://dev.twitch.tv/docs/authentication/scopes/">See warning here</a>.) |
| OAuthResponsePort | Set this value to another port if you have another application already listening to port 5000 on your machine. Defaults to: 5000 |
| OAuthResponseHostname | Set this value to a hostname or IP address if you have a multi-homed machine (more than one IP address) and you would like to bind the OAuth response listener to a specific IP address. Defaults to 'localhost' |
| OAuthTokenFile | Storage for oAuth refresh token, expiration dates, etc. Defaults to %AppData%\\TwitchLib.API\[ApplicationName].json Set this if you will be running multiple instances of the same application that you would like to use with different user tokens. |
| UseUserTokenForHelixCalls | Set this value to true to enable Helix calls that require an oAuth User Token. This requires you to also set ApiSettings.ClientID and ApiSettings.Secret. Defaults to False |
| EnableInsecureTokenStorage | TwitchLib.API keeps OAuthTokenFile in a relativly secure location, but anyone with admin access to your computer could get access to this file and then access to Twitch API using the scopes you have granted to this token. While unlikely, we do want to call out that using this file is insecure. Enabling this setting will enable your app to run without re-authenticating between runs. Defaults to False. |


# Prerequisites
Before you get started with the Helix API, you must first register your application with the Twitch Developer console. Follow the Twitch API guide <a href="https://dev.twitch.tv/docs/authentication/register-app/">here</a>. Come back once you have your Application's ClientId and Secret. 

# Example 1 - App Access Token
```csharp
    var settings = new ApiSettings
    {
        ClientId = CLIENT_ID,
        Secret = SECRET
    };

    // While we are setting the correct scopes, they will not be used becuase we are not
    // using a User Access Token. 
    settings.Scopes.Add(Core.Enums.AuthScopes.Helix_Moderation_Read);
    settings.Scopes.Add(Core.Enums.AuthScopes.Helix_Moderator_Manage_Banned_Users);

    using ILoggerFactory loggerFactory =
        LoggerFactory.Create(builder =>
            builder.AddSimpleConsole(options =>
            {
                options.IncludeScopes = true;
                options.SingleLine = true;
                options.TimestampFormat = "HH:mm:ss ";
            }));

    var twitchAPI = new TwitchAPI(loggerFactory: loggerFactory, settings: settings);

    var channelInformation = twitchAPI.Helix.Channels.GetChannelInformationAsync(BROADCASTER_ID);

    Console.WriteLine($"Channel Information Returned: {channelInformation.Result.Data.Length}");

    try
    {
        // This will fail because an App Access Token cannot be used with the Helix Moderation API.
        var bannedUsers = twitchAPI.Helix.Moderation.GetBannedUsersAsync(BROADCASTER_ID);

        Console.WriteLine($"Banned user count: {bannedUsers.Result.Data.Length}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n\n\nCan't use an App Access Token with the Helix Moderation API: {ex.Message}\n\n\n");
    }```

# Example 2 - User Access Token
```csharp
var settings = new ApiSettings
{
    ClientId = CLIENT_ID,
    Secret = SECRET,
    EnableInsecureTokenStorage = true, // Allows the application to start silently after the first authorization. 
    UseUserTokenForHelixCalls = true // Enables User Access Tokens to be used for Helix API calls.
};

// Now that we are using User Access Tokens, the Helix Scopes can also be used.
settings.Scopes.Add(Core.Enums.AuthScopes.Helix_Moderation_Read);
settings.Scopes.Add(Core.Enums.AuthScopes.Helix_Moderator_Manage_Banned_Users);

using ILoggerFactory loggerFactory =
    LoggerFactory.Create(builder =>
        builder.AddSimpleConsole(options =>
        {
            options.IncludeScopes = true;
            options.SingleLine = true;
            options.TimestampFormat = "HH:mm:ss ";
        }));

var twitchAPI = new TwitchAPI(loggerFactory: loggerFactory, settings: settings);

var channelInformation = twitchAPI.Helix.Channels.GetChannelInformationAsync(BROADCASTER_ID);

Console.WriteLine($"Channel Information Returned: {channelInformation.Result.Data.Length}");

// This works fine now because we have a User Access Token with the correct scopes.
var bannedUsers = twitchAPI.Helix.Moderation.GetBannedUsersAsync(BROADCASTER_ID);

Console.WriteLine($"Banned user count: {bannedUsers.Result.Data.Length}");
```