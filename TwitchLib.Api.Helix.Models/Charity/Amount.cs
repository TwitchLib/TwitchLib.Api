using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Charity
{
    public class Amount
    {
        /// <summary>
        /// The monetary amount. 
        /// The amount is specified in the currency’s minor unit. 
        /// For example, the minor units for USD is cents, so if the amount is $5.50 USD, value is set to 550.
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public int? Value { get; protected set; }

        /// <summary>
        /// The number of decimal places used by the currency. 
        /// For example, USD uses two decimal places. 
        /// Use this number to translate value from minor units to major units by using the formula: value / 10^decimal_places
        /// </summary>
        [JsonProperty(PropertyName = "decimal_places")]
        public int? DecimalPlaces { get; protected set; }

        /// <summary>
        /// The ISO-4217 three-letter currency code that identifies the type of currency in value.
        /// </summary>
        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; protected set; }
    }
}
