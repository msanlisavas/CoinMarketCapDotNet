using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Exchange.Listing.Latest
{
    public class ExchangeListingLatestData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("slug")]
        public string? Slug { get; set; }
        [JsonPropertyName("num_market_pairs")]
        public int NumMarketPairs { get; set; }
        [JsonPropertyName("date_launched")]
        public DateTime? DateLaunched { get; set; }
        [JsonPropertyName("exchange_score")]
        public decimal? ExchangeScore { get; set; }
        [JsonPropertyName("liquidity_score")]
        public decimal? LiquidityScore { get; set; }
        [JsonPropertyName("rank")]
        public decimal? Rank { get; set; }
        [JsonPropertyName("traffic_score")]
        public decimal? TrafficScore { get; set; }
        [JsonPropertyName("last_updated")]
        public DateTime? LastUpdated { get; set; }
        [JsonPropertyName("quote")]
        public Dictionary<string, ExchangeQuoteData>? Quote { get; set; }


    }
}
