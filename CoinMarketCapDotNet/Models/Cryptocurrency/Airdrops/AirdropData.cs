using CoinMarketCapDotNet.Models.General;
using Newtonsoft.Json;
using System;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Airdrops
{
    public class AirdropData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("project_name")]
        public string ProjectName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("start_date")]
        public DateTime? StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("total_prize")]
        public int? TotalPrize { get; set; }

        [JsonProperty("winner_count")]
        public int? WinnerCount { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("coin")]
        public CoinData Coin { get; set; }
    }
}
