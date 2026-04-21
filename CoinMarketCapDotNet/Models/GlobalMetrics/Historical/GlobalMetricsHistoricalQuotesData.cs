using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.GlobalMetrics.Historical
{
    public class GlobalMetricsHistoricalQuotesData
    {
        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }

        [JsonPropertyName("search_interval")]
        public DateTime? SearchInterval { get; set; }

        [JsonPropertyName("btc_dominance")]
        public double? BtcDominance { get; set; }

        [JsonPropertyName("eth_dominance")]
        public double? EthDominance { get; set; }

        [JsonPropertyName("active_cryptocurrencies")]
        public double? ActiveCryptocurrencies { get; set; }

        [JsonPropertyName("active_exchanges")]
        public double? ActiveExchanges { get; set; }

        [JsonPropertyName("active_market_pairs")]
        public double? ActiveMarketPairs { get; set; }
        [JsonPropertyName("quote")]
        public Dictionary<string, GlobalMetricsHistoricalQuoteData> Quote { get; set; }
    }
}
