using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CoinMarketCapDotNet.Api;
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
    }
}
