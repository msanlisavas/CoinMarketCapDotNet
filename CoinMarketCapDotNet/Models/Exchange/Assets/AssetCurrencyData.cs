using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Exchange.Assets
{
    public class AssetCurrencyData
    {
        [JsonPropertyName("crypto_id")]
        public int CryptoId { get; set; }

        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("price_usd")]
        public decimal? PriceUsd { get; set; }
    }
}
