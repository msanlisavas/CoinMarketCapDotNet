using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Content.Latest
{
    public class ContentLatestData
    {
        [JsonProperty("cover")]
        public string Cover { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }

        [JsonProperty("source_name")]
        public string SourceName { get; set; }

        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("released_at")]
        public DateTime? ReleasedAt { get; set; }
        [JsonProperty("assets")]
        public List<ContentAssetsData> Assets { get; set; }
    }
}
