using CoinMarketCapDotNet.Models.General;
using System.Text.Json.Serialization;
using System;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Airdrops
{
    public class AirdropData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("project_name")]
        public string ProjectName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("start_date")]
        public DateTime? StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public DateTime? EndDate { get; set; }

        [JsonPropertyName("total_prize")]
        public int? TotalPrize { get; set; }

        [JsonPropertyName("winner_count")]
        public int? WinnerCount { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("coin")]
        public CoinData Coin { get; set; }
    }
}
