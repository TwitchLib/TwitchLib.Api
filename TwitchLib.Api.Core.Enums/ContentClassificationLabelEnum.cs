namespace TwitchLib.Api.Core.Enums
{
   /// <summary>
   /// <para>Enum representing the Content Classification Labels.</para>
   /// </summary>
   public enum ContentClassificationLabelEnum
   {
      /// <summary>
      /// <para>Content Classification Label for broadcasts with excessive tobacco glorification or promotion, 
      /// any marijuana consumption/use, legal drug and alcohol induced intoxication, discussions of illegal drugs.</para>
      /// </summary>
      DrugsIntoxication,

      /// <summary>
      /// <para>Content Classification Label for broadcasts with content that focuses on 
      /// sexualized physical attributes and activities, sexual topics, or experiences.</para>
      /// </summary>
      SexualThemes,

      /// <summary>
      /// <para>Content Classification Label for broadcasts with simulations and/or depictions 
      /// of realistic violence, gore, extreme injury, or death.</para>
      /// </summary>
      ViolentGraphic,

      /// <summary>
      /// <para>Content Classification Label for broadcasts that participate in online or 
      /// in-person gambling, poker or fantasy sports, that involve the exchange of real money.</para>
      /// </summary>
      Gambling,

      /// <summary>
      /// <para>Content Classification Label for broadcasts with prolonged, and repeated use 
      /// of obscenities, profanities, and vulgarities, especially as a regular part of speech.</para>
      /// </summary>
      ProfanityVulgarity
   }
}
