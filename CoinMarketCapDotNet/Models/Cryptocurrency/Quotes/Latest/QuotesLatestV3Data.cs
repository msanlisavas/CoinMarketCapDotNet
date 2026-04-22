using CoinMarketCapDotNet.Models.Cryptocurrency.Quotes;
using CoinMarketCapDotNet.Models.General;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Quotes.Latest
{
    /// <summary>
    /// Response row for <c>/v3/cryptocurrency/quotes/latest</c>. V3 returns <c>quote</c> as an array
    /// where each element carries its own <c>id</c> and <c>symbol</c>, and <c>tags</c> as a list of
    /// objects rather than strings. This type is separate from <see cref="QuotesLatestData"/> (used
    /// by V1/V2) because the shape differs.
    /// </summary>
    public class QuotesLatestV3Data
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        [JsonPropertyName("is_active")]
        public int? IsActive { get; set; }

        [JsonPropertyName("is_fiat")]
        public int? IsFiat { get; set; }

        [JsonPropertyName("cmc_rank")]
        public int? CmcRank { get; set; }

        [JsonPropertyName("num_market_pairs")]
        public int? NumMarketPairs { get; set; }

        [JsonPropertyName("circulating_supply")]
        public double? CirculatingSupply { get; set; }

        [JsonPropertyName("total_supply")]
        public double? TotalSupply { get; set; }

        [JsonPropertyName("max_supply")]
        public double? MaxSupply { get; set; }

        [JsonPropertyName("infinite_supply")]
        public bool? InfiniteSupply { get; set; }

        [JsonPropertyName("date_added")]
        public DateTime? DateAdded { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTime? LastUpdated { get; set; }

        [JsonPropertyName("tags")]
        public List<TagData> Tags { get; set; } = new List<TagData>();

        [JsonPropertyName("platform")]
        public PlatformData? Platform { get; set; }

        [JsonPropertyName("self_reported_circulating_supply")]
        public double? SelfReportedCirculatingSupply { get; set; }

        [JsonPropertyName("self_reported_market_cap")]
        public double? SelfReportedMarketCap { get; set; }

        [JsonPropertyName("minted_market_cap")]
        public double? MintedMarketCap { get; set; }

        [JsonPropertyName("tvl_ratio")]
        public double? TvlRatio { get; set; }

        [JsonPropertyName("quote")]
        public List<QuoteData>? Quote { get; set; }
    }
}
