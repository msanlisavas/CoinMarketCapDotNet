# Release Notes
v.1.0.3
- Fixed an issue where the latest quotes sometimes returns with null data.
v.1.0.2
- Fixed json serialization issues on latest quotes.
v.1.0.1
- Added xml comments.
v.1.0.0
- Initial release.

# CoinMarketCap C# Wrapper

# CoinMarketCapDotNet

CoinMarketCapDotNet is a C# wrapper for the CoinMarketCap API, providing convenient access to cryptocurrency market data.

## Installation

You can install CoinMarketCapDotNet via NuGet Package Manager or by using the .NET CLI:

### NuGet Package Manager

Search for `CoinMarketCapDotNet` in the NuGet Package Manager UI or run the following command in the Package Manager Console:

Install-Package CoinMarketCapDotNet

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
## Usage

To use CoinMarketCapDotNet, follow these steps:

1. Instantiate the `CoinMarketCapAPI` class with your API key:

```csharp
string apiKey = "YOUR_API_KEY";
CoinMarketCapAPI api = new CoinMarketCapAPI(apiKey);

To use sandbox mode:
string apiKey = "YOUR_API_KEY";
CoinMarketCapAPI api = new CoinMarketCapAPI(apiKey,true);
```

Access the available endpoints through the instantiated API object:
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

```
For example, to get metadata:

var data = await api.Cryptocurrency.GetMapAsync();  // Retrieves a mapping of all supported fiat currencies to unique CoinMarketCap IDs.
```

**Extensions to make life easier:**
Enum Extensions:

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

Contributing
Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

License
MIT License

**Buy me a coffee?**
---
```
BTC: 1NxUuEQcR4Scw8ge3oto6ykLqBpe9LGikS
ETH: 0x9cda155f73220073a9f024daaa72eb06b5c06c86
```


