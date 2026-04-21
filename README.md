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

### Retries and rate-limit backoff (optional)

The wrapper does not build in a retry policy — the right behavior varies by consumer. If you want automatic retries (e.g. exponential backoff on 429 and 5xx), layer a `DelegatingHandler` onto your `HttpClient` before injecting it. The cleanest way is [Polly](https://www.nuget.org/packages/Polly) via `Microsoft.Extensions.Http.Polly`:

```csharp
using CoinMarketCapDotNet.Api;
using CoinMarketCapDotNet.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Polly;
using Polly.Extensions.Http;

var services = new ServiceCollection();

services.AddHttpClient("CoinMarketCap")
    .AddPolicyHandler(HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(r => (int)r.StatusCode == 429)
        .WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(Math.Pow(2, retry))));

services.AddSingleton<CoinMarketCapAPI>(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return new CoinMarketCapAPI(
        new CoinMarketCapOptions { ApiKey = "YOUR_API_KEY" },
        factory.CreateClient("CoinMarketCap"));
});
```

You can layer any `DelegatingHandler` this way — Polly is not a dependency of the wrapper itself.

## CoinMarketCapAPI

### Endpoints

- **CryptocurrencyEndpoint**: Endpoints related to cryptocurrencies.
- **FiatEndpoint**: Endpoints related to fiat currencies.
- **ExchangeEndpoint**: Endpoints related to exchanges.
- **GlobalMetricsEndpoint**: Endpoints related to global market metrics.
- **ToolsEndpoint**: Miscellaneous tools and utilities.
- **BlockchainEndpoint**: Endpoints related to blockchain statistics.
- **KeyEndpoint**: Endpoints related to API keys.
- **ContentEndpoint**: Endpoints related to content (news, articles, etc.).
- **CommunityEndpoint**: Endpoints related to the community.
- **FearAndGreedEndpoint**: CoinMarketCap Fear & Greed Index (added in v2.1.0).
- **IndexEndpoint**: CMC 100 and CMC 20 market indices (added in v2.1.0).
- **DexEndpoint**: DEX data — tokens, pairs, platforms, holders, K-line OHLCV (added across v2.2.0–v2.4.0). Has nested sub-endpoints: `Token`, `Pairs`, `Platform`, `Kline`, `Holders`.

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
- **GetQuotesLatestV3Async**: Retrieves the latest cryptocurrency quotes using V3 endpoint asynchronously *(v2.1.0)*.
- **GetListingLatestV3Async**: Retrieves the latest cryptocurrency listings using V3 endpoint asynchronously *(v2.1.0)*.

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

## FearAndGreedEndpoint *(v2.1.0)*

### Methods

- **GetLatestAsync**: Retrieves the latest CoinMarketCap Fear & Greed Index value with classification asynchronously.
- **GetHistoricalAsync**: Retrieves historical Fear & Greed Index values asynchronously.

---

## IndexEndpoint *(v2.1.0)*

### Methods

- **GetCmc100LatestAsync**: Retrieves the latest CMC 100 Index value with its constituent weights asynchronously.
- **GetCmc100HistoricalAsync**: Retrieves historical CMC 100 Index values at the requested interval asynchronously.
- **GetCmc20LatestAsync**: Retrieves the latest CMC 20 Index value with its constituent weights asynchronously.
- **GetCmc20HistoricalAsync**: Retrieves historical CMC 20 Index values at the requested interval asynchronously.

---

## DexEndpoint *(v2.2.0–v2.4.0)*

27 DEX endpoints across 5 nested sub-groups. Access them via `api.Dex.<SubGroup>.<Method>Async(...)`.

### Dex.Token *(v2.2.0, 14 methods)*

Token-level DEX data. Mix of POST and GET.

- **GetTrendingAsync** *(POST)*: Trending DEX tokens, filterable by network.
- **BatchQueryAsync** *(POST)*: Multi-token query in one request.
- **BatchPriceAsync** *(POST)*: Multi-token price query.
- **GetNewListAsync** *(POST)*: Newly launched DEX tokens.
- **GetMemeListAsync** *(POST)*: DEX meme tokens.
- **GetGainerLoserAsync** *(POST)*: Top DEX gainers and losers.
- **GetTokenAsync**: Detailed information for a specific token.
- **GetPriceAsync**: Current price for a specific token.
- **GetPoolsAsync**: All liquidity pools for a token.
- **GetLiquidityAsync**: Current liquidity snapshot.
- **GetTransactionsAsync**: Recent swap/trade history.
- **GetSecurityAsync**: Security audit summary (honeypot detection, buy/sell taxes).
- **SearchAsync**: Search DEX tokens by keyword.
- **GetLiquidityChangeAsync**: Liquidity change history.

### Dex.Pairs *(v2.3.0, 2 methods)*

DEX trading pair data.

- **GetSpotPairsLatestAsync**: Latest active DEX spot pairs.
- **GetQuotesLatestAsync**: Latest market quotes for a specific trading pair.

### Dex.Platform *(v2.3.0, 2 methods)*

Blockchain network metadata.

- **GetListAsync**: List of all DEX-supported blockchain platforms.
- **GetDetailAsync**: Detailed metadata for a specific platform (chain ID, explorer URL, supported DEXs).

### Dex.Kline *(v2.3.0, 2 methods)*

DEX OHLCV chart data.

- **GetPointsAsync**: K-line price points (timestamp + price + volume).
- **GetCandlesAsync**: K-line OHLCV candles with trader count.

### Dex.Holders *(v2.4.0, 5 methods)*

Wallet distribution and holder classification.

- **GetListAsync** *(POST)*: Paginated holder list with classification, balance, P/L.
- **GetDetailAsync** *(POST)*: Detailed info for a specific wallet address.
- **GetTrendListAsync**: Holder metrics trend over time (count + top-N holding ratios).
- **GetTagCountAsync**: Holder counts grouped by wallet tags (whale, KOL, smart money, bot, etc.).
- **GetCountAsync**: Total holder count for a token.

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

### v2.5.0

- Added `ICoinMarketCapAPI` interface for DI and test-double scenarios. `CoinMarketCapAPI` implements it. Additive — consumers using the concrete type continue to work unchanged.
- Updated README endpoint reference tables to cover every method added since v2.0.0 (Fear & Greed, CMC Index, v3 cryptocurrency methods, and the entire DEX section with its 5 sub-groups).
- Added a Retries and rate-limit backoff section to the README showing how to layer Polly onto an injected `HttpClient`.
- Added XML documentation comments to all `CoinMarketCapOptions` properties.
- Fixed the `Accepts` request header typo (now correctly `Accept`). No behavioral change — CoinMarketCap's servers ignored the previous header, but the corrected name is standards-compliant.
- Gitignored local build artifacts (`/artifacts/`, `build_output.txt`, `bash.exe.stackdump`).

### v2.4.0

- Added the final DEX sub-group: `api.Dex.Holders.*` (5 endpoints).
  - `GetListAsync` (POST) — paginated holder list with classification, balance, P/L.
  - `GetDetailAsync` (POST) — detailed info for a specific wallet address.
  - `GetTrendListAsync` (GET) — holder metrics over time (count + top-N holding ratios).
  - `GetTagCountAsync` (GET) — holder counts grouped by wallet tags (whale, KOL, smart money, bot, etc.).
  - `GetCountAsync` (GET) — total holder count.
- **Tier 3 complete:** The DEX category is now fully wrapped. `api.Dex.{Token,Pairs,Platform,Kline,Holders}` together cover all 22 CMC DEX endpoints shipped across v2.2.0 → v2.4.0.
- Fully additive release — no breaking changes from v2.3.0.

### v2.3.0

- Expanded the DEX endpoint group with three new sub-groups:
  - `api.Dex.Pairs.*` (2 endpoints) — `GetSpotPairsLatestAsync`, `GetQuotesLatestAsync` for DEX trading pair data.
  - `api.Dex.Platform.*` (2 endpoints) — `GetListAsync`, `GetDetailAsync` for blockchain network metadata.
  - `api.Dex.Kline.*` (2 endpoints) — `GetPointsAsync`, `GetCandlesAsync` for DEX OHLCV chart data.
- Fully additive release — no breaking changes from v2.2.0.
- DEX Holders endpoints are deferred to v2.4.0.

### v2.2.0

- Added DEX endpoint group with Token sub-group: 14 new methods under `api.Dex.Token.*` (trending, batch query, batch price, new list, meme list, gainer/loser, token detail, price, pools, liquidity, transactions, security, search, liquidity change).
- Added public `PostDataAsync<T>(endpoint, body, cancellationToken)` transport method on `CoinMarketCapAPI` for POST endpoints.
- Refactored shared response handling into a private `HandleResponseAsync<T>` helper used by both `GetDataAsync` and `PostDataAsync`. No behavior changes for existing GET endpoints.
- Fully additive release — no breaking changes from v2.1.0.
- DEX Pairs, Holders, Platform, and K-line endpoints are deferred to v2.3.0 / v2.4.0.

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

If this library saves you time, a tip in USDT (TRC20, on Tron) is appreciated:

`TGgenJpoPvTFsd61hCUquyM4yQ9mAmD9hc`

<img src="https://github.com/msanlisavas/CoinMarketCapDotNet/raw/master/assets/usdt-tron-qr.jpg" alt="USDT (TRC20) QR code" width="220" />
