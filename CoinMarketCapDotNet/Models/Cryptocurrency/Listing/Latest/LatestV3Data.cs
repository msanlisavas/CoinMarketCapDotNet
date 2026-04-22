using CoinMarketCapDotNet.Models.General;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Listing.Latest
{
    /// <summary>
    /// Response row for <c>/v3/cryptocurrency/listings/latest</c>. V3 returns <c>quote</c> as an array
    /// where each element carries its own <c>id</c> and <c>symbol</c>, unlike V1/V2 which key quotes
    /// by currency symbol. This type is separate from <see cref="LatestData"/> (used by V1/V2) because
    /// the <c>Quote</c> property shape differs.
    /// </summary>
    public class LatestV3Data
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        [JsonPropertyName("cmc_rank")]
        public int? CmcRank { get; set; }

        [JsonPropertyName("num_market_pairs")]
        public int? NumMarketPairs { get; set; }

        [JsonPropertyName("circulating_supply")]
        public decimal? CirculatingSupply { get; set; }

        [JsonPropertyName("total_supply")]
        public decimal? TotalSupply { get; set; }

        [JsonPropertyName("max_supply")]
        public decimal? MaxSupply { get; set; }

        [JsonPropertyName("infinite_supply")]
        public bool? InfiniteSupply { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTime? LastUpdated { get; set; }

        [JsonPropertyName("date_added")]
        public DateTime? DateAdded { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new List<string>();

        [JsonPropertyName("platform")]
        public PlatformData? Platform { get; set; }

        [JsonPropertyName("self_reported_circulating_supply")]
        public decimal? SelfReportedCirculatingSupply { get; set; }

        [JsonPropertyName("self_reported_market_cap")]
        public decimal? SelfReportedMarketCap { get; set; }

        [JsonPropertyName("minted_market_cap")]
        public decimal? MintedMarketCap { get; set; }

        [JsonPropertyName("tvl_ratio")]
        public double? TvlRatio { get; set; }

        [JsonPropertyName("quote")]
        public List<QuoteData>? Quote { get; set; }
    }
}
