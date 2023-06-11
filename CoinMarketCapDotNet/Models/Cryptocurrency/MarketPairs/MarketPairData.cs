using CoinMarketCapDotNet.Models.General;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.MarketPairs
{
    public class MarketPairData
    {
        [JsonProperty("market_id")]
        public int MarketId { get; set; }

        [JsonProperty("market_pair")]
        public string MarketPairName { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("fee_type")]
        public string FeeType { get; set; }

        [JsonProperty("market_url")]
        public string MarketUrl { get; set; }

        [JsonProperty("exchange")]
        public ExchangeData Exchange { get; set; }

        [JsonProperty("market_pair_base")]
        public MarketPairBaseData MarketPairBase { get; set; }

        [JsonProperty("market_pair_quote")]
        public MarketPairQuoteData MarketPairQuote { get; set; }

        [JsonProperty("quote")]
        public Dictionary<string, QuoteData> Quote { get; set; }
    }

}
