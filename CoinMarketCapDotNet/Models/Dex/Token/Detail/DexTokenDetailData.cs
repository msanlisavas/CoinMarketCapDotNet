using CoinMarketCapDotNet.Models.Dex.Token.Common;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Dex.Token.Detail
{
    public class DexTokenDetailData : DexTokenSummary
    {
        [JsonPropertyName("decimals")]
        public int? Decimals { get; set; }

        [JsonPropertyName("total_supply")]
        public double? TotalSupply { get; set; }

        [JsonPropertyName("circulating_supply")]
        public double? CirculatingSupply { get; set; }

        [JsonPropertyName("contract_address")]
        public string? ContractAddress { get; set; }

        [JsonPropertyName("creation_timestamp")]
        public long? CreationTimestamp { get; set; }

        [JsonPropertyName("logo_url")]
        public string? LogoUrl { get; set; }
    }
}
