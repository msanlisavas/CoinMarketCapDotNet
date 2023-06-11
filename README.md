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

dotnet add package CoinMarketCapDotNet

## Usage

To use CoinMarketCapDotNet, follow these steps:

1. Instantiate the `CoinMarketCapAPI` class with your API key:

```csharp
string apiKey = "YOUR_API_KEY";
CoinMarketCapAPI api = new CoinMarketCapAPI(apiKey);

To use sandbox mode:
string apiKey = "YOUR_API_KEY";
CoinMarketCapAPI api = new CoinMarketCapAPI(apiKey,true);


Access the available endpoints through the instantiated API object. 
CryptocurrencyEndpoint -> await api.Cryptocurrency.
FiatEndpoint -> await api.Fiat.
ExchangeEndpoint -> await api.Exchange.
GlobalMetricsEndpoint -> await api.GlobalMetrics.
ToolsEndpoint -> await api.Tools.
BlockchainEndpoint -> await api.Blockchain.
KeyEndpoint -> await api.Key.
ContentEndpoint -> await api.Content.
CommunityEndpoint -> await api.Community.

For example, to get metadata:

var data = await api.Cryptocurrency.GetMapAsync();  // Retrieves a mapping of all supported fiat currencies to unique CoinMarketCap IDs.
```
Extensions to make life easier:
```
Enum Extensions: 
**## .GetEnumMemberValue():**
Example: 
CurrencyEnum currency = CurrencyEnum.EUR;
string enumMemberValue = currency.GetEnumMemberValue(); // Should return "USD" you can use this to fetch USD from endpoints.

**## .GetId():**
Example: 
CurrencyEnum currency = CurrencyEnum.USD;
int id = currency.GetId(); // You should get the cmc equilavent of EUR id.

**## .GetSymbol():**
Example: 
CurrencyEnum currency = CurrencyEnum.TRY; // You should get "TRY"
string symbol = currency.GetSymbol();

**## .GetCurrencyIds():**
Example: 
string symbols = "USD,EUR,TRY";
List<int> ids = symbols.GetCurrencyIds(); // Returns the symbol strings as a id list to be used in endpoints.

**## .GetAllIds():**
Example: 
List<int> allIds = EnumExtensions.GetAllIds<CategoryEnum>(); // Will return all the ids of a given enum

**## .GetAllSymbols():**
Example: 
  List<string> enumMemberValues = EnumExtensions.GetAllSymbols<CurrencyEnum>(); // Will return all the symbols of a given enum

```
Refer to the CoinMarketCap API documentation for more information on available endpoints and their usage.

Contributing
Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

License
MIT License

