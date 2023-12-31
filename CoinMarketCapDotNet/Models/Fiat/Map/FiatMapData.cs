﻿using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Fiat.Map
{
    public class FiatMapData
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("sign")]
        public string Sign { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }




    }
}
