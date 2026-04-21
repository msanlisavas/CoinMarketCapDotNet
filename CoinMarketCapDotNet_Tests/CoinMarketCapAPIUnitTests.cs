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
    }
}
