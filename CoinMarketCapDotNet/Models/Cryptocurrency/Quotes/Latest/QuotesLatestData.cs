using CoinMarketCapDotNet.Models.General;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Quotes.Latest
{
    public class QuotesLatestData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("is_active")]
        public int IsActive { get; set; }

        [JsonProperty("is_fiat")]
        public int? IsFiat { get; set; }

        [JsonProperty("cmc_rank")]
        public int CmcRank { get; set; }

        [JsonProperty("num_market_pairs")]
        public int NumMarketPairs { get; set; }

        [JsonProperty("circulating_supply")]
        public double CirculatingSupply { get; set; }

        [JsonProperty("total_supply")]
        public double TotalSupply { get; set; }

        [JsonProperty("market_cap_by_total_supply")]
        public double MarketCapByTotalSupply { get; set; }

        [JsonProperty("max_supply")]
        public double MaxSupply { get; set; }

        [JsonProperty("date_added")]
        public string DateAdded { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("platform")]
        public PlatformData Platform { get; set; }

        [JsonProperty("last_updated")]
        public string LastUpdated { get; set; }

        [JsonProperty("self_reported_circulating_supply")]
        public double? SelfReportedCirculatingSupply { get; set; }

        [JsonProperty("self_reported_market_cap")]
        public double? SelfReportedMarketCap { get; set; }

        [JsonProperty("quote")]
        public Dictionary<string, QuoteData> Quote { get; set; }
    }
}
