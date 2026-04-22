using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.General
{
    /// <summary>
    /// Accepts <c>true</c>/<c>false</c>, the integers <c>0</c>/<c>1</c>, or the strings
    /// <c>"0"</c>/<c>"1"</c>/<c>"true"</c>/<c>"false"</c> when reading a <see cref="bool"/>.
    /// CoinMarketCap's Map and ExchangeMap endpoints emit <c>is_active</c> as a number, not a
    /// boolean — this converter bridges that without a type change on the model.
    /// </summary>
    public sealed class NumericBoolConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.TokenType switch
        {
            JsonTokenType.True => true,
            JsonTokenType.False => false,
            JsonTokenType.Number => reader.TryGetInt64(out var n) && n != 0,
            JsonTokenType.String => ReadString(reader.GetString()),
            _ => throw new JsonException($"Cannot convert {reader.TokenType} to bool."),
        };

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) =>
            writer.WriteBooleanValue(value);

        private static bool ReadString(string? s)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;
            if (bool.TryParse(s, out var b)) return b;
            if (long.TryParse(s, out var n)) return n != 0;
            throw new JsonException($"Cannot convert '{s}' to bool.");
        }
    }
}
