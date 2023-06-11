using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Exchange.Quotes.Latest
{
    public class ExchangeLatestData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("num_market_pairs")]
        public int NumMarketPairs { get; set; }

        [JsonProperty("exchange_score")]
        public double ExchangeScore { get; set; }

        [JsonProperty("liquidity_score")]
        public double LiquidityScore { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("traffic_score")]
        public double TrafficScore { get; set; }

        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }
        [JsonProperty("quote")]
        public Dictionary<string, ExchangeQuoteData> Quote { get; set; }
    }
}
