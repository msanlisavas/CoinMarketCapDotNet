using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Exchange.Assets
{
    public class AssetPlatformData
    {
        [JsonProperty("crypto_id")]
        public int CryptoId { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

    }
}
