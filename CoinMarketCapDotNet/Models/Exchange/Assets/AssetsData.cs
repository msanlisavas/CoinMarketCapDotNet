using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Exchange.Assets
{
    public class AssetsData
    {

        [JsonPropertyName("wallet_address")]
        public string? WalletAddress { get; set; }
        [JsonPropertyName("balance")]
        public decimal? Balance { get; set; }
        [JsonPropertyName("platform")]
        public AssetPlatformData? Platform { get; set; }
        [JsonPropertyName("currency")]
        public AssetCurrencyData? Currency { get; set; }
    }
}
