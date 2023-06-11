using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Exchange.Info
{
    // Docs data are exactly here. But the endpoint response is not the same as the docs. There are extra properties coming in.
    // https://coinmarketcap.com/api/documentation/v1/#operation/getV1ExchangeInfo
    public class ExchangeInfoData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("slug")]
        public string Slug { get; set; }
        [JsonProperty("logo")]
        public string Logo { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("date_launched")]
        public DateTime DateLaunched { get; set; }
        [JsonProperty("notice")]
        public string Notice { get; set; }
        [JsonProperty("weekly_visits")]
        public int WeeklyVisits { get; set; }
        [JsonProperty("spot_volume_usd")]
        public decimal SpotVolumeUsd { get; set; }
        [JsonProperty("urls")]
        public Dictionary<string, string[]> Urls { get; set; }

    }
}
