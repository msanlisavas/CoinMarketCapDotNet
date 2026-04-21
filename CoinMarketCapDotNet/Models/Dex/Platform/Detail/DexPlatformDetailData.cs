using System.Collections.Generic;
using System.Text.Json.Serialization;
using CoinMarketCapDotNet.Models.Dex.Platform.Common;

namespace CoinMarketCapDotNet.Models.Dex.Platform.Detail
{
    public class DexPlatformDetailData : DexPlatformData
    {
        [JsonPropertyName("supported_dexes")]
        public List<string> SupportedDexes { get; set; } = new List<string>();

        [JsonPropertyName("token_count")]
        public int? TokenCount { get; set; }

        [JsonPropertyName("pair_count")]
        public int? PairCount { get; set; }
    }
}
