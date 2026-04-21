using CoinMarketCapDotNet.Models.General;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.MarketPairs
{
    public class MarketPairData
    {
        [JsonPropertyName("market_id")]
        public int MarketId { get; set; }

        [JsonPropertyName("market_pair")]
        public string? MarketPairName { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("fee_type")]
        public string? FeeType { get; set; }

        [JsonPropertyName("market_url")]
        public string? MarketUrl { get; set; }

        [JsonPropertyName("exchange")]
        public ExchangeData? Exchange { get; set; }

        [JsonPropertyName("market_pair_base")]
        public MarketPairBaseData? MarketPairBase { get; set; }

        [JsonPropertyName("market_pair_quote")]
        public MarketPairQuoteData? MarketPairQuote { get; set; }

        [JsonPropertyName("quote")]
        public Dictionary<string, QuoteData>? Quote { get; set; }
    }

}
