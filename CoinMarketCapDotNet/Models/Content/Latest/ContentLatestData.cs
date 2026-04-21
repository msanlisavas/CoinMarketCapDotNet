using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Content.Latest
{
    public class ContentLatestData
    {
        [JsonPropertyName("cover")]
        public string Cover { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("subtitle")]
        public string Subtitle { get; set; }

        [JsonPropertyName("source_name")]
        public string SourceName { get; set; }

        [JsonPropertyName("source_url")]
        public string SourceUrl { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("released_at")]
        public DateTime? ReleasedAt { get; set; }
        [JsonPropertyName("assets")]
        public List<ContentAssetsData> Assets { get; set; }
    }
}
