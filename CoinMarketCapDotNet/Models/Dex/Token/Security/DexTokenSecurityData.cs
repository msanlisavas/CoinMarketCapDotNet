using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Dex.Token.Security
{
    public class DexTokenSecurityData
    {
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("network_slug")]
        public string? NetworkSlug { get; set; }

        [JsonPropertyName("is_honeypot")]
        public bool? IsHoneypot { get; set; }

        [JsonPropertyName("is_mintable")]
        public bool? IsMintable { get; set; }

        [JsonPropertyName("is_proxy")]
        public bool? IsProxy { get; set; }

        [JsonPropertyName("buy_tax")]
        public double? BuyTax { get; set; }

        [JsonPropertyName("sell_tax")]
        public double? SellTax { get; set; }

        [JsonPropertyName("audit_warnings")]
        public List<string> AuditWarnings { get; set; } = new List<string>();
    }
}
