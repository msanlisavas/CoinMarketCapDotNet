using Newtonsoft.Json;
using System;

namespace CoinMarketCapDotNet.Models.Key
{
    public class PlanData
    {
        [JsonProperty("credit_limit_monthly")]
        public double CreditLimitMonthly { get; set; }

        [JsonProperty("credit_limit_monthly_reset")]
        public string CreditLimitMonthlyReset { get; set; }

        [JsonProperty("credit_limit_monthly_reset_timestamp")]
        public DateTime? CreditLimitMonthlyResetTimestamp { get; set; }

        [JsonProperty("rate_limit_minute")]
        public double? RateLimitMinute { get; set; }
    }
}
