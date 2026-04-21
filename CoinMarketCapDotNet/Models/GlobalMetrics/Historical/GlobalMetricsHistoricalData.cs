using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.GlobalMetrics.Historical
{
    public class GlobalMetricsHistoricalData
    {
        [JsonPropertyName("quotes")]
        public List<GlobalMetricsHistoricalQuotesData> Quotes { get; set; }
    }
}
