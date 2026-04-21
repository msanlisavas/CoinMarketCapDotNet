using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Fiat.Map
{
    public class FiatMapData
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("sign")]
        public string Sign { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }




    }
}
