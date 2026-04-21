using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Index
{
    public class IndexConstituentData
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("weight")]
        public double? Weight { get; set; }
    }
}
