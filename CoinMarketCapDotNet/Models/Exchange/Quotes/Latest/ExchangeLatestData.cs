using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Exchange.Quotes.Latest
{
    public class ExchangeLatestData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("num_market_pairs")]
        public int? NumMarketPairs { get; set; }

        [JsonPropertyName("exchange_score")]
        public double? ExchangeScore { get; set; }

        [JsonPropertyName("liquidity_score")]
        public double? LiquidityScore { get; set; }

        [JsonPropertyName("rank")]
        public int? Rank { get; set; }

        [JsonPropertyName("traffic_score")]
        public double? TrafficScore { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTime? LastUpdated { get; set; }
        [JsonPropertyName("quote")]
        public Dictionary<string, ExchangeQuoteData> Quote { get; set; }
    }
}
