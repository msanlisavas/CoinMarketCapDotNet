using CoinMarketCapDotNet.Api;
using CoinMarketCapDotNet_Tests.Collection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CoinMarketCapDotNet_Tests
{
    [Collection("CoinMarketCapAPICollection")]
    public class CoinMarketCapAPITests
    {
        private readonly CoinMarketCapAPIFixture _fixture;

        public CoinMarketCapAPITests(CoinMarketCapAPIFixture fixture)
        {
            _fixture = fixture;
        }

        #region Api
        [Fact]
        public async Task GetMapAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetMapAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0);

        }
        [Fact]
        public async Task GetInfoAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetInfoAsync("", "", "BTC");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data[0].Symbol == "BTC");

        }
        [Fact]
        public async Task GetListingLatestAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetListingLatestAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0);

        }
        [Fact]
        public async Task GetListingHistoricalAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetListingHistoricalAsync(DateTime.UtcNow);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0 && result.Data[0].DateAdded.Date == DateTime.UtcNow.Date);

        }

        [Fact]
        public async Task GetListingNewAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetListingNewAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0);

        }
        [Fact]
        public async Task GetTrendingGainersLosersAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetTrendingGainersLosersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0);

        }
        [Fact]
        public async Task GetTrendingLatestAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetTrendingLatestAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0);

        }
        [Fact]
        public async Task GetTrendingMostVisitedAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetTrendingMostVisitedAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0);

        }
        [Fact]
        public async Task GetMarketPairsLatestAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetMarketPairsLatestAsync("", "", "BTC");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Symbol == "BTC");

        }
        [Fact]
        public async Task GetOHCLVLatestAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetOHLCVLatestAsync("", "BTC");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data[0].Symbol == "BTC");

        }
        [Fact]
        public async Task GetOHCLVLatestAsync_ReturnsMultipleExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetOHLCVLatestAsync("", "BTC,ETH");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data[0].Symbol == "BTC" && result.Data[1].Symbol == "ETH");

        }
        [Fact]
        public async Task GetOHCLVHistoricalAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetOHCLVHistoricalAsync("", "", "BTC");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data[0].Symbol == "BTC");

        }
        [Fact]
        public async Task GetPricePerformanceStatsLatestAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetPricePerformanceStatsLatestAsync("", "", "BTC,ETH");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count == 2);

        }
        [Fact]
        public async Task GetQuotesHistoricalV2Async_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetQuotesHistoricalV2Async("", "BTC", DateTime.UtcNow.AddDays(-20), DateTime.UtcNow);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0 && result.Data[0].Symbol == "BTC");

        }
        [Fact]
        public async Task GetQuotesHistoricalV3Async_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetQuotesHistoricalV3Async("", "BTC", DateTime.UtcNow.AddDays(-20), DateTime.UtcNow);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0 && result.Data.First().Value.Symbol == "BTC");

        }
        [Fact]
        public async Task GetQuotesLatestAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetQuotesLatestAsync("", "", "BTC");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data[0].FirstOrDefault().Symbol == "BTC");

        }
        [Fact]
        public async Task GetCategoryAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetCategoryAsync("1");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data != null && result.Data[0].Id == 1);

        }
        [Fact]
        public async Task GetCategoriesAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetCategoriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data != null);

        }
        [Fact]
        public async Task GetAirdropAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetAirdropAsync("1");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data != null);

        }
        [Fact]
        public async Task GetAirdropsAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Cryptocurrency.GetAirdropsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data != null);

        }
        [Fact]
        public async Task GetFiatMapAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Fiat.GetMapAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data != null);

        }
        [Fact]
        public async Task GetAssetsAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Exchange.GetAssetsAsync("270");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0);

        }
        [Fact]
        public async Task GetExchangeInfoAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Exchange.GetInfoAsync("", "binance");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0 && result.Data[0].Slug == "binance");

        }
        [Fact]
        public async Task GetExchangeMapAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Exchange.GetMapAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data != null);

        }
        [Fact]
        public async Task GetExchangeListingLatestAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Exchange.GetListingLatestAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data != null);

        }
        [Fact]
        public async Task GetExchangeMarketPairLatestAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Exchange.GetMarketPairsAsync("", "binance");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data != null && result.Data.Slug == "binance");

        }
        [Fact]
        public async Task GetExchangeQuotesHistoricalAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Exchange.GetQuotesHistoricalAsync("", "binance");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data != null && result.Data[0].Slug == "binance");

        }
        [Fact]
        public async Task GetExchangeQuotesLatestAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Exchange.GetQuotesLatestAsync("", "binance");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data != null && result.Data[0].Slug == "binance");

        }
        [Fact]
        public async Task GetGlobalMetricsQuotesHistoricalAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.GlobalMetrics.GetQuotesHistoricalAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data != null);

        }
        [Fact]
        public async Task GetGlobalMetricsQuotesLatestAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.GlobalMetrics.GetQuotesLatestAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data != null);

        }
        [Fact]
        public async Task GetToolsPriceConversionAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Tools.GetPriceConversionAsync(100, "", "BTC");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data != null && result.Data.Symbol == "BTC");

        }
        [Fact]
        public async Task GetBlockchainStatisticsLatestAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Blockchain.GetStatisticsLatestAsync("1,2,1027");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data != null && result.Data.Count == 3);

        }
        [Fact]
        public async Task GetKeyInfoAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Key.GetKeyInfoAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Plan.RateLimitMinute > 0);

        }
        [Fact]
        public async Task GetContentLatestAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Content.GetContentLatestAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0);

        }
        [Fact]
        public async Task GetCommunityTrendingTokenAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Community.GetTrendingTokenAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0);

        }
        [Fact]
        public async Task GetCommunityTrendingTopicAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Community.GetTrendingTopicAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0);

        }
        [Fact]
        public async Task GetContentPostCommentsAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Content.GetPostCommentsAsync("1");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0);

        }
        [Fact]
        public async Task GetContentPostsLatestAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Content.GetPostLatestAsync("", "", "BTC");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0);

        }
        [Fact]
        public async Task GetContentPostsTopAsync_ReturnsExpectedData()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act
            var result = await coinMarketCapAPI.Content.GetPostTopAsync("", "", "BTC");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0);

        }
        [Fact]
        public async Task GetContentPostsTopAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Content.GetPostTopAsync("", "", "BTC");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetContentPostsLatestAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Content.GetPostLatestAsync("", "", "BTC");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetContentPostCommentsAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Content.GetPostCommentsAsync("1");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetCommunityTrendingTopicAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Community.GetTrendingTopicAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetCommunityTrendingTokenAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Community.GetTrendingTokenAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetKeyInfoAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Key.GetKeyInfoAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetContentLatestAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Content.GetContentLatestAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetToolsPriceConversionAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Tools.GetPriceConversionAsync(100, "", "BTC");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetGlobalMetricsQuotesLatestAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.GlobalMetrics.GetQuotesLatestAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetGlobalMetricsQuotesHistoricalAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.GlobalMetrics.GetQuotesHistoricalAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetExchangQuotesLatestAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Exchange.GetQuotesLatestAsync("", "binance");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetExchangQuotesHistoricalLatestAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Exchange.GetQuotesHistoricalAsync("", "binance");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetExchangMarketPairLatestAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Exchange.GetMarketPairsAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetExchangListingLatestAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Exchange.GetListingLatestAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetExchangeMapAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Exchange.GetMapAsync("", "binance");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetExchangeInfoAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Exchange.GetInfoAsync("", "binance");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetAssetsAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Exchange.GetAssetsAsync("270");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetAirdropAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetAirdropAsync("1");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetAirdropsAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetAirdropsAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetCategoriesAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetCategoriesAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }

        [Fact]
        public async Task GetCategoryAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetCategoryAsync("1");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetAirdropAsync_ThrowsException_WhenIdIsNull()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetAirdropAsync("");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetCategoryAsync_ThrowsException_WhenIdAndSymbolIsNull()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetCategoryAsync("");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetQuotesLatestAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetQuotesLatestAsync("", "", "BTC");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetQuotesLatestAsync_ThrowsException_WhenIdAndSymbolIsNull()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetQuotesLatestAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetQuotesHistoricalV2Async_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetQuotesHistoricalV2Async("", "BTC");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetQuotesHistoricalV2Async_ThrowsException_WhenIdAndSymbolIsNull()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetQuotesHistoricalV2Async();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetQuotesHistoricalV3Async_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetQuotesHistoricalV3Async("", "BTC");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetQuotesHistoricalV3Async_ThrowsException_WhenIdAndSymbolIsNull()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetQuotesHistoricalV3Async();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetPricePerformanceStatsLatestAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetPricePerformanceStatsLatestAsync("", "", "BTC");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetPricePerformanceStatsLatestAsync_ThrowsException_WhenIdAndSymbolIsNull()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetPricePerformanceStatsLatestAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }

        [Fact]
        public async Task GetOHCLVHistoricalAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetOHCLVHistoricalAsync("", "", "BTC");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetOHCLVHistoricalAsync_ThrowsException_WhenIdAndSymbolIsNull()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetOHCLVHistoricalAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetOHCLVLatestAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetOHLCVLatestAsync("", "BTC");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetOHCLVLatestAsync_ThrowsException_WhenIdAndSymbolIsNull()
        {
            _fixture.SetSandboxMode(true); // use sandbox mode
            // Arrange
            var coinMarketCapAPI = _fixture.CoinMarketCapAPI;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetOHLCVLatestAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetMarketPairsLatestAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetMarketPairsLatestAsync("", "", "BTC");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetTrendingMostVisitedAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetTrendingMostVisitedAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetTrendingLatestAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetTrendingLatestAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetTrendingGainersLosersAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetTrendingGainersLosersAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetListingNewAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetListingNewAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }

        [Fact]
        public async Task GetInfoAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetInfoAsync("", "", "BTC");
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }

        [Fact]
        public async Task GetMapAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetMapAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetListingLatestAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetListingLatestAsync();
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        [Fact]
        public async Task GetListingHistoricalAsync_ThrowsException_WhenApiReturnsError()
        {
            // Arrange
            var apiKey = "invalid_api_key";
            var coinMarketCapAPI = new CoinMarketCapAPI(apiKey);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await coinMarketCapAPI.Cryptocurrency.GetListingHistoricalAsync(DateTime.UtcNow);
            });

            _fixture.Dispose(); // Reset shared fixture after using a different API key
        }
        #endregion
    }

}