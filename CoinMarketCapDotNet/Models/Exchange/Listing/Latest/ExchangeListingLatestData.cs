using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Exchange.Listing.Latest
{
    public class ExchangeListingLatestData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("slug")]
        public string Slug { get; set; }
        [JsonProperty("num_market_pairs")]
        public int NumMarketPairs { get; set; }
        [JsonProperty("date_launched")]
        public DateTime DateLaunched { get; set; }
        [JsonProperty("exchange_score")]
        public decimal ExchangeScore { get; set; }
        [JsonProperty("liquidity_score")]
        public decimal LiquidityScore { get; set; }
        [JsonProperty("rank")]
        public decimal Rank { get; set; }
        [JsonProperty("traffic_score")]
        public decimal TrafficScore { get; set; }
        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }
        [JsonProperty("quote")]
        public Dictionary<string, ExchangeQuoteData> Quote { get; set; }


    }
}
