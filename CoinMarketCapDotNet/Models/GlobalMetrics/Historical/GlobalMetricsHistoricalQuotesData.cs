using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.GlobalMetrics.Historical
{
    public class GlobalMetricsHistoricalQuotesData
    {
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("search_interval")]
        public DateTime? SearchInterval { get; set; }

        [JsonProperty("btc_dominance")]
        public double BtcDominance { get; set; }

        [JsonProperty("eth_dominance")]
        public double EthDominance { get; set; }

        [JsonProperty("active_cryptocurrencies")]
        public double ActiveCryptocurrencies { get; set; }

        [JsonProperty("active_exchanges")]
        public double ActiveExchanges { get; set; }

        [JsonProperty("active_market_pairs")]
        public double ActiveMarketPairs { get; set; }
        [JsonProperty("quote")]
        public Dictionary<string, GlobalMetricsHistoricalQuoteData> Quote { get; set; }
    }
}
