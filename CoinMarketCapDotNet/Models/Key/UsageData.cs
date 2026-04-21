using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Key
{
    public class UsageData
    {
        [JsonPropertyName("current_minute")]
        public CurrentMinuteData? CurrentMinute { get; set; }
        [JsonPropertyName("current_day")]
        public CurrentDayData? CurrentDay { get; set; }
        [JsonPropertyName("current_month")]
        public CurrentMonthData? CurrentMonth { get; set; }
    }
}
