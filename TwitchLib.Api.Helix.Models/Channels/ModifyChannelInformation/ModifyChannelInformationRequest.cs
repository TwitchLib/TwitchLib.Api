using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.ModifyChannelInformation
{
   /// <summary>
   /// <para>A request to update a channel's properties</para>
   /// <para>All fields are optional but you must specify at least one field.</para>
   /// </summary>
   [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
   public class ModifyChannelInformationRequest
   {
      /// <summary>
      /// <para>The ID of the game that the broadcaster will be playing.</para>
      /// <para>The game will not updated if the ID is not a game ID that Twitch recognizes.</para>
      /// <para>To unset this field, use “0” or “” (an empty string).</para>
      /// </summary>
      [JsonProperty(PropertyName = "game_id", NullValueHandling = NullValueHandling.Ignore)]
      public string GameId { get; set; }

      /// <summary>
      /// <para>The title of the broadcaster's stream.</para>
      /// <para>You may not set this field to an empty string.</para>
      /// </summary>
      [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
      public string Title { get; set; }

      /// <summary>
      /// <para>The broadcaster’s preferred language.</para>
      /// <para>Set the value to an ISO 639-1 two-letter language code - for example, en for English.</para>
      /// <para>Set the value to “other” if the broadcaster’s preferred language is not a Twitch supported language.</para>
      /// <para>The language will not updated if the language code is not a Twitch supported language.</para>
      /// </summary>
      [JsonProperty(PropertyName = "broadcaster_language", NullValueHandling = NullValueHandling.Ignore)]
      public string BroadcasterLanguage { get; set; }

      /// <summary>
      /// <para>The number of seconds you want your broadcast buffered before streaming it live. The delay helps ensure fairness during competitive play.</para>
      /// <para>The maximum delay is 900 seconds (15 minutes).</para>
      /// <para><b>Only users with Partner status may set this field.</b></para>
      /// </summary>
      [JsonProperty(PropertyName = "delay", NullValueHandling = NullValueHandling.Ignore)]
      public int? Delay { get; set; }

      /// <summary>
      /// <para>A list of channel-defined tags to apply to the channel. Tags help identify the content that the channel streams.</para>
      /// <para>A channel may specify a maximum of 10 tags. Each tag is limited to a maximum of 25 characters and may not be an empty string or contain spaces or special characters. Tags are case insensitive. For readability, consider using camelCasing or PascalCasing.</para>
      /// <para>To remove all tags from the channel, set tags to an empty array.</para>
      /// </summary>
      [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)]
      public string[] Tags { get; set; }
   }
}
