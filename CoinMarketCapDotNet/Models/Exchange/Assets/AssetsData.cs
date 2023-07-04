using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Exchange.Assets
{
    public class AssetsData
    {

        [JsonProperty("wallet_address")]
        public string WalletAddress { get; set; }
        [JsonProperty("balance")]
        public decimal? Balance { get; set; }
        [JsonProperty("platform")]
        public AssetPlatformData Platform { get; set; }
        [JsonProperty("currency")]
        public AssetCurrencyData Currency { get; set; }
    }
}
