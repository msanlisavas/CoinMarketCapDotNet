using CoinMarketCapDotNet.Models.General;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Info
{
    public class InfoData
    {
        [JsonPropertyName("urls")]
        public Dictionary<string, string[]>? Urls { get; set; }
        [JsonPropertyName("notice")]
        public string? Notice { get; set; }

        [JsonPropertyName("logo")]
        public string? Logo { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("date_added")]
        public DateTime? DateAdded { get; set; }

        [JsonPropertyName("date_launched")]
        public DateTime? DateLaunched { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new List<string>();

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("platform")]
        public PlatformData? Platform { get; set; }

        [JsonPropertyName("tag-groups")]
        public List<string> TagGroups { get; set; } = new List<string>();
    }
}
