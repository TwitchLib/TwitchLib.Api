using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Charity
{
   /// <summary>
   /// <para></para>
   /// </summary>
   public class Amount
   {
      /// <summary>
      /// <para>The monetary amount specified in the currency’s minor unit.</para>
      /// <para>For example, the minor units for USD is cents, so if the amount is $5.50 USD, value is set to 550.</para>
      /// </summary>
      [JsonProperty(PropertyName = "value")]
      public int? Value { get; protected set; }

      /// <summary>
      /// <para>The number of decimal places used by the currency.<br/>
      /// For example, USD uses two decimal places. </para>
      /// <para>Use this number to translate value from minor units to major units by using the formula: value / 10^decimal_places</para>
      /// </summary>
      [JsonProperty(PropertyName = "decimal_places")]
      public int? DecimalPlaces { get; protected set; }

      /// <summary>
      /// <para>The ISO-4217 three-letter currency code that identifies the type of currency in value.</para>
      /// </summary>
      [JsonProperty(PropertyName = "currency")]
      public string Currency { get; protected set; }
   }
}
