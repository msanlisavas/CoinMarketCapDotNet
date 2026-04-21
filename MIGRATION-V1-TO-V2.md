# Migrating from CoinMarketCapDotNet v1 to v2

v2 is a focused modernization release. The wire-level behavior is unchanged — every endpoint returns the same response shape it did in v1. What changed is the framework targets, the JSON dependency, and the public method signatures.

## What changed and why

- **Target frameworks:** v1 targeted only `.NET Framework 4.7.2`. v2 multi-targets `netstandard2.0` and `net8.0`. .NET Framework 4.6.1+, Mono, Unity, and any .NET Core 2.0+/.NET 5+ runtime can install v2.
- **JSON serializer:** Newtonsoft.Json has been replaced by `System.Text.Json`.
- **Runtime dependencies:** zero on `.NET 8`; a single `System.Text.Json` package on `netstandard2.0`.
- **API surface:** typed exceptions, `CancellationToken` on every async method, options-pattern configuration, injectable `HttpClient`, nullable reference types annotated.

## Framework support matrix

| Consumer runtime         | v1 (.NET Framework 4.7.2) | v2 (netstandard2.0;net8.0) |
|--------------------------|---------------------------|----------------------------|
| .NET Framework 4.6.0     | not supported             | not supported              |
| .NET Framework 4.6.1+    | not supported             | supported (via netstandard2.0) |
| .NET Framework 4.7.2+    | supported                 | supported (via netstandard2.0) |
| .NET Core 2.0+ / .NET 5+ | not supported             | supported                  |
| .NET 8                   | not supported             | supported (zero deps)      |
| Unity 2021.2+            | not supported             | supported                  |

## Dependency changes

- `Newtonsoft.Json` is no longer a dependency. If your application only depended on it transitively through this package, you can remove it from your own `csproj`.
- On `.NET 8`, the package has zero NuGet dependencies — `System.Text.Json` is part of the shared framework.
- On `netstandard2.0`, the package brings `System.Text.Json` 8.x as its only dependency.

## Constructor migration

### v1 — simple case

```csharp
var api = new CoinMarketCapAPI("YOUR_API_KEY");
var sandbox = new CoinMarketCapAPI("YOUR_API_KEY", useSandbox: true);
```

### v2 — simple case still works

```csharp
var api = new CoinMarketCapAPI("YOUR_API_KEY");
var sandbox = new CoinMarketCapAPI("YOUR_API_KEY", useSandbox: true);
```

### v2 — options pattern (recommended)

```csharp
using CoinMarketCapDotNet.Api;
using CoinMarketCapDotNet.Configuration;

var api = new CoinMarketCapAPI(new CoinMarketCapOptions
{
    ApiKey = "YOUR_API_KEY",
    UseSandbox = false,
    Timeout = TimeSpan.FromSeconds(30)
});
```

### v2 — with `IHttpClientFactory` (ASP.NET Core)

```csharp
// Program.cs
builder.Services.AddHttpClient("CoinMarketCap");
builder.Services.AddSingleton(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return new CoinMarketCapAPI(
        new CoinMarketCapOptions { ApiKey = builder.Configuration["CMC:ApiKey"]! },
        factory.CreateClient("CoinMarketCap"));
});
```

## Exception handling migration

### v1

```csharp
try
{
    var data = await api.Cryptocurrency.GetMapAsync();
}
catch (Exception ex)
{
    // Caller had no way to distinguish auth failures from rate limits from server errors.
    Console.WriteLine(ex.Message);
}
```

### v2

```csharp
using CoinMarketCapDotNet.Models.Exceptions;

try
{
    var data = await api.Cryptocurrency.GetMapAsync();
}
catch (CoinMarketCapAuthException ex)
{
    // 401 / 403 — bad or expired API key.
}
catch (CoinMarketCapRateLimitException ex)
{
    // 429 — back off.
}
catch (CoinMarketCapBadRequestException ex)
{
    // 400 — bad query parameters.
}
catch (CoinMarketCapServerException ex)
{
    // 5xx — transient. Retry-with-backoff is the consumer's responsibility.
}
catch (CoinMarketCapException ex)
{
    // Catch-all for any other CMC failure (preserves StatusCode, ErrorCode, CmcErrorMessage).
}
```

All typed exceptions derive from `CoinMarketCapException`, which derives from `System.Exception`. Old `catch (Exception)` blocks continue to work.

## CancellationToken

Every async method now accepts an optional `CancellationToken` as its last parameter:

```csharp
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
var data = await api.Cryptocurrency.GetMapAsync(cancellationToken: cts.Token);
```

**One quirk to know about:** `GetPricePerformanceStatsLatestAsync` ends with a `params string[] timePeriods` parameter. C# requires `params` to be the last parameter, so `cancellationToken` is placed BEFORE it. To pass both, use named arguments:

```csharp
await api.Cryptocurrency.GetPricePerformanceStatsLatestAsync(
    id: "1",
    cancellationToken: cts.Token,
    timePeriods: new[] { "24h", "7d" });
```

## Nullable reference types

v2 enables nullable reference types throughout. Many model properties that were declared as non-nullable in v1 are now correctly typed as nullable to reflect that the CoinMarketCap API legitimately omits them in some payloads. If your project also has `<Nullable>enable</Nullable>`, you may see new compiler hints on response model accesses — these are surfacing real null-handling gaps, not breaking changes.

Notable public API surface changes from this work:
- `EnumExtensions.GetEnumMemberValue()` returns `string?` (was `string`).
- `EnumExtensions.GetSymbol()` returns `string?` (was `string`).
- `EnumExtensions.GetAllSymbols<T>()` returns `List<string?>` (was `List<string>`).
- `DictionaryExtensions` generic methods now require `where TKey : notnull`. If you used these with a nullable key type, you will need to adjust.
- `Response<T>.Status`, `ResponseDict<T>.Data`, `NestedResponseList<T>.Data` are nullable. Existing access chains like `result.Data.Whatever` may need a `?.` operator.

## JSON payload shape

Response model property names and types are otherwise unchanged. Code that reads `Response<T>.Data.Whatever` continues to work as-is (modulo the nullable annotations above). Internally the deserialization is now case-insensitive, lenient about quoted numbers, and uses `System.Text.Json`'s `JsonStringEnumConverter` for enum-shaped fields — the same tolerances Newtonsoft provided.

If you wrote a custom `JsonConverter<T>` against Newtonsoft and applied it to a model in this library: rewrite it against `System.Text.Json.Serialization.JsonConverter<T>` and re-attach via `[JsonConverter(typeof(YourConverter))]`. The shape of the converter API is similar but not identical.

## Common pitfalls

- **Casing:** the deserializer is case-insensitive by default in v2. If you previously relied on case-sensitive failure to detect bad payloads, configure your own `JsonSerializerOptions` and use `JsonSerializer.Deserialize` directly on the response data.
- **Enums:** v2 deserializes enums from their string names (e.g. `"USD"` → `CurrencyEnum.USD`) using `JsonStringEnumConverter`. Numeric enum representations are not supported by default.
- **Timeouts:** the v1 wrapper used the static `HttpClient`'s default timeout (100 seconds). v2 still defaults to that but lets you override via `CoinMarketCapOptions.Timeout`. When you specify `Timeout` and DO NOT inject your own `HttpClient`, v2 creates a per-instance `HttpClient` to avoid mutating shared state.
