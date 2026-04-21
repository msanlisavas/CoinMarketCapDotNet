using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Dex.Holders.TagCount
{
    public class DexHoldersTagCountData
    {
        [JsonPropertyName("tag")]
        public string? Tag { get; set; }

        [JsonPropertyName("count")]
        public int? Count { get; set; }

        [JsonPropertyName("percentage")]
        public double? Percentage { get; set; }
    }
}
