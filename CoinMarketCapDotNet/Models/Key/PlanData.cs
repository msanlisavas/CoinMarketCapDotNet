using System.Text.Json.Serialization;
using System;

namespace CoinMarketCapDotNet.Models.Key
{
    public class PlanData
    {
        [JsonPropertyName("credit_limit_monthly")]
        public double CreditLimitMonthly { get; set; }

        [JsonPropertyName("credit_limit_monthly_reset")]
        public string? CreditLimitMonthlyReset { get; set; }

        [JsonPropertyName("credit_limit_monthly_reset_timestamp")]
        public DateTime? CreditLimitMonthlyResetTimestamp { get; set; }

        [JsonPropertyName("rate_limit_minute")]
        public double? RateLimitMinute { get; set; }
    }
}
