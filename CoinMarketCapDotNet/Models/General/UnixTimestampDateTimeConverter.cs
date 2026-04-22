using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.General
{
    /// <summary>
    /// Reads a <see cref="DateTime"/> from a Unix epoch seconds value that may arrive as either a JSON
    /// number or a JSON string (e.g. <c>1776729600</c> or <c>"1776729600"</c>). Some CoinMarketCap
    /// endpoints — notably <c>/v3/fear-and-greed/historical</c> — emit timestamps this way instead
    /// of ISO-8601. Writes the value back as an ISO-8601 UTC string.
    /// </summary>
    public sealed class UnixTimestampDateTimeConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return null;

                case JsonTokenType.Number:
                    if (reader.TryGetInt64(out var asLong))
                        return DateTimeOffset.FromUnixTimeSeconds(asLong).UtcDateTime;
                    if (reader.TryGetDouble(out var asDouble))
                        return DateTimeOffset.FromUnixTimeSeconds((long)asDouble).UtcDateTime;
                    throw new JsonException($"Unable to read Unix timestamp as Int64 or Double.");

                case JsonTokenType.String:
                    {
                        var s = reader.GetString();
                        if (string.IsNullOrWhiteSpace(s)) return null;
                        if (long.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed))
                            return DateTimeOffset.FromUnixTimeSeconds(parsed).UtcDateTime;
                        // Fallback: if the server ever switches to ISO-8601, accept it transparently.
                        if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var iso))
                            return iso;
                        throw new JsonException($"Unable to parse '{s}' as a Unix timestamp or ISO-8601 DateTime.");
                    }

                default:
                    throw new JsonException($"Unexpected token {reader.TokenType} when reading Unix timestamp.");
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }
            writer.WriteStringValue(value.Value.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture));
        }
    }
}
