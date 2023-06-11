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


Access the available endpoints through the instantiated API object:

CoinMarketCapAPI
Methods:
GetDataAsync
Description: Retrieves data asynchronously.
CryptocurrencyEndpoint
Description: Provides endpoints related to cryptocurrencies.
FiatEndpoint
Description: Provides endpoints related to fiat currencies.
ExchangeEndpoint
Description: Provides endpoints related to exchanges.
GlobalMetricsEndpoint
Description: Provides endpoints related to global market metrics.
ToolsEndpoint
Description: Provides miscellaneous tools and utilities.
BlockchainEndpoint
Description: Provides endpoints related to blockchain statistics.
KeyEndpoint
Description: Provides endpoints related to API keys.
ContentEndpoint
Description: Provides endpoints related to content (news, articles, etc.).
CommunityEndpoint
Description: Provides endpoints related to the community.
CryptocurrencyEndpoint
Methods:
GetAirdropAsync
Description: Retrieves airdrop details asynchronously.
GetAirdropsAsync
Description: Retrieves a list of airdrops asynchronously.
GetCategoriesAsync
Description: Retrieves a list of cryptocurrency categories asynchronously.
GetCategoryAsync
Description: Retrieves category details asynchronously.
GetMapAsync
Description: Retrieves a mapping of all cryptocurrencies to unique CoinMarketCap IDs asynchronously.
GetInfoAsync
Description: Retrieves cryptocurrency information asynchronously.
GetListingLatestAsync
Description: Retrieves the latest cryptocurrency listings asynchronously.
GetListingHistoricalAsync
Description: Retrieves historical cryptocurrency listings asynchronously.
GetListingNewAsync
Description: Retrieves new cryptocurrency listings asynchronously.
GetTrendingGainersLosersAsync
Description: Retrieves trending gainers and losers asynchronously.
GetTrendingLatestAsync
Description: Retrieves the latest trending cryptocurrencies asynchronously.
GetTrendingMostVisitedAsync
Description: Retrieves the most visited trending cryptocurrencies asynchronously.
GetMarketPairsLatestAsync
Description: Retrieves the latest cryptocurrency market pairs asynchronously.
GetOHLCVLatestAsync
Description: Retrieves the latest cryptocurrency OHLCV (Open, High, Low, Close, Volume) data asynchronously.
GetOHCLVHistoricalAsync
Description: Retrieves historical cryptocurrency OHLCV (Open, High, Low, Close, Volume) data asynchronously.
GetPricePerformanceStatsLatestAsync
Description: Retrieves the latest cryptocurrency price performance statistics asynchronously.
GetQuotesHistoricalV2Async
Description: Retrieves historical cryptocurrency quotes using V2 endpoint asynchronously.
GetQuotesLatestAsync
Description: Retrieves the latest cryptocurrency quotes asynchronously.
GetQuotesHistoricalV3Async
Description: Retrieves historical cryptocurrency quotes using V3 endpoint asynchronously.
FiatEndpoint
Methods:
GetMapAsync
Description: Retrieves a mapping of all fiat currencies to unique CoinMarketCap IDs asynchronously.
ExchangeEndpoint
Methods:
GetAssetsAsync
Description: Retrieves a list of exchange assets asynchronously.
GetInfoAsync
Description: Retrieves exchange information asynchronously.
GetMapAsync
Description: Retrieves a mapping of all exchanges to unique CoinMarketCap IDs asynchronously.
GetListingLatestAsync
Description: Retrieves the latest exchange listings asynchronously.
GetMarketPairsAsync
Description: Retrieves exchange market pairs asynchronously.
GetQuotesHistoricalAsync
Description: Retrieves historical exchange quotes asynchronously.
GetQuotesLatestAsync
Description: Retrieves the latest exchange quotes asynchronously.

For example, to get metadata:

var data = await api.Cryptocurrency.GetMapAsync();  // Retrieves a mapping of all supported fiat currencies to unique CoinMarketCap IDs.
```

**Extensions to make life easier:**
Enum Extensions:

.GetEnumMemberValue():
Example:
```
CurrencyEnum currency = CurrencyEnum.EUR;
string enumMemberValue = currency.GetEnumMemberValue(); // Should return "USD" you can use this to fetch USD from endpoints.
```

.GetId():
Example:
```
CurrencyEnum currency = CurrencyEnum.USD;
int id = currency.GetId(); // You should get the cmc equivalent of EUR id.
```

.GetSymbol():
Example:
```
CurrencyEnum currency = CurrencyEnum.TRY; // You should get "TRY"
string symbol = currency.GetSymbol();
```

.GetCurrencyIds():
Example:
```
string symbols = "USD,EUR,TRY";
List<int> ids = symbols.GetCurrencyIds(); // Returns the symbol strings as a id list to be used in endpoints.
```

.GetAllIds():
Example:
```
List<int> allIds = EnumExtensions.GetAllIds<CategoryEnum>(); // Will return all the ids of a given enum
```

.GetAllSymbols():
Example:
```
List<string> enumMemberValues = EnumExtensions.GetAllSymbols<CurrencyEnum>(); // Will return all the symbols of a given enum
```
  
Refer to the CoinMarketCap API documentation for more information on available endpoints and their usage.

Contributing
Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

License
MIT License

