using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Exchange.Assets
{
    public class AssetCurrencyData
    {
        [JsonProperty("crypto_id")]
        public int CryptoId { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price_usd")]
        public decimal? PriceUsd { get; set; }
    }
}
