using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.GlobalMetrics.Latest
{
    public class GlobalMetricsLatestData
    {
        [JsonProperty("btc_dominance")]
        public double BitcoinDominance { get; set; }

        [JsonProperty("eth_dominance")]
        public double EthereumDominance { get; set; }

        [JsonProperty("active_cryptocurrencies")]
        public int ActiveCryptocurrencies { get; set; }

        [JsonProperty("total_cryptocurrencies")]
        public int TotalCryptocurrencies { get; set; }

        [JsonProperty("active_market_pairs")]
        public int ActiveMarketPairs { get; set; }

        [JsonProperty("active_exchanges")]
        public int ActiveExchanges { get; set; }

        [JsonProperty("total_exchanges")]
        public int TotalExchanges { get; set; }

        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }
        [JsonProperty("quote")]
        public Dictionary<string, GlobalMetricsLatestQuoteData> Quote { get; set; }
    }
}
