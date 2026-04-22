using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Dex.Pairs.Common
{
    /// <summary>
    /// DEX trading pair (v4). Fields mirror the live <c>/v4/dex/spot-pairs/latest</c> and
    /// <c>/v4/dex/pairs/quotes/latest</c> schemas, which use <c>base_asset_*</c> / <c>quote_asset_*</c>
    /// naming and nest price/volume under a <c>quote</c> array.
    /// </summary>
    public class DexPairData
    {
        [JsonPropertyName("contract_address")]
        public string? ContractAddress { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("base_asset_id")]
        public string? BaseAssetId { get; set; }

        [JsonPropertyName("base_asset_ucid")]
        public string? BaseAssetUcid { get; set; }

        [JsonPropertyName("base_asset_name")]
        public string? BaseAssetName { get; set; }

        [JsonPropertyName("base_asset_symbol")]
        public string? BaseAssetSymbol { get; set; }

        [JsonPropertyName("base_asset_contract_address")]
        public string? BaseAssetContractAddress { get; set; }

        [JsonPropertyName("quote_asset_id")]
        public string? QuoteAssetId { get; set; }

        [JsonPropertyName("quote_asset_ucid")]
        public string? QuoteAssetUcid { get; set; }

        [JsonPropertyName("quote_asset_name")]
        public string? QuoteAssetName { get; set; }

        [JsonPropertyName("quote_asset_symbol")]
        public string? QuoteAssetSymbol { get; set; }

        [JsonPropertyName("quote_asset_contract_address")]
        public string? QuoteAssetContractAddress { get; set; }

        [JsonPropertyName("dex_id")]
        public string? DexId { get; set; }

        [JsonPropertyName("dex_slug")]
        public string? DexSlug { get; set; }

        [JsonPropertyName("network_id")]
        public string? NetworkId { get; set; }

        [JsonPropertyName("network_slug")]
        public string? NetworkSlug { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTime? LastUpdated { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("scroll_id")]
        public string? ScrollId { get; set; }

        [JsonPropertyName("quote")]
        public List<DexPairQuoteEntry>? Quote { get; set; }
    }

    /// <summary>
    /// Per-currency quote entry nested under <see cref="DexPairData.Quote"/>.
    /// </summary>
    public class DexPairQuoteEntry
    {
        [JsonPropertyName("convert_id")]
        public string? ConvertId { get; set; }

        [JsonPropertyName("price")]
        public double? Price { get; set; }

        [JsonPropertyName("price_by_quote_asset")]
        public double? PriceByQuoteAsset { get; set; }

        [JsonPropertyName("volume_24h")]
        public double? Volume24h { get; set; }

        [JsonPropertyName("percent_change_price_1h")]
        public double? PercentChangePrice1h { get; set; }

        [JsonPropertyName("percent_change_price_24h")]
        public double? PercentChangePrice24h { get; set; }

        [JsonPropertyName("liquidity")]
        public double? Liquidity { get; set; }

        [JsonPropertyName("fully_diluted_value")]
        public double? FullyDilutedValue { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTime? LastUpdated { get; set; }
    }
}
