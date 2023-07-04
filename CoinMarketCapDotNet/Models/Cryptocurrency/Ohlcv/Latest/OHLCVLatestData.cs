using CoinMarketCapDotNet.Models.General;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Ohlcv.Latest
{
    public class OHLCVLatestData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("last_updated")]
        public DateTime? LastUpdated { get; set; }

        [JsonProperty("time_open")]
        public DateTime? TimeOpen { get; set; }

        [JsonProperty("time_high")]
        public DateTime? TimeHigh { get; set; }

        [JsonProperty("time_low")]
        public DateTime? TimeLow { get; set; }

        [JsonProperty("time_close")]
        public DateTime? TimeClose { get; set; }

        [JsonProperty("quote")]
        public Dictionary<string, MarketQuoteData> Quotes { get; set; }
    }
}
