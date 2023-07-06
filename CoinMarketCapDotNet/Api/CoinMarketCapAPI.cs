using CoinMarketCapDotNet.Api.General;
using CoinMarketCapDotNet.Extensions;
using CoinMarketCapDotNet.Models.Blockchain.Statistics;
using CoinMarketCapDotNet.Models.Blockchain.Statistics.Query;
using CoinMarketCapDotNet.Models.Community.Trending;
using CoinMarketCapDotNet.Models.Community.Trending.Query;
using CoinMarketCapDotNet.Models.Content.Latest;
using CoinMarketCapDotNet.Models.Content.Latest.Query;
using CoinMarketCapDotNet.Models.Content.Posts;
using CoinMarketCapDotNet.Models.Content.Posts.Comments;
using CoinMarketCapDotNet.Models.Content.Posts.Comments.Query;
using CoinMarketCapDotNet.Models.Content.Posts.Latest.Query;
using CoinMarketCapDotNet.Models.Content.Posts.Top.Query;
using CoinMarketCapDotNet.Models.Cryptocurrency.Airdrops;
using CoinMarketCapDotNet.Models.Cryptocurrency.Airdrops.Query;
using CoinMarketCapDotNet.Models.Cryptocurrency.Categories.Query;
using CoinMarketCapDotNet.Models.Cryptocurrency.Category;
using CoinMarketCapDotNet.Models.Cryptocurrency.Category.Query;
using CoinMarketCapDotNet.Models.Cryptocurrency.Info;
using CoinMarketCapDotNet.Models.Cryptocurrency.Info.Query;
using CoinMarketCapDotNet.Models.Cryptocurrency.Listing.Historical;
using CoinMarketCapDotNet.Models.Cryptocurrency.Listing.Latest;
using CoinMarketCapDotNet.Models.Cryptocurrency.Listing.New;
using CoinMarketCapDotNet.Models.Cryptocurrency.Listing.Query;
using CoinMarketCapDotNet.Models.Cryptocurrency.Map;
using CoinMarketCapDotNet.Models.Cryptocurrency.Map.Query;
using CoinMarketCapDotNet.Models.Cryptocurrency.MarketPairs.Latest;
using CoinMarketCapDotNet.Models.Cryptocurrency.MarketPairs.Query;
using CoinMarketCapDotNet.Models.Cryptocurrency.Ohlcv.Historical;
using CoinMarketCapDotNet.Models.Cryptocurrency.Ohlcv.Latest;
using CoinMarketCapDotNet.Models.Cryptocurrency.Ohlcv.Query;
using CoinMarketCapDotNet.Models.Cryptocurrency.PricePerformanceStats.Latest;
using CoinMarketCapDotNet.Models.Cryptocurrency.PricePerformanceStats.Query;
using CoinMarketCapDotNet.Models.Cryptocurrency.Quotes.Historical;
using CoinMarketCapDotNet.Models.Cryptocurrency.Quotes.Latest;
using CoinMarketCapDotNet.Models.Cryptocurrency.Quotes.Query;
using CoinMarketCapDotNet.Models.Cryptocurrency.Trending.GainersLosers;
using CoinMarketCapDotNet.Models.Cryptocurrency.Trending.Latest;
using CoinMarketCapDotNet.Models.Cryptocurrency.Trending.MostVisited;
using CoinMarketCapDotNet.Models.Cryptocurrency.Trending.Query;
using CoinMarketCapDotNet.Models.Enums;
using CoinMarketCapDotNet.Models.Exchange.Assets;
using CoinMarketCapDotNet.Models.Exchange.Assets.Query;
using CoinMarketCapDotNet.Models.Exchange.Info;
using CoinMarketCapDotNet.Models.Exchange.Listing.Latest;
using CoinMarketCapDotNet.Models.Exchange.Listing.Latest.Query;
using CoinMarketCapDotNet.Models.Exchange.Map;
using CoinMarketCapDotNet.Models.Exchange.Map.Query;
using CoinMarketCapDotNet.Models.Exchange.MarketPairs.Latest;
using CoinMarketCapDotNet.Models.Exchange.Quotes.Historical;
using CoinMarketCapDotNet.Models.Exchange.Quotes.Historical.Query;
using CoinMarketCapDotNet.Models.Exchange.Quotes.Latest;
using CoinMarketCapDotNet.Models.Exchange.Quotes.Latest.Query;
using CoinMarketCapDotNet.Models.Fiat.Map;
using CoinMarketCapDotNet.Models.Fiat.Map.Query;
using CoinMarketCapDotNet.Models.General;
using CoinMarketCapDotNet.Models.GlobalMetrics.Historical;
using CoinMarketCapDotNet.Models.GlobalMetrics.Historical.Query;
using CoinMarketCapDotNet.Models.GlobalMetrics.Latest;
using CoinMarketCapDotNet.Models.Key;
using CoinMarketCapDotNet.Models.Tools;
using CoinMarketCapDotNet.Models.Tools.Query;
using CoinMarketCapSdk.Models.Cryptocurrency.Categories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoinMarketCapDotNet.Api
{
    public class CoinMarketCapAPI
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly string apiKey;
        private readonly string apiBase;

        public CoinMarketCapAPI(string apiKey, bool useSandbox = false)
        {
            this.apiKey = apiKey;
            this.apiBase = useSandbox ? "https://sandbox-api.coinmarketcap.com/"
                                      : "https://pro-api.coinmarketcap.com/";

            Cryptocurrency = new CryptocurrencyEndpoint(this); // Initialize Cryptocurrency instance
            Fiat = new FiatEndpoint(this); // Initialize Fiat instance
            Exchange = new ExchangeEndpoint(this); // Initialize Exchange instance
            GlobalMetrics = new GlobalMetricsEndpoint(this); // Initialize GlobalMetrics instance
            Tools = new ToolsEndpoint(this); // Initialize Tools instance
            Blockchain = new BlockchainEndpoint(this); // Initialize Blockchain instance
            Key = new KeyEndpoint(this); // Initialize Key instance
            Content = new ContentEndpoint(this); // Initialize Content instance
            Community = new CommunityEndpoint(this); // Initialize Community instance
        }

        public async Task<T> GetDataAsync<T>(string endpoint) where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{apiBase}{endpoint}");
            request.Headers.Add("X-CMC_PRO_API_KEY", apiKey);
            request.Headers.Add("Accepts", "application/json");

            var response = await client.SendAsync(request);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var content = await response.Content.ReadAsStringAsync();
                    T result = JsonConvert.DeserializeObject<T>(content);
                    return result ?? throw new Exception("Failed to deserialize response content.");
                case HttpStatusCode.BadRequest:
                    var badRequestContent = await response.Content.ReadAsStringAsync();
                    var badRequestStatus = JsonConvert.DeserializeObject<ResponseDict<Status>>(badRequestContent);
                    throw new Exception($"Bad request: {badRequestStatus.Status.ErrorMessage}");
                case HttpStatusCode.Unauthorized:
                    var unauthorizedContent = await response.Content.ReadAsStringAsync();
                    var unauthorizedStatus = JsonConvert.DeserializeObject<ResponseDict<Status>>(unauthorizedContent);
                    throw new Exception($"Unauthorized: {unauthorizedStatus.Status.ErrorMessage}");
                case HttpStatusCode.Forbidden:
                    var forbiddenContent = await response.Content.ReadAsStringAsync();
                    var forbiddenStatus = JsonConvert.DeserializeObject<ResponseDict<Status>>(forbiddenContent);
                    throw new Exception($"Forbidden: {forbiddenStatus.Status.ErrorMessage}");
                case HttpStatusCode.InternalServerError:
                    var internalServerErrorContent = await response.Content.ReadAsStringAsync();
                    var internalServerErrorStatus = JsonConvert.DeserializeObject<ResponseDict<Status>>(internalServerErrorContent);
                    throw new Exception($"Internal Server Error: {internalServerErrorStatus.Status.ErrorMessage}");
                default:
                    throw new Exception($"Error: {response.StatusCode}");
            }
        }


        public CryptocurrencyEndpoint Cryptocurrency { get; } // Instance of Cryptocurrency class
        public FiatEndpoint Fiat { get; } // Instance of Fiat class
        public ExchangeEndpoint Exchange { get; } // Instance of Exchange class
        public GlobalMetricsEndpoint GlobalMetrics { get; } // Instance of GlobalMetrics class
        public ToolsEndpoint Tools { get; } // Instance of Tools class
        public BlockchainEndpoint Blockchain { get; } // Instance of Blockchain class
        public KeyEndpoint Key { get; } // Instance of Key class
        public ContentEndpoint Content { get; } // Instance of Content class
        public CommunityEndpoint Community { get; } // Instance of Community class

        public class CryptocurrencyEndpoint
        {
            private readonly CoinMarketCapAPI coinMarketCapAPI;

            public CryptocurrencyEndpoint(CoinMarketCapAPI coinMarketCapAPI)
            {
                this.coinMarketCapAPI = coinMarketCapAPI;
            }
            /// <summary>
            /// Retrieves information about a single airdrop available on CoinMarketCap. Includes the cryptocurrency metadata.
            /// </summary>
            /// <param name="id">Airdrop Unique ID. This can be found using the Airdrops API.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>
            /// A response object containing the airdrop information and associated cryptocurrency metadata.
            /// </returns>
            /// <remarks>
            /// Cache / Update frequency: Data is updated only as needed, every 30 seconds.
            /// Plan credit use: 1 API call credit per request no matter query size.
            /// CMC equivalent pages: Our free airdrops page coinmarketcap.com/airdrop/.
            /// </remarks>
            public async Task<Response<AirdropData>> GetAirdropAsync(string id)
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    throw new ArgumentException("'id' must be provided.");
                }
                var parameters = new AirdropsQueryParameters();
                parameters.AddId(id);
                var endpoint = $"{Endpoints.Cryptocurrency.Airdrop}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<Response<AirdropData>>(endpoint);
                var data = response?.Data;
                return new Response<AirdropData>
                {
                    Status = response?.Status,
                    Data = data
                };
            }
            /// <summary>
            /// Retrieves a list of past, present, or future airdrops that have run on CoinMarketCap.
            /// </summary>
            /// <param name="start">Optionally offset the start (1-based index) of the paginated list of items to return. (Default: 1)</param>
            /// <param name="limit">Optionally specify the number of results to return. Use this parameter and the "start" parameter to determine your own pagination size. (Default: 100, Maximum: 5000)</param>
            /// <param name="status">Filter the airdrops by status. Possible values: "ONGOING", "ENDED", "UPCOMING".</param>
            /// <param name="id">Filtered airdrops by one cryptocurrency CoinMarketCap IDs. Example: "1"</param>
            /// <param name="slug">Alternatively filter airdrops by a cryptocurrency slug. Example: "bitcoin"</param>
            /// <param name="symbol">Alternatively filter airdrops by a cryptocurrency symbol. Example: "BTC"</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>
            /// A response object containing a list of airdrops matching the specified filters.
            /// </returns>
            /// <remarks>
            /// Cache / Update frequency: Data is updated only as needed, every 30 seconds.
            /// Plan credit use: 1 API call credit per request no matter query size.
            /// CMC equivalent pages: Our free airdrops page coinmarketcap.com/airdrop/.
            /// </remarks>
            public async Task<Response<List<AirdropsData>>> GetAirdropsAsync(int start = 1, int limit = 100, StatusEnum status = StatusEnum.Ongoing, string id = "", string slug = "", string symbol = "")
            {
                var parameters = new AirdropsQueryParameters();
                parameters.AddStart(start);
                parameters.AddLimit(limit);
                parameters.AddStatus(status);
                parameters.AddId(id);
                parameters.AddSlug(slug);
                parameters.AddSymbol(symbol);
                var endpoint = $"{Endpoints.Cryptocurrency.Airdrops}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<NestedResponseList<AirdropsData>>(endpoint);
                var data = response?.Data.Data.ToList();
                return new Response<List<AirdropsData>>
                {
                    Status = response?.Status,
                    Data = data
                };
            }
            /// <summary>
            /// Retrieves information about all coin categories available on CoinMarketCap, including a paginated list of cryptocurrency quotes and metadata from each category.
            /// </summary>
            /// <param name="start">Optionally offset the start (1-based index) of the paginated list of items to return. (Default: 1)</param>
            /// <param name="limit">Optionally specify the number of results to return. Use this parameter and the "start" parameter to determine your own pagination size.</param>
            /// <param name="ids">Filtered categories by one or more comma-separated cryptocurrency CoinMarketCap IDs. Example: "1,2"</param>
            /// <param name="slugs">Alternatively filter categories by a comma-separated list of cryptocurrency slugs. Example: "bitcoin,ethereum"</param>
            /// <param name="symbol">Alternatively filter categories by one or more comma-separated cryptocurrency symbols. Example: "BTC,ETH"</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Free
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>
            /// A response object containing information about all coin categories available on CoinMarketCap, along with the paginated list of cryptocurrency quotes and metadata from each category.
            /// </returns>
            /// <remarks>
            /// Cache / Update frequency: Data is updated only as needed, every 30 seconds.
            /// Plan credit use: 1 API call credit per request + 1 call credit per 200 cryptocurrencies returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: Our free coin categories page coinmarketcap.com/cryptocurrency-category/.
            /// </remarks>
            public async Task<Response<List<CategoriesData>>> GetCategoriesAsync(int start = 1, int limit = 100, string ids = "", string slugs = "", string symbol = "")
            {
                var parameters = new CategoriesQueryParameters();
                parameters.AddId(ids);
                parameters.AddStart(start);
                parameters.AddLimit(limit);
                parameters.AddSlug(slugs);
                parameters.AddSymbol(symbol);

                var endpoint = $"{Endpoints.Cryptocurrency.Categories}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<NestedResponseList<CategoriesData>>(endpoint);

                var data = response?.Data.Data.ToList();
                return new Response<List<CategoriesData>>
                {
                    Status = response?.Status,
                    Data = data
                };
            }
            /// <summary>
            /// Retrieves information about a single coin category available on CoinMarketCap, including a paginated list of cryptocurrency quotes and metadata for the category.
            /// </summary>
            /// <param name="id">The Category ID. This can be found using the Categories API.</param>
            /// <param name="start">Optionally offset the start (1-based index) of the paginated list of coins to return. (Default: 1)</param>
            /// <param name="limit">Optionally specify the number of coins to return. Use this parameter and the "start" parameter to determine your own pagination size.</param>
            /// <param name="convert">Optionally calculate market quotes in up to 120 currencies at once by passing a comma-separated list of cryptocurrency or fiat currency symbols. Each additional convert option beyond the first requires an additional call credit. A list of supported fiat options can be found here. Each conversion is returned in its own "quote" object.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol. This option is identical to convert outside of ID format. Ex: convert_id=1,2781 would replace convert=BTC,USD in your query. This parameter cannot be used when convert is used.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Free
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>
            /// A response object containing information about the single coin category available on CoinMarketCap, including the paginated list of cryptocurrency quotes and metadata for the category.
            /// </returns>
            /// <remarks>
            /// Cache / Update frequency: Data is updated only as needed, every 30 seconds.
            /// Plan credit use: 1 API call credit per request + 1 call credit per 200 cryptocurrencies returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: Our Cryptocurrency Category page coinmarketcap.com/cryptocurrency-category/.
            /// </remarks>
            public async Task<Response<List<CategoryData>>> GetCategoryAsync(string id, int start = 1, int limit = 100, string convert = "", string convertId = "")
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    throw new ArgumentException("'id' must be provided.");
                }
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new CategoryQueryParameters();
                parameters.AddId(id);
                parameters.AddStart(start);
                parameters.AddLimit(limit);
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);

                var endpoint = $"{Endpoints.Cryptocurrency.Category}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseDict<CategoryData>>(endpoint);

                var data = response?.Data?.Values?.ToList();
                return new Response<List<CategoryData>>
                {
                    Status = response?.Status,
                    Data = data
                };
            }


            /// <summary>
            /// Retrieves a mapping of all cryptocurrencies to unique CoinMarketCap IDs. Includes typical identifiers such as name, symbol, and token address for flexible mapping to ID.
            /// </summary>
            /// <param name="listingStatus">Specifies the listing status of cryptocurrencies to be returned. By default, only active cryptocurrencies are returned. Pass "inactive" to get a list of inactive cryptocurrencies, or "untracked" to get a list of cryptocurrencies that are listed but do not yet meet methodology requirements to have tracked markets available. You may pass one or more comma-separated values. (Default: "active")</param>
            /// <param name="start">Optionally offset the start (1-based index) of the paginated list of items to return. (Default: 1)</param>
            /// <param name="limit">Optionally specify the number of results to return. Use this parameter and the "start" parameter to determine your own pagination size. (Default: 100)</param>
            /// <param name="sort">Specifies the field to sort the list of cryptocurrencies by. (Default: "id")</param>
            /// <param name="symbol">Optionally pass a comma-separated list of cryptocurrency symbols to return CoinMarketCap IDs for. If this option is passed, other options will be ignored.</param>
            /// <param name="aux">Optionally specify a comma-separated list of supplemental data fields to return. Pass "platform,first_historical_data,last_historical_data,is_active,status" to include all auxiliary fields.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing a mapping of all cryptocurrencies to unique CoinMarketCap IDs, including typical identifiers and auxiliary fields.</returns>
            /// <remarks>
            /// Cache / Update frequency: Mapping data is updated only as needed, every 30 seconds.
            /// Plan credit use: 1 API call credit per request no matter query size.
            /// CMC equivalent pages: No equivalent, this data is only available via API.
            /// </remarks>
            public async Task<Response<List<MapData>>> GetMapAsync(string listingStatus = "active", int start = 1, int limit = 5000, SortMapEnum sort = SortMapEnum.Id, string symbol = "", string aux = "")
            {
                var parameters = new MapQueryParameters();
                parameters.Add("start", start);
                parameters.Add("limit", limit);
                parameters.Add("symbol", symbol);
                parameters.Add("listing_status", listingStatus);
                parameters.Add("sort", sort.GetEnumMemberValue());
                parameters.Add("aux", aux);

                var endpoint = $"{Endpoints.Cryptocurrency.Map}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseList<MapData>>(endpoint);
                var data = response?.Data.ToList();
                return new Response<List<MapData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Retrieves static metadata available for one or more cryptocurrencies. This information includes details like logo, description, official website URL, social links, and links to a cryptocurrency's technical documentation.
            /// </summary>
            /// <param name="ids">One or more comma-separated CoinMarketCap cryptocurrency IDs. Example: "1,2"</param>
            /// <param name="slugs">Alternatively, pass a comma-separated list of cryptocurrency slugs. Example: "bitcoin,ethereum"</param>
            /// <param name="symbols">Alternatively, pass one or more comma-separated cryptocurrency symbols. Example: "BTC,ETH". At least one "ids," "slugs," or "symbols" parameter is required for this request.</param>
            /// <param name="address">Alternatively, pass in a contract address. Example: "0xc40af1e4fecfa05ce6bab79dcd8b373d2e436c4e"</param>
            /// <param name="skipInvalid">Pass true to relax request validation rules. When requesting records on multiple cryptocurrencies, an error is returned if any invalid cryptocurrencies are requested or a cryptocurrency does not have matching records in the requested timeframe. If set to true, invalid lookups will be skipped, allowing valid cryptocurrencies to still be returned. (Default: false)</param>
            /// <param name="aux">Optionally specify a comma-separated list of supplemental data fields to return. Pass "urls,logo,description,tags,platform,date_added,notice,status" to include all auxiliary fields.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing static metadata for the requested cryptocurrencies, including details like logo, description, official website URL, social links, and technical documentation links.</returns>
            /// <remarks>
            /// Cache / Update frequency: Static data is updated only as needed, every 30 seconds.
            /// Plan credit use: 1 call credit per 100 cryptocurrencies returned (rounded up).
            /// CMC equivalent pages: Cryptocurrency detail page metadata like coinmarketcap.com/currencies/bitcoin/.
            /// </remarks>
            public async Task<Response<List<List<InfoData>>>> GetInfoAsync(string ids = "", string slugs = "", string symbols = "", string address = "", bool skipInvalid = false, string aux = "")
            {
                if (string.IsNullOrWhiteSpace(ids) && string.IsNullOrWhiteSpace(symbols) && string.IsNullOrWhiteSpace(slugs))
                {
                    throw new ArgumentException("At least one 'id', 'symbol' or 'slug' must be provided.");
                }
                var parameters = new InfoQueryParameters();
                parameters.Add("id", ids);
                parameters.Add("slug", slugs);
                parameters.Add("symbol", symbols);
                parameters.Add("address", address);
                parameters.Add("skip_invalid", skipInvalid.ToString());
                parameters.Add("aux", aux);

                var endpoint = $"{Endpoints.Cryptocurrency.Info}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseDict<List<InfoData>>>(endpoint);
                var data = response?.Data?.Values?.ToList();
                return new Response<List<List<InfoData>>>
                {
                    Status = response?.Status,
                    Data = data
                };
            }
            /// <summary>
            /// Retrieves a paginated list of all active cryptocurrencies with the latest market data. You can sort the list by various market ranking fields and use the "convert" option to return market values in multiple fiat and cryptocurrency conversions in the same call.
            /// </summary>
            /// <param name="start">Optionally offset the start (1-based index) of the paginated list of items to return. (Default: 1)</param>
            /// <param name="limit">Optionally specify the number of results to return. Use this parameter and the "start" parameter to determine your own pagination size. (Default: 100)</param>
            /// <param name="priceMin">Optionally specify a threshold of the minimum USD price to filter results by.</param>
            /// <param name="priceMax">Optionally specify a threshold of the maximum USD price to filter results by.</param>
            /// <param name="marketCapMin">Optionally specify a threshold of the minimum market cap to filter results by.</param>
            /// <param name="marketCapMax">Optionally specify a threshold of the maximum market cap to filter results by.</param>
            /// <param name="volume24hMin">Optionally specify a threshold of the minimum 24-hour USD volume to filter results by.</param>
            /// <param name="volume24hMax">Optionally specify a threshold of the maximum 24-hour USD volume to filter results by.</param>
            /// <param name="circulatingSupplyMin">Optionally specify a threshold of the minimum circulating supply to filter results by.</param>
            /// <param name="circulatingSupplyMax">Optionally specify a threshold of the maximum circulating supply to filter results by.</param>
            /// <param name="percentChange24hMin">Optionally specify a threshold of the minimum 24-hour percent change to filter results by.</param>
            /// <param name="percentChange24hMax">Optionally specify a threshold of the maximum 24-hour percent change to filter results by.</param>
            /// <param name="convert">Optionally calculate market quotes in up to 120 currencies at once by passing a comma-separated list of cryptocurrency or fiat currency symbols. Each additional convert option beyond the first requires an additional call credit.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol. This option is identical to "convert" outside of ID format.</param>
            /// <param name="sort">Specify the field to sort the list of cryptocurrencies by. Valid options are "market_cap", "name", "symbol", "date_added", "price", "circulating_supply", "total_supply", "max_supply", "num_market_pairs", "volume_24h", "percent_change_1h", "percent_change_24h", "percent_change_7d", "market_cap_by_total_supply_strict", "volume_7d", "volume_30d".</param>
            /// <param name="sortDir">Specify the direction in which to order cryptocurrencies against the specified sort. Valid options are "asc" (ascending) and "desc" (descending).</param>
            /// <param name="cryptocurrencyType">Specify the type of cryptocurrency to include. Valid options are "all", "coins", "tokens".</param>
            /// <param name="tag">Specify the tag of cryptocurrency to include. Valid options are "all", "defi", "filesharing".</param>
            /// <param name="aux">Optionally specify a comma-separated list of supplemental data fields to return. Pass "num_market_pairs,cmc_rank,date_added,tags,platform,max_supply,circulating_supply,total_supply,market_cap_by_total_supply,volume_24h_reported,volume_7d,volume_7d_reported,volume_30d,volume_30d_reported,is_market_cap_included_in_calc" to include all auxiliary fields.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing a paginated list of all active cryptocurrencies with the latest market data.</returns>
            /// <remarks>
            /// Cache / Update frequency: Every 60 seconds.
            /// Plan credit use: 1 call credit per 200 cryptocurrencies returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: Our latest cryptocurrency listing and ranking pages like coinmarketcap.com/all/views/all/, coinmarketcap.com/tokens/, coinmarketcap.com/gainers-losers/, coinmarketcap.com/new/.
            /// </remarks>
            public async Task<Response<List<LatestData>>> GetListingLatestAsync
                (
                    int start = 1,
                    int limit = 100,
                    double? priceMin = null,
                    double? priceMax = null,
                    double? marketCapMin = null,
                    double? marketCapMax = null,
                    double? volume24hMin = null,
                    double? volume24hMax = null,
                    double? circulatingSupplyMin = null,
                    double? circulatingSupplyMax = null,
                    double? percentChange24hMin = null,
                    double? percentChange24hMax = null,
                    string convert = null,
                    string convertId = null,
                    SortListingLatestEnum sort = SortListingLatestEnum.MarketCap,
                    SortDirectionEnum sortDir = SortDirectionEnum.Ascending,
                    CryptocurrencyTypeEnum cryptocurrencyType = CryptocurrencyTypeEnum.All,
                    TagEnum tag = TagEnum.All,
                    string aux = null
                )
            {
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new LatestQueryParameters();
                parameters.AddStart(start);
                parameters.AddLimit(limit);
                parameters.AddPriceMin(priceMin);
                parameters.AddPriceMax(priceMax);
                parameters.AddMarketCapMin(marketCapMin);
                parameters.AddMarketCapMax(marketCapMax);
                parameters.AddVolume24hMin(volume24hMin);
                parameters.AddVolume24hMax(volume24hMax);
                parameters.AddCirculatingSupplyMin(circulatingSupplyMin);
                parameters.AddCirculatingSupplyMax(circulatingSupplyMax);
                parameters.AddPercentChange24hMin(percentChange24hMin);
                parameters.AddPercentChange24hMax(percentChange24hMax);
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);
                parameters.AddSort(sort);
                parameters.AddSortDir(sortDir);
                parameters.AddCryptocurrencyType(cryptocurrencyType);
                parameters.AddTag(tag);
                parameters.AddAux(aux);

                var endpoint = $"{Endpoints.Cryptocurrency.Listing.Latest}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseList<LatestData>>(endpoint);
                var data = response?.Data.ToList();
                return new Response<List<LatestData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Retrieves a ranked and sorted list of all cryptocurrencies for a historical UTC date.
            /// </summary>
            /// <param name="date">The date (Unix or ISO 8601) to reference the day of the snapshot. Only the date portion of the timestamp will be referenced. It is recommended to send an ISO date format like "2019-10-10" without time.</param>
            /// <param name="start">Optionally offset the start (1-based index) of the paginated list of items to return. (Default: 1)</param>
            /// <param name="limit">Optionally specify the number of results to return. Use this parameter and the "start" parameter to determine your own pagination size. (Default: 100)</param>
            /// <param name="convert">Optionally calculate market quotes in up to 120 currencies at once by passing a comma-separated list of cryptocurrency or fiat currency symbols. Each additional convert option beyond the first requires an additional call credit.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol. This option is identical to "convert" outside of ID format.</param>
            /// <param name="sort">Specify the field to sort the list of cryptocurrencies by. Valid options are "cmc_rank", "name", "symbol", "market_cap", "price", "circulating_supply", "total_supply", "max_supply", "num_market_pairs", "volume_24h", "percent_change_1h", "percent_change_24h", "percent_change_7d".</param>
            /// <param name="sortDir">Specify the direction in which to order cryptocurrencies against the specified sort. Valid options are "asc" (ascending) and "desc" (descending).</param>
            /// <param name="cryptocurrencyType">Specify the type of cryptocurrency to include. Valid options are "all", "coins", "tokens".</param>
            /// <param name="aux">Optionally specify a comma-separated list of supplemental data fields to return. Pass "platform,tags,date_added,circulating_supply,total_supply,max_supply,cmc_rank,num_market_pairs" to include all auxiliary fields.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist (1 month)
            /// - Startup (1 month)
            /// - Standard (3 months)
            /// - Professional (12 months)
            /// - Enterprise (Up to 6 years)
            /// </remarks>
            /// <returns>A response object containing a ranked and sorted list of all cryptocurrencies for a historical UTC date.</returns>
            /// <remarks>
            /// Cache / Update frequency: The last completed UTC day is available 30 minutes after midnight on the next UTC day.
            /// Plan credit use: 1 call credit per 100 cryptocurrencies returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: Our historical daily crypto ranking snapshot pages like this one on February 02, 2014.
            /// </remarks>
            public async Task<Response<List<HistoricalData>>> GetListingHistoricalAsync
                (
                    DateTime date,
                    int start = 1,
                    int limit = 5000,
                    string convert = "",
                    string convertId = "",
                    SortListingHistoricalEnum sort = SortListingHistoricalEnum.CMCRank,
                    SortDirectionEnum sortDir = SortDirectionEnum.Ascending,
                    CryptocurrencyTypeEnum cryptocurrencyType = CryptocurrencyTypeEnum.All,
                    string aux = ""
                )
            {
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new HistoricalQueryParameters();
                parameters.AddDate(date);
                parameters.AddStart(start);
                parameters.AddLimit(limit);
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);
                parameters.AddSort(sort);
                parameters.AddSortDir(sortDir);
                parameters.AddCryptocurrencyType(cryptocurrencyType);
                parameters.AddAux(aux);

                var endpoint = $"{Endpoints.Cryptocurrency.Listing.Historical}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseList<HistoricalData>>(endpoint);
                var data = response?.Data.ToList();
                return new Response<List<HistoricalData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }

            /// <summary>
            /// Retrieves a paginated list of most recently added cryptocurrencies.
            /// </summary>
            /// <param name="start">Optionally offset the start (1-based index) of the paginated list of items to return. (Default: 1)</param>
            /// <param name="limit">Optionally specify the number of results to return. Use this parameter and the "start" parameter to determine your own pagination size. (Default: 100)</param>
            /// <param name="convert">Optionally calculate market quotes in up to 120 currencies at once by passing a comma-separated list of cryptocurrency or fiat currency symbols. Each additional convert option beyond the first requires an additional call credit.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol. This option is identical to "convert" outside of ID format.</param>
            /// <param name="sortDir">Specify the direction in which to order cryptocurrencies against the specified sort. Valid options are "asc" (ascending) and "desc" (descending).</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing a paginated list of most recently added cryptocurrencies.</returns>
            /// <remarks>
            /// Cache / Update frequency: Every 60 seconds.
            /// Plan credit use: 1 call credit per 200 cryptocurrencies returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: Our "new" cryptocurrency page coinmarketcap.com/new/
            /// </remarks>
            public async Task<Response<List<NewData>>> GetListingNewAsync(int start = 1, int limit = 100, string convert = "", string convertId = "", SortDirectionEnum sortDir = SortDirectionEnum.Ascending)
            {
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new NewQueryParameters();
                parameters.AddStart(start);
                parameters.AddLimit(limit);
                parameters.AddConvert(convert);
                parameters.AddSortDir(sortDir);
                parameters.AddConvertId(convertId);
                var endpoint = $"{Endpoints.Cryptocurrency.Listing.New}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseList<NewData>>(endpoint);
                var data = response?.Data.ToList();
                return new Response<List<NewData>>
                {
                    Status = response?.Status,
                    Data = data
                };


            }
            /// <summary>
            /// Retrieves a paginated list of trending cryptocurrencies, sorted by the largest price gains or losses.
            /// </summary>
            /// <param name="start">Optionally offset the start (1-based index) of the paginated list of items to return. (Default: 1)</param>
            /// <param name="limit">Optionally specify the number of results to return. Use this parameter and the "start" parameter to determine your own pagination size. (Default: 100)</param>
            /// <param name="timePeriod">Adjusts the overall window of time for the biggest gainers and losers. Valid options are "1h," "24h," "30d," and "7d." (Default: "24h")</param>
            /// <param name="convert">Optionally calculate market quotes in up to 120 currencies at once by passing a comma-separated list of cryptocurrency or fiat currency symbols. Each additional convert option beyond the first requires an additional call credit.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol. This option is identical to "convert" outside of ID format.</param>
            /// <param name="sortDir">Specify the direction in which to order cryptocurrencies against the specified sort. Valid options are "asc" (ascending) and "desc" (descending). (Default: "desc")</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing a paginated list of trending cryptocurrencies.</returns>
            /// <remarks>
            /// Cache / Update frequency: Every 10 minutes.
            /// Plan credit use: 1 call credit per 200 cryptocurrencies returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: Our cryptocurrency Gainers & Losers page coinmarketcap.com/gainers-losers/.
            /// </remarks>
            public async Task<Response<List<GainersLosersData>>> GetTrendingGainersLosersAsync
                (
                int start = 1,
                int limit = 100,
                TimePeriodEnum timePeriod = TimePeriodEnum.Daily,
                string convert = "",
                string convertId = "",
                SortTrendingGainersLosersEnum sort = SortTrendingGainersLosersEnum.PercentChange24h,
                SortDirectionEnum sortDir = SortDirectionEnum.Ascending
                )
            {
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new TrendingGainersLosersQueryParameters();
                parameters.AddStart(start);
                parameters.AddLimit(limit);
                parameters.AddTimePeriod(timePeriod);
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);
                parameters.AddSort(sort);
                parameters.AddSortDir(sortDir);

                var endpoint = $"{Endpoints.Cryptocurrency.Trending.GainersLosers}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseList<GainersLosersData>>(endpoint);
                var data = response?.Data?.ToList();
                return new Response<List<GainersLosersData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Retrieves a paginated list of trending cryptocurrency market data, sorted by CoinMarketCap search volume.
            /// </summary>
            /// <param name="start">Optionally offset the start (1-based index) of the paginated list of items to return. (Default: 1)</param>
            /// <param name="limit">Optionally specify the number of results to return. Use this parameter and the "start" parameter to determine your own pagination size. (Default: 100)</param>
            /// <param name="timePeriod">Adjusts the overall window of time for the latest trending coins. Valid options are "24h," "30d," and "7d." (Default: "24h")</param>
            /// <param name="convert">Optionally calculate market quotes in up to 120 currencies at once by passing a comma-separated list of cryptocurrency or fiat currency symbols. Each additional convert option beyond the first requires an additional call credit.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol. This option is identical to "convert" outside of ID format.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing a paginated list of trending cryptocurrency market data.</returns>
            /// <remarks>
            /// Cache / Update frequency: Every 10 minutes.
            /// Plan credit use: 1 call credit per 200 cryptocurrencies returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: Our cryptocurrency Trending page coinmarketcap.com/trending-cryptocurrencies/.
            /// </remarks>
            public async Task<Response<List<TrendingLatestData>>> GetTrendingLatestAsync(int start = 1, int limit = 100, TimePeriodEnum timePeriod = TimePeriodEnum.Daily, string convert = "", string convertId = "")
            {
                if (timePeriod == TimePeriodEnum.Hourly)
                {
                    throw new ArgumentException("Valid values are 'Daily:24h, Monthly:30d, Weekly:7d.");
                }
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new TrendingGainersLosersQueryParameters();
                parameters.AddStart(start);
                parameters.AddLimit(limit);
                parameters.AddTimePeriod(timePeriod);
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);

                var endpoint = $"{Endpoints.Cryptocurrency.Trending.Latest}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseList<TrendingLatestData>>(endpoint);
                var data = response?.Data?.ToList();
                return new Response<List<TrendingLatestData>>
                {
                    Status = response?.Status,
                    Data = data
                };
            }
            /// <summary>
            /// Retrieves a paginated list of trending cryptocurrency market data, sorted by traffic to coin detail pages.
            /// </summary>
            /// <param name="start">Optionally offset the start (1-based index) of the paginated list of items to return. (Default: 1)</param>
            /// <param name="limit">Optionally specify the number of results to return. Use this parameter and the "start" parameter to determine your own pagination size. (Default: 100)</param>
            /// <param name="timePeriod">Adjusts the overall window of time for most visited currencies. Valid options are "24h," "30d," and "7d." (Default: "24h")</param>
            /// <param name="convert">Optionally calculate market quotes in up to 120 currencies at once by passing a comma-separated list of cryptocurrency or fiat currency symbols. Each additional convert option beyond the first requires an additional call credit.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol. This option is identical to "convert" outside of ID format.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing a paginated list of trending cryptocurrency market data.</returns>
            /// <remarks>
            /// Cache / Update frequency: Every 24 hours.
            /// Plan credit use: 1 call credit per 200 cryptocurrencies returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: The CoinMarketCap “Most Visited” trending list. coinmarketcap.com/most-viewed-pages/.
            /// </remarks>
            public async Task<Response<List<MostVisitedData>>> GetTrendingMostVisitedAsync(int start = 1, int limit = 100, TimePeriodEnum timePeriod = TimePeriodEnum.Daily, string convert = "", string convertId = "")
            {
                if (timePeriod == TimePeriodEnum.Hourly)
                {
                    throw new ArgumentException("Valid values are 'Daily:24h, Monthly:30d, Weekly:7d.");
                }
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new TrendingGainersLosersQueryParameters();
                parameters.AddStart(start);
                parameters.AddLimit(limit);
                parameters.AddTimePeriod(timePeriod);
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);

                var endpoint = $"{Endpoints.Cryptocurrency.Trending.MostVisited}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<NestedResponseList<MostVisitedData>>(endpoint);
                var data = response?.Data?.Data?.ToList();
                return new Response<List<MostVisitedData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Retrieves a paginated list of all active market pairs that CoinMarketCap tracks for a given cryptocurrency or fiat currency.
            /// </summary>
            /// <param name="id">A cryptocurrency or fiat currency by CoinMarketCap ID to list market pairs for. A single cryptocurrency "id", "slug", or "symbol" is required.</param>
            /// <param name="slug">Alternatively, pass a cryptocurrency by slug.</param>
            /// <param name="symbol">Alternatively, pass a cryptocurrency by symbol. Fiat currencies are not supported by this field.</param>
            /// <param name="start">Optionally offset the start (1-based index) of the paginated list of items to return. (Default: 1)</param>
            /// <param name="limit">Optionally specify the number of results to return. Use this parameter and the "start" parameter to determine your own pagination size. (Default: 100)</param>
            /// <param name="sortDir">Optionally specify the sort direction of markets returned. Valid options are "asc" and "desc". (Default: "desc")</param>
            /// <param name="sort">Optionally specify the sort order of markets returned. Valid options are "volume_24h_strict", "cmc_rank", "cmc_rank_advanced", "effective_liquidity", "market_score", and "market_reputation". (Default: "volume_24h_strict")</param>
            /// <param name="aux">Optionally specify a comma-separated list of supplemental data fields to return. Valid options are "num_market_pairs", "category", and "fee_type". (Default: "num_market_pairs")</param>
            /// <param name="matchedId">Optionally include one or more fiat or cryptocurrency IDs to filter market pairs by. This parameter cannot be used when matchedSymbol is used.</param>
            /// <param name="matchedSymbol">Optionally include one or more fiat or cryptocurrency symbols to filter market pairs by. This parameter cannot be used when matchedId is used.</param>
            /// <param name="category">The category of trading this market falls under. Valid options are "all", "spot", "derivatives", "otc", and "perpetual". (Default: "all")</param>
            /// <param name="feeType">The fee type the exchange enforces for this market. Valid options are "all", "percentage", "no-fees", "transactional-mining", and "unknown". (Default: "all")</param>
            /// <param name="convert">Optionally calculate market quotes in up to 120 currencies at once by passing a comma-separated list of cryptocurrency or fiat currency symbols. Each additional convert option beyond the first requires an additional call credit.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol. This option is identical to "convert" outside of ID format.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing a paginated list of active market pairs and their latest price and volume information.</returns>
            /// <remarks>
            /// Cache / Update frequency: Every 1 minute.
            /// Plan credit use: 1 call credit per 100 market pairs returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: Our active cryptocurrency markets pages like coinmarketcap.com/currencies/bitcoin/#markets.
            /// </remarks>
            public async Task<Response<MarketPairsLatestData>> GetMarketPairsLatestAsync
                (
                string id = "",
                string slug = "",
                string symbol = "",
                int start = 1,
                int limit = 100,
                SortDirectionEnum sortDir = SortDirectionEnum.Descending,
                SortMarketPairsLatestEnum sort = SortMarketPairsLatestEnum.Volume24hStrict,
                string aux = "",
                string matchedId = "",
                string matchedSymbol = "",
                CategoryEnum category = CategoryEnum.All,
                FeeTypeEnum feeType = FeeTypeEnum.All,
                string convert = "",
                string convertId = ""
                )
            {
                if (string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(symbol) && string.IsNullOrWhiteSpace(slug))
                {
                    throw new ArgumentException("At least one 'id', 'symbol' or 'slug' must be provided.");
                }
                if (!string.IsNullOrWhiteSpace(matchedId) && !string.IsNullOrWhiteSpace(matchedSymbol))
                {
                    throw new ArgumentException("matched_id and matched_symbol cannot be used together.");
                }
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new MarketPairsQueryParameters();
                parameters.AddId(id);
                parameters.AddSlug(slug);
                parameters.AddSymbol(symbol);
                parameters.AddStart(start);
                parameters.AddLimit(limit);
                parameters.AddSortDir(sortDir);
                parameters.AddSort(sort);
                parameters.AddAux(aux);
                parameters.AddMatchedId(matchedId);
                parameters.AddMatchedSymbol(matchedSymbol);
                parameters.AddCategory(category);
                parameters.AddFeeType(feeType);
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);

                var endpoint = $"{Endpoints.Cryptocurrency.MarketPairs.Latest}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<Response<MarketPairsLatestData>>(endpoint);
                var data = response?.Data;
                return new Response<MarketPairsLatestData>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Retrieves the latest OHLCV (Open, High, Low, Close, Volume) market values for one or more cryptocurrencies for the current UTC day.
            /// </summary>
            /// <param name="ids">One or more comma-separated cryptocurrency CoinMarketCap IDs. At least one "id" or "symbol" is required.</param>
            /// <param name="symbols">Alternatively, pass one or more comma-separated cryptocurrency symbols.</param>
            /// <param name="convert">Optionally calculate market quotes in up to 120 currencies at once by passing a comma-separated list of cryptocurrency or fiat currency symbols. Each additional convert option beyond the first requires an additional call credit.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol. This option is identical to "convert" outside of ID format.</param>
            /// <param name="skipInvalid">Pass true to relax request validation rules. If set to true, invalid lookups will be skipped allowing valid cryptocurrencies to still be returned. (Default: true)</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing the latest OHLCV market values for the requested cryptocurrencies.</returns>
            /// <remarks>
            /// Cache / Update frequency: Every 10 minutes. Additional OHLCV intervals and 1 minute updates will be available in the future.
            /// Plan credit use: 1 call credit per 100 OHLCV values returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: No equivalent, this data is only available via API.
            /// </remarks>
            public async Task<Response<List<OHLCVLatestData>>> GetOHLCVLatestAsync(string ids = "", string symbols = "", string convert = "", string convertId = "", bool skipInvalid = true)
            {
                if (string.IsNullOrWhiteSpace(ids) && string.IsNullOrWhiteSpace(symbols))
                {
                    throw new ArgumentException("At least one 'id' or 'symbol' must be provided.");
                }
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new OHLCVLatestQueryParameters();
                parameters.AddIds(ids);
                parameters.AddSymbols(symbols);
                parameters.AddConvert(convert);
                parameters.AddConvertIds(convertId);
                parameters.AddSkipInvalid(skipInvalid);

                var endpoint = $"{Endpoints.Cryptocurrency.OHLCV.Latest}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseDict<OHLCVLatestData>>(endpoint);
                var data = response?.Data?.Values?.ToList();
                return new Response<List<OHLCVLatestData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Retrieves historical OHLCV (Open, High, Low, Close, Volume) data along with market cap for one or more cryptocurrencies using time interval parameters.
            /// </summary>
            /// <param name="id">One or more comma-separated cryptocurrency CoinMarketCap IDs.</param>
            /// <param name="slug">Alternatively, pass a comma-separated list of cryptocurrency slugs.</param>
            /// <param name="symbol">Alternatively, pass one or more comma-separated cryptocurrency symbols.</param>
            /// <param name="timePeriod">Time period to return OHLCV data for. The default is "daily".</param>
            /// <param name="timeStart">Timestamp (Unix or ISO 8601) to start returning OHLCV time periods for. Only the date portion of the timestamp is used for daily OHLCV.</param>
            /// <param name="timeEnd">Timestamp (Unix or ISO 8601) to stop returning OHLCV time periods for (inclusive). Only the date portion of the timestamp is used for daily OHLCV.</param>
            /// <param name="count">Optionally limit the number of time periods to return results for. The default is 10 items.</param>
            /// <param name="interval">Optionally adjust the interval that "timePeriod" is sampled.</param>
            /// <param name="convert">By default, market quotes are returned in USD. Optionally calculate market quotes in up to 3 fiat currencies or cryptocurrencies.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol.</param>
            /// <param name="skipInvalid">Pass true to relax request validation rules. If set to true, invalid lookups will be skipped allowing valid cryptocurrencies to still be returned. (Default: true)</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist
            /// - Startup (1 month)
            /// - Standard (3 months)
            /// - Professional (12 months)
            /// - Enterprise (Up to 6 years)
            /// </remarks>
            /// <returns>A response object containing historical OHLCV data for the requested cryptocurrencies.</returns>
            /// <remarks>
            /// Cache / Update frequency: Latest Daily OHLCV record is available ~5 to ~10 minutes after each midnight UTC. The latest hourly OHLCV record is available 5 minutes after each UTC hour.
            /// Plan credit use: 1 call credit per 100 OHLCV data points returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: Our historical cryptocurrency data pages like coinmarketcap.com/currencies/bitcoin/historical-data/.
            /// </remarks>
            public async Task<Response<List<OHLCVHistoricalData>>> GetOHCLVHistoricalAsync
                (
                string id = "",
                string slug = "",
                string symbol = "",
                TimePeriodOHLCVEnum timePeriod = TimePeriodOHLCVEnum.Daily,
                DateTime? timeStart = null,
                DateTime? timeEnd = null,
                int count = 10,
                IntervalEnum interval = IntervalEnum.Daily,
                string convert = "",
                string convertId = "",
                bool skipInvalid = false
                )
            {
                if (string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(symbol) && string.IsNullOrWhiteSpace(slug))
                {
                    throw new ArgumentException("At least one 'id', 'symbol' or 'slug' must be provided.");
                }
                if (count < 1 || count > 10000)
                {
                    throw new ArgumentException("Count cannot be less than 1 or cannot be greater than 10000");
                }
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new OHLCVHistoricalQueryParameters();
                parameters.AddId(id);
                parameters.AddSlug(slug);
                parameters.AddSymbol(symbol);
                parameters.AddTimePeriod(timePeriod);
                parameters.AddTimeStart(timeStart);
                parameters.AddTimeEnd(timeEnd);
                parameters.AddCount(count);
                parameters.AddInterval(interval);
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);
                parameters.AddSkipInvalid(skipInvalid);

                var endpoint = $"{Endpoints.Cryptocurrency.OHLCV.Historical}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseDict<OHLCVHistoricalData>>(endpoint);
                var data = response?.Data?.Values?.ToList();
                return new Response<List<OHLCVHistoricalData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Retrieves price performance statistics for one or more cryptocurrencies including launch price ROI and all-time high / all-time low.
            /// </summary>
            /// <param name="id">One or more comma-separated cryptocurrency CoinMarketCap IDs.</param>
            /// <param name="slug">Alternatively, pass a comma-separated list of cryptocurrency slugs.</param>
            /// <param name="symbol">Alternatively, pass one or more comma-separated cryptocurrency symbols.</param>
            /// <param name="timePeriods">Specify one or more comma-delimited time periods to return stats for. "all_time" is the default.</param>
            /// <param name="convert">Optionally calculate quotes in up to 120 currencies at once by passing a comma-separated list of cryptocurrency or fiat currency symbols.</param>
            /// <param name="convertId">Optionally calculate quotes by CoinMarketCap ID instead of symbol.</param>
            /// <param name="skipInvalid">Pass true to relax request validation rules. If set to true, invalid lookups will be skipped allowing valid cryptocurrencies to still be returned. (Default: true)</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing price performance statistics for the requested cryptocurrencies.</returns>
            /// <remarks>
            /// Cache / Update frequency: Every 60 seconds.
            /// Plan credit use: 1 call credit per 100 cryptocurrencies returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: The statistics module displayed on cryptocurrency pages like Bitcoin.
            /// </remarks>
            public async Task<Response<List<List<PricePerformanceStatsLatestData>>>> GetPricePerformanceStatsLatestAsync
                (
                    string id = "",
                    string slug = "",
                    string symbol = "",
                    string convert = "",
                    string convertId = "",
                    bool skipInvalid = false,
                    params string[] timePeriods
                )
            {
                if (string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(symbol) && string.IsNullOrWhiteSpace(slug))
                {
                    throw new ArgumentException("At least one 'id', 'symbol' or 'slug' must be provided.");
                }
                var parameters = new PricePerformanceStatsQueryParameters();
                parameters.AddId(id);
                parameters.AddSlug(slug);
                parameters.AddSymbol(symbol);
                parameters.AddTimePeriod(string.Join(",", timePeriods));
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);
                parameters.AddSkipInvalid(skipInvalid);

                var endpoint = $"{Endpoints.Cryptocurrency.PricePerformanceStats.Latest}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseDict<List<PricePerformanceStatsLatestData>>>(endpoint);
                var data = response?.Data?.Values?.ToList();
                return new Response<List<List<PricePerformanceStatsLatestData>>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Retrieves an interval of historic market quotes for any cryptocurrency based on time and interval parameters.
            /// </summary>
            /// <param name="id">One or more comma-separated CoinMarketCap cryptocurrency IDs.</param>
            /// <param name="symbol">Alternatively, pass one or more comma-separated cryptocurrency symbols.</param>
            /// <param name="timeStart">Timestamp (Unix or ISO 8601) to start returning quotes for.</param>
            /// <param name="timeEnd">Timestamp (Unix or ISO 8601) to stop returning quotes for (inclusive).</param>
            /// <param name="count">The number of interval periods to return results for.</param>
            /// <param name="interval">Interval of time to return data points for.</param>
            /// <param name="convert">By default, market quotes are returned in USD. Optionally calculate market quotes in up to 3 other fiat currencies or cryptocurrencies.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol.</param>
            /// <param name="aux">Optionally specify a comma-separated list of supplemental data fields to return.</param>
            /// <param name="skipInvalid">Pass true to relax request validation rules. If set to true, invalid lookups will be skipped allowing valid cryptocurrencies to still be returned. (Default: true)</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist (1 month)
            /// - Startup (1 month)
            /// - Standard (3 month)
            /// - Professional (12 months)
            /// - Enterprise (Up to 6 years)
            /// </remarks>
            /// <returns>A response object containing the historic market quotes for the requested cryptocurrencies.</returns>
            /// <remarks>
            /// Cache / Update frequency: Every 5 minutes.
            /// Plan credit use: 1 call credit per 100 historical data points returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: Our historical cryptocurrency charts like coinmarketcap.com/currencies/bitcoin/#charts.
            /// </remarks>
            public async Task<Response<List<QuotesHistoricalData>>> GetQuotesHistoricalV2Async
                (
                string id = "",
                string symbol = "",
                DateTime? timeStart = null,
                DateTime? timeEnd = null,
                int count = 10,
                IntervalEnum interval = IntervalEnum.FiveMinutes,
                string convert = "",
                string convertId = "",
                string aux = "",
                bool skipInvalid = true
                )
            {
                if (string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(symbol))
                {
                    throw new ArgumentException("At least one 'id', 'symbol' must be provided.");
                }
                if (count < 1 || count > 10000)
                {
                    throw new ArgumentException("Count cannot be less than 1 or cannot be greater than 10000");
                }
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new QuotesHistoricalQueryParameters();
                parameters.AddId(id);
                parameters.AddSymbol(symbol);
                parameters.AddTimeStart(timeStart);
                parameters.AddTimeEnd(timeEnd);
                parameters.AddCount(count);
                parameters.AddInterval(interval);
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);
                parameters.AddSkipInvalid(skipInvalid);
                parameters.AddAux(aux);

                var endpoint = $"{Endpoints.Cryptocurrency.Quotes.HistoricalV2}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseDict<QuotesHistoricalData>>(endpoint);
                var data = response?.Data?.Values?.ToList();
                return new Response<List<QuotesHistoricalData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Retrieves the latest market quote for one or more cryptocurrencies.
            /// </summary>
            /// <param name="id">One or more comma-separated CoinMarketCap cryptocurrency IDs.</param>
            /// <param name="slug">Alternatively, pass a comma-separated list of cryptocurrency slugs.</param>
            /// <param name="symbol">Alternatively, pass one or more comma-separated cryptocurrency symbols.</param>
            /// <param name="convert">Optionally calculate market quotes in up to 120 currencies at once.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol.</param>
            /// <param name="aux">Optionally specify a comma-separated list of supplemental data fields to return.</param>
            /// <param name="skipInvalid">Pass true to relax request validation rules. If set to true, invalid lookups will be skipped allowing valid cryptocurrencies to still be returned. (Default: true)</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Startup
            /// - Hobbyist
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing the latest market quotes for the requested cryptocurrencies.</returns>
            /// <remarks>
            /// Cache / Update frequency: Every 60 seconds.
            /// Plan credit use: 1 call credit per 100 cryptocurrencies returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: Latest market data pages for specific cryptocurrencies like coinmarketcap.com/currencies/bitcoin/.
            /// </remarks>
            public async Task<Response<List<List<QuotesLatestData>>>> GetQuotesLatestAsync(string id = "", string slug = "", string symbol = "", string convert = "", string convertId = "", string aux = "", bool skipInvalid = true)
            {
                if (string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(symbol) && string.IsNullOrWhiteSpace(slug))
                {
                    throw new ArgumentException("At least one 'id', 'symbol' or 'slug' must be provided.");
                }
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new QuotesLatestQueryParameters();
                parameters.AddId(id);
                parameters.AddSymbol(symbol);
                parameters.AddSlug(slug);
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);
                parameters.AddSkipInvalid(skipInvalid);
                parameters.AddAux(aux);

                var endpoint = $"{Endpoints.Cryptocurrency.Quotes.Latest}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseDict<List<QuotesLatestData>>>(endpoint);
                var data = response?.Data?.Values?.ToList();
                return new Response<List<List<QuotesLatestData>>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Retrieves an interval of historic market quotes for any cryptocurrency based on time and interval parameters.
            /// </summary>
            /// <param name="id">One or more comma-separated CoinMarketCap cryptocurrency IDs.</param>
            /// <param name="symbol">Alternatively, pass one or more comma-separated cryptocurrency symbols.</param>
            /// <param name="timeStart">Timestamp (Unix or ISO 8601) to start returning quotes for.</param>
            /// <param name="timeEnd">Timestamp (Unix or ISO 8601) to stop returning quotes for (inclusive).</param>
            /// <param name="count">The number of interval periods to return results for.</param>
            /// <param name="interval">Interval of time to return data points for.</param>
            /// <param name="convert">Optionally calculate market quotes in up to 3 other fiat currencies or cryptocurrencies.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol.</param>
            /// <param name="aux">Optionally specify a comma-separated list of supplemental data fields to return.</param>
            /// <param name="skipInvalid">Pass true to relax request validation rules. If set to true, invalid lookups will be skipped allowing valid cryptocurrencies to still be returned. (Default: true)</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist (1 month)
            /// - Startup (1 month)
            /// - Standard (3 month)
            /// - Professional (12 months)
            /// - Enterprise (Up to 6 years)
            /// </remarks>
            /// <returns>A response object containing the interval of historic market quotes for the requested cryptocurrencies.</returns>
            /// <remarks>
            /// Cache / Update frequency: Every 5 minutes.
            /// Plan credit use: 1 call credit per 100 historical data points returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: Our historical cryptocurrency charts like coinmarketcap.com/currencies/bitcoin/#charts.
            /// </remarks>
            public async Task<ResponseDict<QuotesHistoricalData>> GetQuotesHistoricalV3Async
                (
                string id = "",
                string symbol = "",
                DateTime? timeStart = null,
                DateTime? timeEnd = null,
                int count = 10,
                IntervalEnum interval = IntervalEnum.FiveMinutes,
                string convert = "",
                string convertId = "",
                string aux = "",
                bool skipInvalid = true
                )
            {
                if (string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(symbol))
                {
                    throw new ArgumentException("At least one 'id', 'symbol' must be provided.");
                }
                if (count < 1 || count > 10000)
                {
                    throw new ArgumentException("Count cannot be less than 1 or cannot be greater than 10000");
                }
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new QuotesHistoricalQueryParameters();
                parameters.AddId(id);
                parameters.AddSymbol(symbol);
                parameters.AddTimeStart(timeStart);
                parameters.AddTimeEnd(timeEnd);
                parameters.AddCount(count);
                parameters.AddInterval(interval);
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);
                parameters.AddSkipInvalid(skipInvalid);
                parameters.AddAux(aux);

                var endpoint = $"{Endpoints.Cryptocurrency.Quotes.HistoricalV3}?{parameters}";
                return await coinMarketCapAPI.GetDataAsync<ResponseDict<QuotesHistoricalData>>(endpoint);
            }







        }
        public class FiatEndpoint
        {
            private readonly CoinMarketCapAPI coinMarketCapAPI;

            public FiatEndpoint(CoinMarketCapAPI coinMarketCapAPI)
            {
                this.coinMarketCapAPI = coinMarketCapAPI;
            }
            /// <summary>
            /// Retrieves a mapping of all supported fiat currencies to unique CoinMarketCap IDs.
            /// </summary>
            /// <param name="start">Optionally offset the start (1-based index) of the paginated list of items to return.</param>
            /// <param name="limit">Optionally specify the number of results to return.</param>
            /// <param name="sort">What field to sort the list by.</param>
            /// <param name="includeMetals">Pass true to include precious metals. (Default: false)</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing the mapping of supported fiat currencies to CoinMarketCap IDs.</returns>
            /// <remarks>
            /// Cache / Update frequency: Mapping data is updated only as needed, every 30 seconds.
            /// Plan credit use: 1 API call credit per request no matter query size.
            /// CMC equivalent pages: No equivalent, this data is only available via API.
            /// </remarks>
            public async Task<Response<List<FiatMapData>>> GetMapAsync(int start = 1, int limit = 5000, SortFiatMapEnum sort = SortFiatMapEnum.Id, bool includeMetals = false)
            {
                var parameters = new FiatMapQueryParameters();
                parameters.AddStart(start);
                parameters.AddLimit(limit);
                parameters.AddSort(sort);
                parameters.AddIncludeMetals(includeMetals);

                var endpoint = $"{Endpoints.Fiat.Map}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseList<FiatMapData>>(endpoint);
                var data = response?.Data.ToList();
                return new Response<List<FiatMapData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }


        }
        public class ExchangeEndpoint
        {
            private readonly CoinMarketCapAPI coinMarketCapAPI;

            public ExchangeEndpoint(CoinMarketCapAPI coinMarketCapAPI)
            {
                this.coinMarketCapAPI = coinMarketCapAPI;
            }
            /// <summary>
            /// Retrieves the exchange assets in the form of token holdings.
            /// </summary>
            /// <param name="exchangeId">A CoinMarketCap exchange ID.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Free
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing the exchange assets in the form of token holdings.</returns>
            /// <remarks>
            /// Cache / Update frequency: Balance data is updated statically based on the source. Price data is updated every 5 minutes.
            /// Plan credit use: 1 credit.
            /// CMC equivalent pages: Exchange detail page like coinmarketcap.com/exchanges/binance/
            /// </remarks>
            public async Task<Response<List<List<AssetsData>>>> GetAssetsAsync(string exchangeId)
            {
                var parameters = new AssetsQueryParameters();
                parameters.AddId(exchangeId);

                var endpoint = $"{Endpoints.Exchange.Assets}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseDict<List<AssetsData>>>(endpoint);
                var data = response?.Data.Values.ToList();
                return new Response<List<List<AssetsData>>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Retrieves the static metadata for one or more exchanges.
            /// </summary>
            /// <param name="exchangeIds">One or more comma-separated CoinMarketCap cryptocurrency exchange IDs.</param>
            /// <param name="slugs">Alternatively, one or more comma-separated exchange names in URL-friendly shorthand "slug" format (all lowercase, spaces replaced with hyphens).</param>
            /// <param name="auxiliaryFields">Optionally specify a comma-separated list of supplemental data fields to return.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing the static metadata for the specified exchanges.</returns>
            /// <remarks>
            /// Cache / Update frequency: Static data is updated only as needed, every 30 seconds.
            /// Plan credit use: 1 call credit per 100 exchanges returned (rounded up).
            /// CMC equivalent pages: Exchange detail page metadata like coinmarketcap.com/exchanges/binance/.
            /// </remarks>
            public async Task<Response<List<ExchangeInfoData>>> GetInfoAsync(string exchangeIds = "", string slugs = "", string auxiliaryFields = "")
            {
                if (string.IsNullOrWhiteSpace(exchangeIds) && string.IsNullOrWhiteSpace(slugs))
                {
                    throw new ArgumentException("At least one 'id' or 'slug' must be provided.");
                }
                var parameters = new InfoQueryParameters();
                parameters.Add("id", exchangeIds);
                parameters.Add("slug", slugs);
                parameters.Add("aux", auxiliaryFields);

                var endpoint = $"{Endpoints.Exchange.Info}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseDict<ExchangeInfoData>>(endpoint);
                var data = response?.Data?.Values?.ToList();
                return new Response<List<ExchangeInfoData>>
                {
                    Status = response?.Status,
                    Data = data
                };
            }
            /// <summary>
            /// Retrieves a paginated list of all active cryptocurrency exchanges by CoinMarketCap ID.
            /// </summary>
            /// <param name="listingStatus">Only active exchanges are returned by default. Pass "inactive" to get a list of exchanges that are no longer active. Pass "untracked" to get a list of exchanges that are registered but do not currently meet methodology requirements to have active markets tracked.</param>
            /// <param name="exchangeSlugs">Optionally pass a comma-separated list of exchange slugs (lowercase URL friendly shorthand name with spaces replaced with dashes) to return CoinMarketCap IDs for.</param>
            /// <param name="start">Optionally offset the start (1-based index) of the paginated list of items to return.</param>
            /// <param name="limit">Optionally specify the number of results to return. Use this parameter and the "start" parameter to determine your own pagination size.</param>
            /// <param name="sort">What field to sort the list of exchanges by.</param>
            /// <param name="auxiliaryFields">Optionally specify a comma-separated list of supplemental data fields to return.</param>
            /// <param name="cryptoId">Optionally include one fiat or cryptocurrency ID to filter market pairs by.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing the paginated list of active cryptocurrency exchanges.</returns>
            /// <remarks>
            /// Cache / Update frequency: Mapping data is updated only as needed, every 30 seconds.
            /// Plan credit use: 1 call credit per call.
            /// CMC equivalent pages: No equivalent, this data is only available via API.
            /// </remarks>
            public async Task<Response<List<ExchangeMapData>>> GetMapAsync(string listingStatus = "active", string exchangeSlugs = "", int start = 1, int limit = 5000, ExchangeMapSortEnum sort = ExchangeMapSortEnum.Id, string auxiliaryFields = "", string cryptoId = "")
            {
                var parameters = new ExchangeMapQueryParameters();
                parameters.AddListingStatus(listingStatus);
                parameters.AddSlug(exchangeSlugs);
                parameters.AddStart(start);
                parameters.AddLimit(limit);
                parameters.AddSort(sort);
                parameters.AddAux(auxiliaryFields);
                parameters.AddCryptoId(cryptoId);

                var endpoint = $"{Endpoints.Exchange.Map}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseList<ExchangeMapData>>(endpoint);
                var data = response?.Data.ToList();
                return new Response<List<ExchangeMapData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Retrieves a paginated list of all cryptocurrency exchanges including the latest aggregate market data for each exchange.
            /// </summary>
            /// <param name="start">Optionally offset the start (1-based index) of the paginated list of items to return.</param>
            /// <param name="limit">Optionally specify the number of results to return. Use this parameter and the "start" parameter to determine your own pagination size.</param>
            /// <param name="sort">What field to sort the list of exchanges by.</param>
            /// <param name="sortDirection">The direction in which to order exchanges against the specified sort.</param>
            /// <param name="marketType">The type of exchange markets to include in rankings.</param>
            /// <param name="category">The category for this exchange.</param>
            /// <param name="auxiliaryFields">Optionally specify a comma-separated list of supplemental data fields to return.</param>
            /// <param name="convert">Optionally calculate market quotes in multiple fiat and cryptocurrency conversions.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing the paginated list of cryptocurrency exchanges with the latest aggregate market data.</returns>
            /// <remarks>
            /// Cache / Update frequency: Every 1 minute.
            /// Plan credit use: 1 call credit per 100 exchanges returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: Our latest exchange listing and ranking pages like coinmarketcap.com/rankings/exchanges/.
            /// </remarks>
            public async Task<Response<List<ExchangeListingLatestData>>> GetListingLatestAsync
               (
                   int start = 1,
                   int limit = 100,
                   ExchangeListingSortEnum sort = ExchangeListingSortEnum.Volume24h,
                   SortDirectionEnum sortDirection = SortDirectionEnum.Ascending,
                   MarketTypeEnum marketType = MarketTypeEnum.All,
                   ExchangeCategoryEnum category = ExchangeCategoryEnum.All,
                   string auxiliaryFields = "",
                   string convert = "",
                   string convertId = ""
               )
            {
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new ExchangeLatestQueryParameters();
                parameters.AddStart(start);
                parameters.AddLimit(limit);
                parameters.AddSort(sort);
                parameters.AddSortDir(sortDirection);
                parameters.AddMarketType(marketType);
                parameters.AddCategory(category);
                parameters.AddAux(auxiliaryFields);
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);


                var endpoint = $"{Endpoints.Exchange.Listings.Latest}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseList<ExchangeListingLatestData>>(endpoint);
                var data = response?.Data.ToList();
                return new Response<List<ExchangeListingLatestData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Retrieves all active market pairs that CoinMarketCap tracks for a given exchange.
            /// </summary>
            /// <param name="id">A CoinMarketCap exchange ID.</param>
            /// <param name="slug">Alternatively pass an exchange "slug" (URL friendly all lowercase shorthand version of name with spaces replaced with hyphens).</param>
            /// <param name="start">Optionally offset the start (1-based index) of the paginated list of items to return.</param>
            /// <param name="limit">Optionally specify the number of results to return. Use this parameter and the "start" parameter to determine your own pagination size.</param>
            /// <param name="auxiliaryFields">Optionally specify a comma-separated list of supplemental data fields to return.</param>
            /// <param name="matchedId">Optionally include one or more comma-delimited fiat or cryptocurrency IDs to filter market pairs by.</param>
            /// <param name="matchedSymbol">Optionally include one or more comma-delimited fiat or cryptocurrency symbols to filter market pairs by.</param>
            /// <param name="category">The category of trading this market falls under.</param>
            /// <param name="feeType">The fee type the exchange enforces for this market.</param>
            /// <param name="convert">Optionally calculate market quotes in multiple fiat and cryptocurrency conversions.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing the paginated list of active market pairs for the given exchange.</returns>
            /// <remarks>
            /// Cache / Update frequency: Every 60 seconds.
            /// Plan credit use: 1 call credit per 100 market pairs returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: Our exchange level active markets pages like coinmarketcap.com/exchanges/binance/.
            /// </remarks>
            public async Task<Response<ExchangeMarketPairLatestData>> GetMarketPairsAsync
              (
                  string id = "",
                  string slug = "",
                  int start = 1,
                  int limit = 100,
                  string auxiliaryFields = "",
                  string matchedId = "",
                  string matchedSymbol = "",
                  CategoryEnum category = CategoryEnum.All,
                  FeeTypeEnum feeType = FeeTypeEnum.All,
                  string convert = "",
                  string convertId = ""
              )
            {
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                if (!string.IsNullOrWhiteSpace(matchedId) && !string.IsNullOrWhiteSpace(matchedSymbol))
                {
                    throw new ArgumentException("matched_id and matched_symbol cannot be used together.");
                }
                var parameters = new MarketPairsQueryParameters();
                parameters.AddId(id);
                parameters.AddSlug(slug);
                parameters.AddStart(start);
                parameters.AddLimit(limit);
                parameters.AddAux(auxiliaryFields);
                parameters.AddMatchedId(matchedId);
                parameters.AddMatchedSymbol(matchedSymbol);
                parameters.AddCategory(category);
                parameters.AddFeeType(feeType);
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);



                var endpoint = $"{Endpoints.Exchange.MarketPairs.Latest}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseDict<ExchangeMarketPairLatestData>>(endpoint);
                var data = response?.Data.Values.FirstOrDefault();
                return new Response<ExchangeMarketPairLatestData>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Retrieves an interval of historic quotes for one or more exchanges based on time and interval parameters.
            /// </summary>
            /// <param name="id">One or more comma-separated exchange CoinMarketCap IDs.</param>
            /// <param name="slug">Alternatively, one or more comma-separated exchange names in URL friendly shorthand "slug" format.</param>
            /// <param name="timeStart">Timestamp (Unix or ISO 8601) to start returning quotes for.</param>
            /// <param name="timeEnd">Timestamp (Unix or ISO 8601) to stop returning quotes for (inclusive).</param>
            /// <param name="count">The number of interval periods to return results for.</param>
            /// <param name="interval">Interval of time to return data points for.</param>
            /// <param name="convert">Optionally calculate market quotes in multiple fiat and cryptocurrency conversions.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist (1 month)
            /// - Startup (1 month)
            /// - Standard (3 months)
            /// - Professional (Up to 12 months)
            /// - Enterprise (Up to 6 years)
            /// </remarks>
            /// <returns>A response object containing the historic quotes for the specified exchanges.</returns>
            /// <remarks>
            /// Cache / Update frequency: Every 5 minutes.
            /// Plan credit use: 1 call credit per 100 historical data points returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: No equivalent, this data is only available via API outside of our volume sparkline charts in coinmarketcap.com/rankings/exchanges/.
            /// </remarks>
            public async Task<Response<List<ExchangeHistoricalData>>> GetQuotesHistoricalAsync
                (
                string id = "",
                string slug = "",
                DateTime? timeStart = null,
                DateTime? timeEnd = null,
                int count = 10,
                IntervalEnum interval = IntervalEnum.FiveMinutes,
                string convert = "",
                string convertId = ""
                )
            {
                if (string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(slug))
                {
                    throw new ArgumentException("At least one 'id', 'slug' must be provided.");
                }
                if (count < 1 || count > 10000)
                {
                    throw new ArgumentException("Count cannot be less than 1 or cannot be greater than 10000");
                }
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new ExchangeHistoricalQueryParameters();
                parameters.AddId(id);
                parameters.AddSlug(slug);
                parameters.AddTimeStart(timeStart);
                parameters.AddTimeEnd(timeEnd);
                parameters.AddCount(count);
                parameters.AddInterval(interval);
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);

                var endpoint = $"{Endpoints.Exchange.Quotes.Historical}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseDict<ExchangeHistoricalData>>(endpoint);
                var data = response?.Data?.Values?.ToList();
                return new Response<List<ExchangeHistoricalData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Retrieves the latest aggregate market data for one or more exchanges.
            /// </summary>
            /// <param name="id">One or more comma-separated CoinMarketCap exchange IDs.</param>
            /// <param name="slug">Alternatively, a comma-separated list of exchange "slugs" (URL friendly shorthand version of name).</param>
            /// <param name="convert">Optionally calculate market quotes in multiple fiat and cryptocurrency conversions.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol.</param>
            /// <param name="aux">Optionally specify a comma-separated list of supplemental data fields to return.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing the latest aggregate market data for the specified exchanges.</returns>
            /// <remarks>
            /// Cache / Update frequency: Every 60 seconds.
            /// Plan credit use: 1 call credit per 100 exchanges returned (rounded up) and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: Latest market data summary for specific exchanges like coinmarketcap.com/rankings/exchanges/.
            /// </remarks>
            public async Task<Response<List<ExchangeLatestData>>> GetQuotesLatestAsync
                (
                string id = "",
                string slug = "",
                string convert = "",
                string convertId = "",
                string aux = ""
                )
            {
                if (string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(slug))
                {
                    throw new ArgumentException("At least one 'id', 'slug' must be provided.");
                }
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new ExchangeQuotesLatestQueryParameters();
                parameters.AddId(id);
                parameters.AddSlug(slug);
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);
                parameters.AddAux(aux);


                var endpoint = $"{Endpoints.Exchange.Quotes.Latest}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseDict<ExchangeLatestData>>(endpoint);
                var data = response?.Data?.Values?.ToList();
                return new Response<List<ExchangeLatestData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }


        }
        public class GlobalMetricsEndpoint
        {
            private readonly CoinMarketCapAPI coinMarketCapAPI;

            public GlobalMetricsEndpoint(CoinMarketCapAPI coinMarketCapAPI)
            {
                this.coinMarketCapAPI = coinMarketCapAPI;
            }
            /// <summary>
            /// Retrieves an interval of historical global cryptocurrency market metrics based on time and interval parameters.
            /// </summary>
            /// <param name="timeStart">Timestamp to start returning quotes for. Optional, if not passed, quotes will be calculated in reverse from "timeEnd".</param>
            /// <param name="timeEnd">Timestamp to stop returning quotes for (inclusive). Optional, if not passed, defaults to the current time.</param>
            /// <param name="count">The number of interval periods to return results for. Optional, required if both "timeStart" and "timeEnd" aren't supplied. The default is 10 items. The current query limit is 10000.</param>
            /// <param name="interval">Interval of time to return data points for.</param>
            /// <param name="convert">Optionally calculate market quotes in multiple fiat and cryptocurrency conversions.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol.</param>
            /// <param name="aux">Optionally specify a comma-separated list of supplemental data fields to return.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist (1 month)
            /// - Startup (1 month)
            /// - Standard (3 months)
            /// - Professional (12 months)
            /// - Enterprise (Up to 6 years)
            /// </remarks>
            /// <returns>A response object containing the interval of historical global cryptocurrency market metrics.</returns>
            /// <remarks>
            /// Cache / Update frequency: Every 5 minutes.
            /// Plan credit use: 1 call credit per 100 historical data points returned (rounded up).
            /// CMC equivalent pages: Our Total Market Capitalization global chart coinmarketcap.com/charts/.
            /// </remarks>
            public async Task<Response<GlobalMetricsHistoricalData>> GetQuotesHistoricalAsync
               (
               DateTime? timeStart = null,
               DateTime? timeEnd = null,
               int count = 10,
               IntervalEnum interval = IntervalEnum.Daily,
               string convert = "",
               string convertId = "",
               string aux = ""
               )
            {
                if (count < 1 || count > 10000)
                {
                    throw new ArgumentException("Count cannot be less than 1 or cannot be greater than 10000");
                }
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new GlobalMetricsHistoricalQueryParameters();
                parameters.AddTimeStart(timeStart);
                parameters.AddTimeEnd(timeEnd);
                parameters.AddCount(count);
                parameters.AddInterval(interval);
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);
                parameters.AddAux(aux);

                var endpoint = $"{Endpoints.GlobalMetrics.Quotes.Historical}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<Response<GlobalMetricsHistoricalData>>(endpoint);
                var data = response?.Data;
                return new Response<GlobalMetricsHistoricalData>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Retrieves the latest global cryptocurrency market metrics.
            /// </summary>
            /// <param name="convert">Optionally calculate market quotes in multiple fiat and cryptocurrency conversions.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <returns>A response object containing the latest global cryptocurrency market metrics.</returns>
            /// <remarks>
            /// Cache / Update frequency: Every 5 minutes.
            /// Plan credit use: 1 call credit per call and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: The latest aggregate global market stats ticker across all CMC pages like coinmarketcap.com.
            /// </remarks>
            public async Task<Response<GlobalMetricsLatestData>> GetQuotesLatestAsync
               (
               string convert = "",
               string convertId = ""
               )
            {
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new GlobalMetricsHistoricalQueryParameters();
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);

                var endpoint = $"{Endpoints.GlobalMetrics.Quotes.Latest}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<Response<GlobalMetricsLatestData>>(endpoint);
                var data = response?.Data;
                return new Response<GlobalMetricsLatestData>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
        }
        public class ToolsEndpoint
        {
            private readonly CoinMarketCapAPI coinMarketCapAPI;

            public ToolsEndpoint(CoinMarketCapAPI coinMarketCapAPI)
            {
                this.coinMarketCapAPI = coinMarketCapAPI;
            }
            /// <summary>
            /// Converts an amount of one cryptocurrency or fiat currency into one or more different currencies utilizing the latest market rate for each currency.
            /// </summary>
            /// <param name="amount">An amount of currency to convert.</param>
            /// <param name="id">The CoinMarketCap currency ID of the base cryptocurrency or fiat to convert from.</param>
            /// <param name="symbol">Alternatively, the currency symbol of the base cryptocurrency or fiat to convert from.</param>
            /// <param name="dateTime">Optional timestamp (Unix or ISO 8601) to reference historical pricing during conversion.</param>
            /// <param name="convert">Pass up to 120 comma-separated fiat or cryptocurrency symbols to convert the source amount to.</param>
            /// <param name="convertId">Optionally calculate market quotes by CoinMarketCap ID instead of symbol.</param>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic (Latest market price conversions)
            /// - Hobbyist (Latest market price conversions + 1 month historical)
            /// - Startup (Latest market price conversions + 1 month historical)
            /// - Standard (Latest market price conversions + 3 months historical)
            /// - Professional (Latest market price conversions + 12 months historical)
            /// - Enterprise (Latest market price conversions + up to 6 years historical)
            /// </remarks>
            /// <returns>A response object containing the converted amounts for each specified currency.</returns>
            /// <remarks>
            /// Cache / Update frequency: Every 60 seconds for the latest cryptocurrency and fiat currency rates.
            /// Plan credit use: 1 call credit per call and 1 call credit per convert option beyond the first.
            /// CMC equivalent pages: Our cryptocurrency conversion page at coinmarketcap.com/converter/.
            /// </remarks>
            public async Task<Response<PriceConversionData>> GetPriceConversionAsync
               (
               double amount,
               string id = "",
               string symbol = "",
               DateTime? dateTime = null,
               string convert = "",
               string convertId = ""
               )
            {
                if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(symbol))
                {
                    throw new ArgumentException("At least one 'id' or 'symbol' must be provided.");
                }
                if (!string.IsNullOrWhiteSpace(convert) && !string.IsNullOrWhiteSpace(convertId))
                {
                    throw new ArgumentException("convert and convert_id cannot be used together.");
                }
                var parameters = new PriceConvertionQueryParameters();
                parameters.AddAmount(amount);
                parameters.AddId(id);
                parameters.AddSymbol(symbol);
                parameters.AddTime(dateTime);
                parameters.AddConvert(convert);
                parameters.AddConvertId(convertId);

                var endpoint = $"{Endpoints.Tools.PriceConversion}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseDict<PriceConversionData>>(endpoint);
                var data = response?.Data.Values.FirstOrDefault();
                return new Response<PriceConversionData>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
        }
        public class BlockchainEndpoint
        {
            private readonly CoinMarketCapAPI coinMarketCapAPI;

            public BlockchainEndpoint(CoinMarketCapAPI coinMarketCapAPI)
            {
                this.coinMarketCapAPI = coinMarketCapAPI;
            }
            /// <summary>
            /// Returns the latest blockchain statistics data for 1 or more blockchains. Bitcoin, Litecoin, and Ethereum are currently supported. Additional blockchains will be made available on a regular basis.
            /// </summary>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Enterprise
            /// </remarks>
            /// <returns>A response containing a list of blockchain data.</returns>
            /// <remarks>
            /// Cache/Update frequency: Every 15 seconds.
            /// Plan credit use: 1 call credit per request.
            /// CMC equivalent pages: Our blockchain explorer pages like blockchain.coinmarketcap.com/.
            /// </remarks>
            /// <param name="ids">One or more comma-separated cryptocurrency CoinMarketCap IDs to return blockchain data for. Pass 1,2,1027 to request all currently supported blockchains.</param>
            /// <param name="symbols">Alternatively pass one or more comma-separated cryptocurrency symbols. Pass BTC,LTC,ETH to request all currently supported blockchains.</param>
            /// <param name="slugs">Alternatively pass a comma-separated list of cryptocurrency slugs. Pass bitcoin,litecoin,ethereum to request all currently supported blockchains.</param>
            public async Task<Response<List<BlockchainData>>> GetStatisticsLatestAsync
                (
                 string ids = "",
                 string symbols = "",
                 string slugs = ""
                )
            {
                var parameters = new BlockchainQueryParameters();
                parameters.AddId(ids);
                parameters.AddSymbol(symbols);
                parameters.AddSlug(slugs);

                var endpoint = $"{Endpoints.Blockchain.Statistics.Latest}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseDict<BlockchainData>>(endpoint);
                var data = response?.Data?.Values.ToList();
                return new Response<List<BlockchainData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
        }
        public class KeyEndpoint
        {
            private readonly CoinMarketCapAPI coinMarketCapAPI;

            public KeyEndpoint(CoinMarketCapAPI coinMarketCapAPI)
            {
                this.coinMarketCapAPI = coinMarketCapAPI;
            }
            /// <summary>
            /// Returns API key details and usage stats.
            /// </summary>
            /// <returns>A response object containing the API key details and usage statistics.</returns>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Basic
            /// - Hobbyist
            /// - Startup
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <remarks>
            /// Cache / Update frequency: No cache, this endpoint updates as requests are made with your key.
            /// Plan credit use: No API credit cost. Requests to this endpoint do contribute to your minute based rate limit however.
            /// CMC equivalent pages: Our Developer Portal dashboard for your API Key at pro.coinmarketcap.com/account.
            /// </remarks>
            public async Task<Response<KeyInfoData>> GetKeyInfoAsync()
            {
                var endpoint = $"{Endpoints.Key.Info}";
                var response = await coinMarketCapAPI.GetDataAsync<Response<KeyInfoData>>(endpoint);
                var data = response?.Data;
                return new Response<KeyInfoData>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
        }
        public class ContentEndpoint
        {
            private readonly CoinMarketCapAPI coinMarketCapAPI;

            public ContentEndpoint(CoinMarketCapAPI coinMarketCapAPI)
            {
                this.coinMarketCapAPI = coinMarketCapAPI;
            }
            /// <summary>
            /// Returns a paginated list of content pulled from CMC News/Headlines and Alexandria articles.
            /// </summary>
            /// <param name="start">Offset the start (1-based index) of the paginated list of items to return.</param>
            /// <param name="limit">Number of results to return.</param>
            /// <param name="Ids">Optionally pass a comma-separated list of CoinMarketCap cryptocurrency IDs to filter by.</param>
            /// <param name="Slugs">Optionally pass a comma-separated list of cryptocurrency slugs to filter by.</param>
            /// <param name="Symbols">Optionally pass a comma-separated list of cryptocurrency symbols to filter by.</param>
            /// <param name="newsTypes">Optionally specify a comma-separated list of news types to filter by.</param>
            /// <param name="contentTypes">Optionally specify a comma-separated list of content types to filter by.</param>
            /// <param name="categories">Optionally pass a comma-separated list of categories to filter by.</param>
            /// <param name="language">Optionally pass a language code to filter by.</param>
            /// <returns>A response object containing the paginated list of news articles.</returns>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <remarks>
            /// Cache / Update frequency: Five Minutes
            /// Plan credit use: 0 credit
            /// </remarks>

            public async Task<Response<List<ContentLatestData>>> GetContentLatestAsync(int start = 1, int limit = 100, string Ids = "", string Slugs = "", string Symbols = "", NewsTypeEnum newsTypes = NewsTypeEnum.All, ContentTypeEnum contentTypes = ContentTypeEnum.All, string categories = "", LanguageEnum language = LanguageEnum.English)
            {
                var parameters = new ContentLatestQueryParameters();
                parameters.AddStart(start);
                parameters.AddLimit(limit);
                parameters.AddId(Ids);
                parameters.AddSlug(Slugs);
                parameters.AddSymbol(Symbols);
                parameters.AddNewsType(newsTypes);
                parameters.AddContentType(contentTypes);
                parameters.AddCategory(categories);
                parameters.AddLanguage(language);



                var endpoint = $"{Endpoints.Content.Latest}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseList<ContentLatestData>>(endpoint);
                var data = response?.Data.ToList();
                return new Response<List<ContentLatestData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Returns comments of a CMC Community post.
            /// </summary>
            /// <param name="postId">The ID of the post to retrieve comments for.</param>
            /// <returns>A response object containing the comments of the specified post.</returns>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <remarks>
            /// Cache / Update frequency: Five Minutes
            /// Plan credit use: 0 credit
            /// </remarks>
            public async Task<Response<List<PostCommentsData>>> GetPostCommentsAsync(string postId)
            {

                var parameters = new PostCommentsQueryParameters();
                parameters.AddPostId(postId);


                var endpoint = $"{Endpoints.Content.Posts.Comments}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseList<PostCommentsData>>(endpoint);
                var data = response?.Data.ToList();
                return new Response<List<PostCommentsData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Returns the latest crypto-related posts from the CMC Community.
            /// </summary>
            /// <param name="id">Optional CoinMarketCap cryptocurrency ID to filter posts by.</param>
            /// <param name="slug">Alternatively, pass a cryptocurrency slug to filter posts by.</param>
            /// <param name="symbol">Alternatively, pass a cryptocurrency symbol to filter posts by.</param>
            /// <param name="lastScore">Optional score from the response to find the next batch of posts.</param>
            /// <returns>A response object containing the latest crypto-related posts.</returns>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <remarks>
            /// Cache / Update frequency: Five Minutes
            /// Plan credit use: 0 credit
            /// </remarks>
            public async Task<Response<List<ContentPostsListData>>> GetPostLatestAsync(string id = "", string slug = "", string symbol = "", string lastScore = "")
            {

                var parameters = new ContentPostsLatestQueryParameters();
                parameters.AddId(id);
                parameters.AddSlug(slug);
                parameters.AddSymbol(symbol);
                parameters.AddLastScore(lastScore);


                var endpoint = $"{Endpoints.Content.Posts.Latest}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseDict<ContentPostsListData>>(endpoint);
                var data = response?.Data?.Values.ToList();
                return new Response<List<ContentPostsListData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Returns the top crypto-related posts from the CMC Community.
            /// </summary>
            /// <param name="id">Optional CoinMarketCap cryptocurrency ID to filter posts by.</param>
            /// <param name="slug">Alternatively, pass a cryptocurrency slug to filter posts by.</param>
            /// <param name="symbol">Alternatively, pass a cryptocurrency symbol to filter posts by.</param>
            /// <param name="lastScore">Optional score from the response to find the next batch of related posts.</param>
            /// <returns>A response object containing the top crypto-related posts.</returns>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <remarks>
            /// Cache / Update frequency: Five Minutes
            /// Plan credit use: 0 credit
            /// </remarks>
            public async Task<Response<List<ContentPostsListData>>> GetPostTopAsync(string id = "", string slug = "", string symbol = "", string lastScore = "")
            {

                var parameters = new ContentPostsTopQueryParameters();
                parameters.AddId(id);
                parameters.AddSlug(slug);
                parameters.AddSymbol(symbol);
                parameters.AddLastScore(lastScore);


                var endpoint = $"{Endpoints.Content.Posts.Top}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseDict<ContentPostsListData>>(endpoint);
                var data = response?.Data?.Values.ToList();
                return new Response<List<ContentPostsListData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }

        }
        public class CommunityEndpoint
        {
            private readonly CoinMarketCapAPI coinMarketCapAPI;

            public CommunityEndpoint(CoinMarketCapAPI coinMarketCapAPI)
            {
                this.coinMarketCapAPI = coinMarketCapAPI;
            }
            /// <summary>
            /// Returns the latest trending tokens from the CMC Community.
            /// </summary>
            /// <param name="limit">Optional. The number of results to return. Default is 5.</param>
            /// <returns>A response object containing the latest trending tokens.</returns>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <remarks>
            /// Cache / Update frequency: One Minute
            /// Plan credit use: 0 credit
            /// </remarks>
            public async Task<Response<List<TrendingTokenData>>> GetTrendingTokenAsync(int limit = 5)
            {
                if (limit < 1 && limit > 5)
                {
                    throw new ArgumentException("Limit must be between 1 and 5.");
                }

                var parameters = new TrendingTokenQueryParameters();
                parameters.AddLimit(limit);


                var endpoint = $"{Endpoints.Community.Trending.Token}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseList<TrendingTokenData>>(endpoint);
                var data = response?.Data.ToList();
                return new Response<List<TrendingTokenData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }
            /// <summary>
            /// Returns the latest trending topics from the CMC Community.
            /// </summary>
            /// <param name="limit">Optional. The number of results to return. Default is 5.</param>
            /// <returns>A response object containing the latest trending topics.</returns>
            /// <remarks>
            /// This endpoint is available on the following API plans:
            /// - Standard
            /// - Professional
            /// - Enterprise
            /// </remarks>
            /// <remarks>
            /// Cache / Update frequency: One Minute
            /// Plan credit use: 0 credit
            /// </remarks>
            public async Task<Response<List<TrendingTopicData>>> GetTrendingTopicAsync(int limit = 5)
            {
                if (limit < 1 && limit > 5)
                {
                    throw new ArgumentException("Limit must be between 1 and 5.");
                }

                var parameters = new TrendingTokenQueryParameters();
                parameters.AddLimit(limit);


                var endpoint = $"{Endpoints.Community.Trending.Topic}?{parameters}";
                var response = await coinMarketCapAPI.GetDataAsync<ResponseList<TrendingTopicData>>(endpoint);
                var data = response?.Data.ToList();
                return new Response<List<TrendingTopicData>>
                {
                    Status = response?.Status,
                    Data = data
                };

            }

        }
    }

}
