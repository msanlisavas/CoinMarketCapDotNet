using System;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Dex.Holders.Count
{
    public class DexHoldersCountData
    {
        [JsonPropertyName("token_address")]
        public string? TokenAddress { get; set; }

        [JsonPropertyName("network_slug")]
        public string? NetworkSlug { get; set; }

        [JsonPropertyName("holder_count")]
        public int? HolderCount { get; set; }

        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
    }
}
