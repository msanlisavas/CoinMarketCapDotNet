# CoinMarketCapDotNet

[![NuGet](https://img.shields.io/nuget/v/CoinMarketCapDotNet.svg?label=NuGet)](https://www.nuget.org/packages/CoinMarketCapDotNet)
[![Downloads](https://img.shields.io/nuget/dt/CoinMarketCapDotNet.svg?label=downloads)](https://www.nuget.org/packages/CoinMarketCapDotNet)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.txt)

CoinMarketCapDotNet is a C# wrapper for the CoinMarketCap API, providing convenient access to cryptocurrency market data.

## Migrating from v1?

See [MIGRATION-V1-TO-V2.md](MIGRATION-V1-TO-V2.md) for the full v1 → v2 migration guide.

## Installation

You can install CoinMarketCapDotNet via NuGet Package Manager or by using the .NET CLI:

### NuGet Package Manager

Search for `CoinMarketCapDotNet` in the NuGet Package Manager UI or run the following command in the Package Manager Console:

```
Install-Package CoinMarketCapDotNet
```

### .NET CLI

Run the following command in your project directory:

```
dotnet add package CoinMarketCapDotNet
```

### Package Manager

Run the following command:

```
NuGet\Install-Package CoinMarketCapDotNet
```

v2 multi-targets `netstandard2.0` and `net8.0`. On `.NET 8` the package has zero runtime dependencies; on `netstandard2.0` it brings only `System.Text.Json` 8.x.

## Usage

### Quick start

```csharp
using CoinMarketCapDotNet.Api;

var api = new CoinMarketCapAPI("YOUR_API_KEY");
var data = await api.Cryptocurrency.GetMapAsync();
```

To use sandbox mode:

```csharp
var api = new CoinMarketCapAPI("YOUR_API_KEY", useSandbox: true);
```

### Options pattern (recommended for production)

```csharp
using CoinMarketCapDotNet.Api;
using CoinMarketCapDotNet.Configuration;

var api = new CoinMarketCapAPI(new CoinMarketCapOptions
{
    ApiKey = "YOUR_API_KEY",
    UseSandbox = false,
    Timeout = TimeSpan.FromSeconds(30)
});

using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
var data = await api.Cryptocurrency.GetMapAsync(cancellationToken: cts.Token);
```

### Error handling

```csharp
using CoinMarketCapDotNet.Models.Exceptions;

try
{
    var data = await api.Cryptocurrency.GetMapAsync();
}
catch (CoinMarketCapRateLimitException)  { /* back off */ }
catch (CoinMarketCapAuthException)       { /* check your API key */ }
catch (CoinMarketCapException ex)
{
    // Catch-all preserves ex.StatusCode, ex.ErrorCode, ex.CmcErrorMessage
}
```

## CoinMarketCapAPI

### Endpoints

- **CryptocurrencyEndpoint**: Provides endpoints related to cryptocurrencies.
- **FiatEndpoint**: Provides endpoints related to fiat currencies.
- **ExchangeEndpoint**: Provides endpoints related to exchanges.
- **GlobalMetricsEndpoint**: Provides endpoints related to global market metrics.
- **ToolsEndpoint**: Provides miscellaneous tools and utilities.
- **BlockchainEndpoint**: Provides endpoints related to blockchain statistics.
- **KeyEndpoint**: Provides endpoints related to API keys.
- **ContentEndpoint**: Provides endpoints related to content (news, articles, etc.).
- **CommunityEndpoint**: Provides endpoints related to the community.

---

## CryptocurrencyEndpoint

### Methods

- **GetAirdropAsync**: Retrieves airdrop details asynchronously.
- **GetAirdropsAsync**: Retrieves a list of airdrops asynchronously.
- **GetCategoriesAsync**: Retrieves a list of cryptocurrency categories asynchronously.
- **GetCategoryAsync**: Retrieves category details asynchronously.
- **GetMapAsync**: Retrieves a mapping of all cryptocurrencies to unique CoinMarketCap IDs asynchronously.
- **GetInfoAsync**: Retrieves cryptocurrency information asynchronously.
- **GetListingLatestAsync**: Retrieves the latest cryptocurrency listings asynchronously.
- **GetListingHistoricalAsync**: Retrieves historical cryptocurrency listings asynchronously.
- **GetListingNewAsync**: Retrieves new cryptocurrency listings asynchronously.
- **GetTrendingGainersLosersAsync**: Retrieves trending gainers and losers asynchronously.
- **GetTrendingLatestAsync**: Retrieves the latest trending cryptocurrencies asynchronously.
- **GetTrendingMostVisitedAsync**: Retrieves the most visited trending cryptocurrencies asynchronously.
- **GetMarketPairsLatestAsync**: Retrieves the latest cryptocurrency market pairs asynchronously.
- **GetOHLCVLatestAsync**: Retrieves the latest cryptocurrency OHLCV (Open, High, Low, Close, Volume) data asynchronously.
- **GetOHCLVHistoricalAsync**: Retrieves historical cryptocurrency OHLCV (Open, High, Low, Close, Volume) data asynchronously.
- **GetPricePerformanceStatsLatestAsync**: Retrieves the latest cryptocurrency price performance statistics asynchronously.
- **GetQuotesHistoricalV2Async**: Retrieves historical cryptocurrency quotes using V2 endpoint asynchronously.
- **GetQuotesLatestAsync**: Retrieves the latest cryptocurrency quotes asynchronously.
- **GetQuotesHistoricalV3Async**: Retrieves historical cryptocurrency quotes using V3 endpoint asynchronously.

---

## FiatEndpoint

### Methods

- **GetMapAsync**: Retrieves a mapping of all fiat currencies to unique CoinMarketCap IDs asynchronously.

---

## ExchangeEndpoint

### Methods

- **GetAssetsAsync**: Retrieves a list of exchange assets asynchronously.
- **GetInfoAsync**: Retrieves exchange information asynchronously.
- **GetMapAsync**: Retrieves a mapping of all exchanges to unique CoinMarketCap IDs asynchronously.
- **GetListingLatestAsync**: Retrieves the latest exchange listings asynchronously.
- **GetMarketPairsAsync**: Retrieves exchange market pairs asynchronously.
- **GetQuotesHistoricalAsync**: Retrieves historical exchange quotes asynchronously.
- **GetQuotesLatestAsync**: Retrieves the latest exchange quotes asynchronously.

## GlobalMetricsEndpoint

### Methods

- **GetQuotesHistoricalAsync**: Retrieves historical global market metrics quotes asynchronously.
- **GetQuotesLatestAsync**: Retrieves the latest global market metrics quotes asynchronously.

---

## ToolsEndpoint

### Methods

- **GetPriceConversionAsync**: Retrieves price conversion data asynchronously.

---

## BlockchainEndpoint

### Methods

- **GetStatisticsLatestAsync**: Retrieves the latest blockchain statistics data asynchronously.

---

## KeyEndpoint

### Methods

- **GetKeyInfoAsync**: Retrieves API key details and usage stats asynchronously.

---

## ContentEndpoint

### Methods

- **GetContentLatestAsync**: Retrieves the latest crypto-related posts from the CMC Community asynchronously.
- **GetPostCommentsAsync**: Retrieves comments of a CMC Community post asynchronously.
- **GetPostLatestAsync**: Retrieves the latest crypto-related posts from the CMC Community asynchronously.
- **GetPostTopAsync**: Retrieves the top crypto-related posts from the CMC Community asynchronously.

---

## CommunityEndpoint

### Methods

- **GetTrendingTokenAsync**: Retrieves the latest trending tokens from the CMC Community asynchronously.
- **GetTrendingTopicAsync**: Retrieves the latest trending topics from the CMC Community asynchronously.

---

For example, to get the cryptocurrency map:

```csharp
var data = await api.Cryptocurrency.GetMapAsync();  // Retrieves a mapping of all cryptocurrencies to unique CoinMarketCap IDs.
```

## Extensions to make life easier

**Enum Extensions:**

**.GetEnumMemberValue():**
Example:
```
CurrencyEnum currency = CurrencyEnum.EUR;
string enumMemberValue = currency.GetEnumMemberValue(); // Should return "USD" you can use this to fetch USD from endpoints.
```

**.GetId():**
Example:
```
CurrencyEnum currency = CurrencyEnum.USD;
int id = currency.GetId(); // You should get the cmc equivalent of EUR id.
```

**.GetSymbol():**
Example:
```
CurrencyEnum currency = CurrencyEnum.TRY; // You should get "TRY"
string symbol = currency.GetSymbol();
```

**.GetCurrencyIds():**
Example:
```
string symbols = "USD,EUR,TRY";
List<int> ids = symbols.GetCurrencyIds(); // Returns the symbol strings as a id list to be used in endpoints.
```

**.GetAllIds():**
Example:
```
List<int> allIds = EnumExtensions.GetAllIds<CategoryEnum>(); // Will return all the ids of a given enum
```

**.GetAllSymbols():**
Example:
```
List<string> enumMemberValues = EnumExtensions.GetAllSymbols<CurrencyEnum>(); // Will return all the symbols of a given enum
```

Refer to the CoinMarketCap API documentation for more information on available endpoints and their usage.

## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

## License

MIT License

## Release Notes

### v2.1.0

- Added Fear and Greed Index endpoint group: `api.FearAndGreed.GetLatestAsync()`, `api.FearAndGreed.GetHistoricalAsync(...)`.
- Added CMC Index endpoint group with CMC 100 and CMC 20 latest + historical: `api.Index.GetCmc100LatestAsync()`, `api.Index.GetCmc100HistoricalAsync(...)`, `api.Index.GetCmc20LatestAsync()`, `api.Index.GetCmc20HistoricalAsync(...)`.
- Added v3 cryptocurrency methods: `GetQuotesLatestV3Async` and `GetListingLatestV3Async`. The existing v1/v2 methods are unchanged.
- Fully additive release — no breaking changes from v2.0.0.

### v2.0.0

- Multi-targets `netstandard2.0` and `net8.0`.
- Migrated to `System.Text.Json` — zero runtime dependencies on .NET 8.
- Typed exception hierarchy under `CoinMarketCapDotNet.Models.Exceptions`.
- `CancellationToken` on every async method.
- Options-pattern configuration via `CoinMarketCapOptions`.
- Injectable `HttpClient` for `IHttpClientFactory` / DI scenarios.
- Nullable reference types enabled throughout.
- See [MIGRATION-V1-TO-V2.md](MIGRATION-V1-TO-V2.md) for upgrade instructions.

For older v1.x release notes, see the [GitHub Releases page](https://github.com/msanlisavas/CoinMarketCapDotNet/releases).

## Known Issues

Sandbox results are kind of different than the live ones. Tests that pass when the sandbox is true don't pass on live mode and cause serialization issues. The endpoints are tested on sandbox results. Not all endpoints are available for the free tier.

Unless CoinMarketCap provides an enterprise key I won't be able to make sure everything is on point. Please create an issue if you encounter a problem.

On top of that, the API Docs Response Schema is not always the same as the response we get. I will be sticking with the response schema for now.

## Buy me a coffee?

```
BTC: 1NxUuEQcR4Scw8ge3oto6ykLqBpe9LGikS
ETH: 0x9cda155f73220073a9f024daaa72eb06b5c06c86
```
