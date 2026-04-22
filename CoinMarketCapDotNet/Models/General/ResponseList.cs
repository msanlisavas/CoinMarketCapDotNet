using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.General
{
    public class ResponseList<T>
    {
        // Some CMC endpoints return `"data": null` on otherwise-successful responses (e.g. when a search
        // has no matches). The converter collapses null to an empty list so callers can always iterate
        // Data without a null check.
        [JsonPropertyName("data")]
        [JsonConverter(typeof(NullTolerantListConverterFactory))]
        public List<T> Data { get; set; } = new List<T>();

        [JsonPropertyName("status")]
        public Status? Status { get; set; }
    }

    internal sealed class NullTolerantListConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(List<>);

        public override JsonConverter CreateConverter(System.Type typeToConvert, JsonSerializerOptions options)
        {
            var itemType = typeToConvert.GetGenericArguments()[0];
            var converterType = typeof(NullTolerantListConverter<>).MakeGenericType(itemType);
            return (JsonConverter)System.Activator.CreateInstance(converterType)!;
        }
    }

    internal sealed class NullTolerantListConverter<T> : JsonConverter<List<T>>
    {
        // Without this, System.Text.Json short-circuits on null JSON tokens and assigns null to the
        // property without calling this converter — defeating the whole purpose.
        public override bool HandleNull => true;

        public override List<T> Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return new List<T>();
            return JsonSerializer.Deserialize<List<T>>(ref reader, options) ?? new List<T>();
        }

        public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
