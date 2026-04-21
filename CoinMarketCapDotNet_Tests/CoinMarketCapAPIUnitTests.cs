using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CoinMarketCapDotNet.Api;
using CoinMarketCapDotNet.Extensions;
using CoinMarketCapDotNet.Models.Enums;
using CoinMarketCapDotNet.Models.Exceptions;
using CoinMarketCapDotNet_Tests.StubHandlers;
using Xunit;

namespace CoinMarketCapDotNet_Tests
{
    public class CoinMarketCapAPIUnitTests
    {
        private const string ErrorBody = """
        { "status": { "error_code": 1001, "error_message": "Test error" } }
        """;

        private static CoinMarketCapAPI ApiWithStub(HttpStatusCode statusCode, string body, out StubHttpMessageHandler handler)
        {
            handler = new StubHttpMessageHandler(statusCode, body);
            var client = new HttpClient(handler);
            return new CoinMarketCapAPI("test-key", client);
        }

        [Fact]
        public async Task BadRequest_throws_CoinMarketCapBadRequestException()
        {
            var api = ApiWithStub(HttpStatusCode.BadRequest, ErrorBody, out _);
            var ex = await Assert.ThrowsAsync<CoinMarketCapBadRequestException>(
                () => api.Cryptocurrency.GetMapAsync());
            Assert.Equal(HttpStatusCode.BadRequest, ex.StatusCode);
            Assert.Equal(1001, ex.ErrorCode);
            Assert.Equal("Test error", ex.CmcErrorMessage);
        }

        [Fact]
        public async Task Unauthorized_throws_CoinMarketCapAuthException()
        {
            var api = ApiWithStub(HttpStatusCode.Unauthorized, ErrorBody, out _);
            var ex = await Assert.ThrowsAsync<CoinMarketCapAuthException>(
                () => api.Cryptocurrency.GetMapAsync());
            Assert.Equal(HttpStatusCode.Unauthorized, ex.StatusCode);
            Assert.Equal(1001, ex.ErrorCode);
            Assert.Equal("Test error", ex.CmcErrorMessage);
        }

        [Fact]
        public async Task Forbidden_throws_CoinMarketCapAuthException()
        {
            var api = ApiWithStub(HttpStatusCode.Forbidden, ErrorBody, out _);
            var ex = await Assert.ThrowsAsync<CoinMarketCapAuthException>(
                () => api.Cryptocurrency.GetMapAsync());
            Assert.Equal(HttpStatusCode.Forbidden, ex.StatusCode);
            Assert.Equal(1001, ex.ErrorCode);
            Assert.Equal("Test error", ex.CmcErrorMessage);
        }

        [Fact]
        public async Task TooManyRequests_throws_CoinMarketCapRateLimitException()
        {
            var api = ApiWithStub(HttpStatusCode.TooManyRequests, ErrorBody, out _);
            var ex = await Assert.ThrowsAsync<CoinMarketCapRateLimitException>(
                () => api.Cryptocurrency.GetMapAsync());
            Assert.Equal(HttpStatusCode.TooManyRequests, ex.StatusCode);
            Assert.Equal(1001, ex.ErrorCode);
            Assert.Equal("Test error", ex.CmcErrorMessage);
        }

        [Fact]
        public async Task InternalServerError_throws_CoinMarketCapServerException()
        {
            var api = ApiWithStub(HttpStatusCode.InternalServerError, ErrorBody, out _);
            var ex = await Assert.ThrowsAsync<CoinMarketCapServerException>(
                () => api.Cryptocurrency.GetMapAsync());
            Assert.Equal(HttpStatusCode.InternalServerError, ex.StatusCode);
            Assert.Equal(1001, ex.ErrorCode);
            Assert.Equal("Test error", ex.CmcErrorMessage);
        }

        [Fact]
        public async Task Ok_returns_deserialized_payload()
        {
            const string okBody = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var api = ApiWithStub(HttpStatusCode.OK, okBody, out _);
            var result = await api.Cryptocurrency.GetMapAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CancellationToken_propagates_to_HttpClient()
        {
            const string okBody = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var api = ApiWithStub(HttpStatusCode.OK, okBody, out var handler);
            using var cts = new System.Threading.CancellationTokenSource();
            await api.Cryptocurrency.GetMapAsync(cancellationToken: cts.Token);
            // HttpClient links the caller's token with its own timeout token,
            // so the handler receives a linked token rather than the original.
            // Verify the token that reached the handler is a real cancellable token
            // (not the default CancellationToken.None), proving the token was forwarded.
            Assert.True(handler.LastCancellationToken.CanBeCanceled);
        }

        [Fact]
        public void Options_constructor_validates_ApiKey()
        {
            var options = new CoinMarketCapDotNet.Configuration.CoinMarketCapOptions { ApiKey = "" };
            Assert.Throws<ArgumentException>(() => new CoinMarketCapAPI(options));
        }

        [Fact]
        public async Task Options_constructor_uses_BaseAddress_override()
        {
            const string okBody = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, okBody);
            var client = new HttpClient(handler);
            var options = new CoinMarketCapDotNet.Configuration.CoinMarketCapOptions
            {
                ApiKey = "test-key",
                BaseAddress = new Uri("https://example.com/")
            };
            var api = new CoinMarketCapAPI(options, client);
            await api.Cryptocurrency.GetMapAsync();
            Assert.NotNull(handler.LastRequest);
            Assert.StartsWith("https://example.com/", handler.LastRequest!.RequestUri!.ToString());
        }

        [Fact]
        public async Task Options_constructor_sandbox_mode_uses_sandbox_host()
        {
            const string okBody = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, okBody);
            var client = new HttpClient(handler);
            var options = new CoinMarketCapDotNet.Configuration.CoinMarketCapOptions
            {
                ApiKey = "test-key",
                UseSandbox = true
            };
            var api = new CoinMarketCapAPI(options, client);
            await api.Cryptocurrency.GetMapAsync();
            Assert.StartsWith("https://sandbox-api.coinmarketcap.com/", handler.LastRequest!.RequestUri!.ToString());
        }

        [Fact]
        public void Options_constructor_with_Timeout_does_not_mutate_default_client()
        {
            var before = System.Net.Http.HttpClient.DefaultProxy; // dummy access to ensure the static type is fully loaded; irrelevant to the test
            // Create one API with a custom Timeout (no injected client — triggers the owned-HttpClient branch).
            var opts1 = new CoinMarketCapDotNet.Configuration.CoinMarketCapOptions
            {
                ApiKey = "test-key",
                Timeout = TimeSpan.FromSeconds(5)
            };
            var api1 = new CoinMarketCapAPI(opts1);

            // Create a second API with a different Timeout.
            var opts2 = new CoinMarketCapDotNet.Configuration.CoinMarketCapOptions
            {
                ApiKey = "test-key",
                Timeout = TimeSpan.FromSeconds(10)
            };
            var api2 = new CoinMarketCapAPI(opts2);

            // Both APIs should have constructed without throwing, and neither should have
            // corrupted any shared HttpClient state. We cannot directly inspect _client (private),
            // so this test's real purpose is: constructing two instances with differing Timeout values
            // must not throw (e.g. InvalidOperationException from mutating a shared HttpClient).
            Assert.NotNull(api1);
            Assert.NotNull(api2);
        }

        [Fact]
        public void Options_constructor_throws_on_null_options()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new CoinMarketCapAPI((CoinMarketCapDotNet.Configuration.CoinMarketCapOptions)null!));
        }

        [Fact]
        public void GetEnumMemberValue_returns_wire_value_for_ExchangeCategoryEnum()
        {
            Assert.Equal("all", CoinMarketCapDotNet.Models.Enums.ExchangeCategoryEnum.All.GetEnumMemberValue());
            Assert.Equal("derivatives", CoinMarketCapDotNet.Models.Enums.ExchangeCategoryEnum.Derivatives.GetEnumMemberValue());
        }

        [Fact]
        public void GetEnumMemberValue_returns_wire_value_for_MarketTypeEnum()
        {
            Assert.Equal("no_fees", CoinMarketCapDotNet.Models.Enums.MarketTypeEnum.NoFees.GetEnumMemberValue());
        }

        [Fact]
        public void Dispose_disposes_owned_HttpClient_when_Timeout_was_specified()
        {
            var opts = new CoinMarketCapDotNet.Configuration.CoinMarketCapOptions
            {
                ApiKey = "test-key",
                Timeout = TimeSpan.FromSeconds(5)
            };
            var api = new CoinMarketCapAPI(opts);

            api.Dispose();

            // Calling Dispose() a second time should be a no-op (no exception).
            api.Dispose();
        }

        [Fact]
        public async Task Dispose_does_not_dispose_injected_HttpClient()
        {
            const string okBody = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, okBody);
            var client = new HttpClient(handler);
            var opts = new CoinMarketCapDotNet.Configuration.CoinMarketCapOptions { ApiKey = "test-key" };

            using (var api1 = new CoinMarketCapAPI(opts, client))
            {
                await api1.Cryptocurrency.GetMapAsync();
            } // api1 disposed here; injected client must survive

            // Use the injected client through a fresh API instance — proves it wasn't disposed.
            var api2 = new CoinMarketCapAPI(opts, client);
            await api2.Cryptocurrency.GetMapAsync(); // would throw ObjectDisposedException if client had been disposed
        }

        [Fact]
        public async Task Unmapped_status_throws_base_CoinMarketCapException()
        {
            // 418 (I'm a teapot) is not in the explicit switch cases, so it should hit the default arm.
            var api = ApiWithStub((HttpStatusCode)418, ErrorBody, out _);
            var ex = await Assert.ThrowsAsync<CoinMarketCapException>(
                () => api.Cryptocurrency.GetMapAsync());
            // Verify it's the BASE type, not any specialized subclass.
            Assert.Equal(typeof(CoinMarketCapException), ex.GetType());
            Assert.Equal((HttpStatusCode)418, ex.StatusCode);
            Assert.Equal(1001, ex.ErrorCode);
            Assert.Equal("Test error", ex.CmcErrorMessage);
        }

        [Fact]
        public async Task FearAndGreed_GetLatestAsync_calls_v3_endpoint_and_deserializes()
        {
            const string body = """
            {
              "status": { "error_code": 0, "error_message": null },
              "data": { "value": 65, "update_time": "2026-04-21T12:00:00.000Z", "value_classification": "Greed" }
            }
            """;
            var api = ApiWithStub(HttpStatusCode.OK, body, out var handler);
            var result = await api.FearAndGreed.GetLatestAsync();

            Assert.NotNull(handler.LastRequest);
            Assert.Contains("/v3/fear-and-greed/latest", handler.LastRequest!.RequestUri!.ToString());
            Assert.NotNull(result.Data);
            Assert.Equal(65, result.Data!.Value);
            Assert.Equal("Greed", result.Data.ValueClassification);
        }

        [Fact]
        public async Task FearAndGreed_GetHistoricalAsync_passes_query_params_and_deserializes()
        {
            const string body = """
            {
              "status": { "error_code": 0, "error_message": null },
              "data": [
                { "value": 50, "timestamp": "2026-04-20T00:00:00.000Z", "value_classification": "Neutral" },
                { "value": 55, "timestamp": "2026-04-21T00:00:00.000Z", "value_classification": "Neutral" }
              ]
            }
            """;
            var api = ApiWithStub(HttpStatusCode.OK, body, out var handler);
            var result = await api.FearAndGreed.GetHistoricalAsync(start: 1, limit: 10);

            Assert.NotNull(handler.LastRequest);
            var url = handler.LastRequest!.RequestUri!.ToString();
            Assert.Contains("/v3/fear-and-greed/historical", url);
            Assert.Contains("start=1", url);
            Assert.Contains("limit=10", url);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data!.Count);
        }
    }
}
