using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.GlobalMetrics.Latest
{
    public class GlobalMetricsLatestData
    {
        [JsonPropertyName("btc_dominance")]
        public double? BitcoinDominance { get; set; }

        [JsonPropertyName("eth_dominance")]
        public double? EthereumDominance { get; set; }

        [JsonPropertyName("active_cryptocurrencies")]
        public int? ActiveCryptocurrencies { get; set; }

        [JsonPropertyName("total_cryptocurrencies")]
        public int? TotalCryptocurrencies { get; set; }

        [JsonPropertyName("active_market_pairs")]
        public int? ActiveMarketPairs { get; set; }

        [JsonPropertyName("active_exchanges")]
        public int? ActiveExchanges { get; set; }

        [JsonPropertyName("total_exchanges")]
        public int? TotalExchanges { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTime? LastUpdated { get; set; }
        [JsonPropertyName("quote")]
        public Dictionary<string, GlobalMetricsLatestQuoteData>? Quote { get; set; }
    }
}
