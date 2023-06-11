﻿using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Content.Latest
{
    public class ContentAssetsData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }
    }
}
