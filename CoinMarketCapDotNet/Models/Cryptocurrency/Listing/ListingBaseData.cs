using CoinMarketCapDotNet.Models.General;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Listing
{
    public class ListingBaseData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }
        [JsonProperty("cmc_rank")]
        public int? CMCRank { get; set; }

        [JsonProperty("num_market_pairs")]
        public int? NumMarketPairs { get; set; }

        [JsonProperty("circulating_supply")]
        public decimal? CirculatingSupply { get; set; }

        [JsonProperty("total_supply")]
        public decimal? TotalSupply { get; set; }

        [JsonProperty("max_supply")]
        public decimal? MaxSupply { get; set; }

        [JsonProperty("infinite_supply")]
        public bool? InfiniteSupply { get; set; }

        [JsonProperty("last_updated")]
        public DateTime? LastUpdated { get; set; }

        [JsonProperty("date_added")]
        public DateTime? DateAdded { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }


        [JsonProperty("platform")]
        public PlatformData Platform { get; set; }

        [JsonProperty("quote")]
        public Dictionary<string, QuoteData> Quote { get; set; }
    }
}
