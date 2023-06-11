using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.MarketPairs
{
    public class MarketPairQuoteData
    {
        [JsonProperty("currency_id")]
        public int CurrencyId { get; set; }

        [JsonProperty("currency_name")]
        public string CurrencyName { get; set; }

        [JsonProperty("currency_symbol")]
        public string CurrencySymbol { get; set; }

        [JsonProperty("currency_slug")]
        public string CurrencySlug { get; set; }

        [JsonProperty("exchange_symbol")]
        public string ExchangeSymbol { get; set; }

        [JsonProperty("currency_type")]
        public string CurrencyType { get; set; }
    }
}
