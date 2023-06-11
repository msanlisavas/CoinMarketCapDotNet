using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.General
{
    public class TimePeriodData
    {
        [JsonProperty("open_timestamp")]
        public string OpenTimestamp { get; set; }

        [JsonProperty("high_timestamp")]
        public string HighTimestamp { get; set; }

        [JsonProperty("low_timestamp")]
        public string LowTimestamp { get; set; }

        [JsonProperty("close_timestamp")]
        public string CloseTimestamp { get; set; }

        [JsonProperty("quote")]
        public Dictionary<string, TimePeriodQuoteData> Quote { get; set; }
    }
}
