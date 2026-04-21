using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.MarketPairs
{
    public class MarketPairBaseData
    {
        [JsonPropertyName("currency_id")]
        public int CurrencyId { get; set; }

        [JsonPropertyName("currency_name")]
        public string CurrencyName { get; set; }

        [JsonPropertyName("currency_symbol")]
        public string CurrencySymbol { get; set; }

        [JsonPropertyName("currency_slug")]
        public string CurrencySlug { get; set; }

        [JsonPropertyName("exchange_symbol")]
        public string ExchangeSymbol { get; set; }

        [JsonPropertyName("currency_type")]
        public string CurrencyType { get; set; }
    }

}
