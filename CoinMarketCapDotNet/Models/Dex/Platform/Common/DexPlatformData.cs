using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Dex.Platform.Common
{
    public class DexPlatformData
    {
        [JsonPropertyName("network_slug")]
        public string? NetworkSlug { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("chain_id")]
        public string? ChainId { get; set; }

        [JsonPropertyName("explorer_url")]
        public string? ExplorerUrl { get; set; }

        [JsonPropertyName("wrapped_token_address")]
        public string? WrappedTokenAddress { get; set; }

        [JsonPropertyName("wrapped_token_symbol")]
        public string? WrappedTokenSymbol { get; set; }

        [JsonPropertyName("logo_url")]
        public string? LogoUrl { get; set; }
    }
}
