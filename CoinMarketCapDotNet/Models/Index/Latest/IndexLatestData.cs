using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Index.Latest
{
    public class IndexLatestData
    {
        [JsonPropertyName("value")]
        public double? Value { get; set; }

        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }

        [JsonPropertyName("constituents")]
        public List<IndexConstituentData> Constituents { get; set; } = new List<IndexConstituentData>();
    }
}
