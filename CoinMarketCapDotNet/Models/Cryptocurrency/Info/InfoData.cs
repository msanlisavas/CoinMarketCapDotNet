using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Info
{
    public class InfoData
    {
        [JsonProperty("urls")]
        public Dictionary<string, string[]> Urls { get; set; }
        [JsonProperty("notice")]
        public string Notice { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("date_added")]
        public DateTime? DateAdded { get; set; }

        [JsonProperty("date_launched")]
        public DateTime? DateLaunched { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("tag-groups")]
        public List<string> TagGroups { get; set; }
    }
}
