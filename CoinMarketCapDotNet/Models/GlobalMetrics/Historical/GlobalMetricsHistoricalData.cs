using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.GlobalMetrics.Historical
{
    public class GlobalMetricsHistoricalData
    {
        [JsonProperty("quotes")]
        public List<GlobalMetricsHistoricalQuotesData> Quotes { get; set; }
    }
}
