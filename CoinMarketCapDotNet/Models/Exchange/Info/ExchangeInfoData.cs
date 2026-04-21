using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Exchange.Info
{
    // Docs data are exactly here. But the endpoint response is not the same as the docs. There are extra properties coming in.
    // https://coinmarketcap.com/api/documentation/v1/#operation/getV1ExchangeInfo
    public class ExchangeInfoData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("slug")]
        public string Slug { get; set; }
        [JsonPropertyName("logo")]
        public string Logo { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("date_launched")]
        public DateTime? DateLaunched { get; set; }
        [JsonPropertyName("notice")]
        public string Notice { get; set; }
        [JsonPropertyName("weekly_visits")]
        public int? WeeklyVisits { get; set; }
        [JsonPropertyName("spot_volume_usd")]
        public decimal? SpotVolumeUsd { get; set; }
        [JsonPropertyName("urls")]
        public Dictionary<string, string[]> Urls { get; set; }

    }
}
