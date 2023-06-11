using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Key
{
    public class UsageData
    {
        [JsonProperty("current_minute")]
        public CurrentMinuteData CurrentMinute { get; set; }
        [JsonProperty("current_day")]
        public CurrentDayData CurrentDay { get; set; }
        [JsonProperty("current_month")]
        public CurrentMonthData CurrentMonth { get; set; }
    }
}
