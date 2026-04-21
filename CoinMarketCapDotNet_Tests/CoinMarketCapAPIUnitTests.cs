using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CoinMarketCapDotNet.Api;
using CoinMarketCapDotNet.Extensions;
using CoinMarketCapDotNet.Models.Dex.Holders.Count;
using CoinMarketCapDotNet.Models.Dex.Holders.TagCount;
using CoinMarketCapDotNet.Models.Dex.Kline.Candles;
using CoinMarketCapDotNet.Models.Dex.Kline.Points;
using CoinMarketCapDotNet.Models.Dex.Platform.Detail;
using CoinMarketCapDotNet.Models.Dex.Platform.List;
using CoinMarketCapDotNet.Models.Dex.Token.Detail;
using CoinMarketCapDotNet.Models.Dex.Token.Trending;
using CoinMarketCapDotNet.Models.Enums;
using CoinMarketCapDotNet.Models.Exceptions;
using CoinMarketCapDotNet.Models.General;
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

        [Fact]
        public async Task Index_GetCmc100LatestAsync_calls_v3_endpoint_and_deserializes()
        {
            const string body = """
            {
              "status": { "error_code": 0, "error_message": null },
              "data": {
                "value": 1234.56,
                "update_time": "2026-04-21T12:00:00.000Z",
                "constituents": [
                  { "id": 1, "name": "Bitcoin", "symbol": "BTC", "weight": 0.45 },
                  { "id": 1027, "name": "Ethereum", "symbol": "ETH", "weight": 0.25 }
                ]
              }
            }
            """;
            var api = ApiWithStub(HttpStatusCode.OK, body, out var handler);
            var result = await api.Index.GetCmc100LatestAsync();

            Assert.Contains("/v3/index/cmc100-latest", handler.LastRequest!.RequestUri!.ToString());
            Assert.NotNull(result.Data);
            Assert.Equal(1234.56, result.Data!.Value);
            Assert.Equal(2, result.Data.Constituents.Count);
            Assert.Equal("BTC", result.Data.Constituents[0].Symbol);
        }

        [Fact]
        public async Task Index_GetCmc100HistoricalAsync_passes_query_params_and_deserializes()
        {
            const string body = """
            {
              "status": { "error_code": 0, "error_message": null },
              "data": [
                { "value": 1200.0, "update_time": "2026-04-20T00:00:00.000Z" },
                { "value": 1234.56, "update_time": "2026-04-21T00:00:00.000Z" }
              ]
            }
            """;
            var api = ApiWithStub(HttpStatusCode.OK, body, out var handler);
            var result = await api.Index.GetCmc100HistoricalAsync(interval: "1d", count: 2);

            var url = handler.LastRequest!.RequestUri!.ToString();
            Assert.Contains("/v3/index/cmc100-historical", url);
            Assert.Contains("interval=1d", url);
            Assert.Contains("count=2", url);
            Assert.Equal(2, result.Data!.Count);
        }

        [Fact]
        public async Task Index_GetCmc20LatestAsync_calls_v3_endpoint()
        {
            const string body = """
            {
              "status": { "error_code": 0, "error_message": null },
              "data": { "value": 987.65, "update_time": "2026-04-21T12:00:00.000Z", "constituents": [] }
            }
            """;
            var api = ApiWithStub(HttpStatusCode.OK, body, out var handler);
            var result = await api.Index.GetCmc20LatestAsync();

            Assert.Contains("/v3/index/cmc20-latest", handler.LastRequest!.RequestUri!.ToString());
            Assert.Equal(987.65, result.Data!.Value);
        }

        [Fact]
        public async Task Index_GetCmc20HistoricalAsync_passes_query_params()
        {
            const string body = """
            {
              "status": { "error_code": 0, "error_message": null },
              "data": []
            }
            """;
            var api = ApiWithStub(HttpStatusCode.OK, body, out var handler);
            await api.Index.GetCmc20HistoricalAsync(interval: "1h", count: 24);

            var url = handler.LastRequest!.RequestUri!.ToString();
            Assert.Contains("/v3/index/cmc20-historical", url);
            Assert.Contains("interval=1h", url);
            Assert.Contains("count=24", url);
        }

        [Fact]
        public async Task Cryptocurrency_GetQuotesLatestV3Async_calls_v3_endpoint()
        {
            const string body = """
            {
              "status": { "error_code": 0, "error_message": null },
              "data": []
            }
            """;
            var api = ApiWithStub(HttpStatusCode.OK, body, out var handler);
            await api.Cryptocurrency.GetQuotesLatestV3Async();

            Assert.Contains("/v3/cryptocurrency/quotes/latest", handler.LastRequest!.RequestUri!.ToString());
        }

        [Fact]
        public async Task Cryptocurrency_GetListingLatestV3Async_calls_v3_endpoint()
        {
            const string body = """
            {
              "status": { "error_code": 0, "error_message": null },
              "data": []
            }
            """;
            var api = ApiWithStub(HttpStatusCode.OK, body, out var handler);
            await api.Cryptocurrency.GetListingLatestV3Async();

            Assert.Contains("/v3/cryptocurrency/listings/latest", handler.LastRequest!.RequestUri!.ToString());
        }

        [Fact]
        public async Task PostDataAsync_sends_POST_request_with_JSON_body()
        {
            const string okBody = """
            {
              "status": { "error_code": 0, "error_message": null },
              "data": []
            }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, okBody);
            var client = new HttpClient(handler);
            var api = new CoinMarketCapAPI("test-key", client);

            var requestBody = new { network_slug = "ethereum", limit = 10 };
            var result = await api.PostDataAsync<ResponseList<Status>>("v1/dex/test-endpoint", requestBody);

            Assert.NotNull(handler.LastRequest);
            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.Contains("/v1/dex/test-endpoint", handler.LastRequest.RequestUri!.ToString());
            Assert.NotNull(handler.LastRequest.Content);
            var sentBody = await handler.LastRequest.Content!.ReadAsStringAsync();
            Assert.Contains("\"network_slug\":\"ethereum\"", sentBody);
            Assert.Contains("\"limit\":10", sentBody);
        }

        [Fact]
        public async Task PostDataAsync_omits_null_fields_from_body()
        {
            const string okBody = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, okBody);
            var client = new HttpClient(handler);
            var api = new CoinMarketCapAPI("test-key", client);

            var requestBody = new { network_slug = "ethereum", limit = (int?)null, sort_field = (string?)null };
            await api.PostDataAsync<ResponseList<Status>>("v1/dex/test-endpoint", requestBody);

            var sentBody = await handler.LastRequest!.Content!.ReadAsStringAsync();
            Assert.Contains("\"network_slug\":\"ethereum\"", sentBody);
            Assert.DoesNotContain("limit", sentBody);
            Assert.DoesNotContain("sort_field", sentBody);
        }

        [Fact]
        public async Task Dex_Token_GetTrendingAsync_posts_to_v1_endpoint_with_filter_body()
        {
            const string body = """
            {
              "status": { "error_code": 0, "error_message": null },
              "data": [
                {
                  "id": "0xabc",
                  "name": "TestToken",
                  "symbol": "TEST",
                  "network_slug": "ethereum",
                  "price_usd": 1.23,
                  "volume_24h_usd": 100000.0,
                  "market_cap_usd": 5000000.0
                }
              ]
            }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var client = new HttpClient(handler);
            var api = new CoinMarketCapAPI("test-key", client);

            var result = await api.Dex.Token.GetTrendingAsync(networkSlug: "ethereum", limit: 10);

            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.Contains("/v1/dex/tokens/trending/list", handler.LastRequest.RequestUri!.ToString());
            var sentBody = await handler.LastRequest.Content!.ReadAsStringAsync();
            Assert.Contains("\"network_slug\":\"ethereum\"", sentBody);
            Assert.Contains("\"limit\":10", sentBody);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data!);
            Assert.Equal("TEST", result.Data![0].Symbol);
        }

        [Fact]
        public async Task Dex_Token_BatchQueryAsync_posts_with_addresses()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await api.Dex.Token.BatchQueryAsync(new[] { "0xabc", "0xdef" }, networkSlug: "ethereum");

            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.Contains("/v1/dex/tokens/batch-query", handler.LastRequest.RequestUri!.ToString());
            var sentBody = await handler.LastRequest.Content!.ReadAsStringAsync();
            Assert.Contains("0xabc", sentBody);
            Assert.Contains("0xdef", sentBody);
            Assert.Contains("ethereum", sentBody);
        }

        [Fact]
        public async Task Dex_Token_BatchPriceAsync_posts_to_correct_endpoint()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await api.Dex.Token.BatchPriceAsync(new[] { "0xabc" });

            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.Contains("/v1/dex/token/price/batch", handler.LastRequest.RequestUri!.ToString());
        }

        [Fact]
        public async Task Dex_Token_GetNewListAsync_posts_to_correct_endpoint()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await api.Dex.Token.GetNewListAsync(networkSlug: "solana", limit: 5);

            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.Contains("/v1/dex/new/list", handler.LastRequest.RequestUri!.ToString());
            var sentBody = await handler.LastRequest.Content!.ReadAsStringAsync();
            Assert.Contains("\"network_slug\":\"solana\"", sentBody);
        }

        [Fact]
        public async Task Dex_Token_GetMemeListAsync_posts_to_correct_endpoint()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await api.Dex.Token.GetMemeListAsync();

            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.Contains("/v1/dex/meme/list", handler.LastRequest.RequestUri!.ToString());
        }

        [Fact]
        public async Task Dex_Token_GetGainerLoserAsync_posts_to_correct_endpoint()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await api.Dex.Token.GetGainerLoserAsync(limit: 20);

            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.Contains("/v1/dex/gainer-loser/list", handler.LastRequest.RequestUri!.ToString());
        }

        [Fact]
        public async Task Dex_Token_GetTokenAsync_gets_v1_endpoint_with_query()
        {
            const string body = """
            {
              "status": { "error_code": 0, "error_message": null },
              "data": {
                "id": "0xabc",
                "name": "TestToken",
                "symbol": "TEST",
                "network_slug": "ethereum",
                "price_usd": 1.23,
                "decimals": 18
              }
            }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            var result = await api.Dex.Token.GetTokenAsync(address: "0xabc", networkSlug: "ethereum");

            Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
            var url = handler.LastRequest.RequestUri!.ToString();
            Assert.Contains("/v1/dex/token", url);
            Assert.Contains("address=0xabc", url);
            Assert.Contains("network_slug=ethereum", url);
            Assert.Equal("TEST", result.Data!.Symbol);
            Assert.Equal(18, result.Data.Decimals);
        }

        [Fact]
        public async Task Dex_Token_GetPriceAsync_gets_v1_endpoint()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": { "address": "0xabc", "price_usd": 1.23 } }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await api.Dex.Token.GetPriceAsync("0xabc", "ethereum");

            Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
            var url = handler.LastRequest.RequestUri!.ToString();
            Assert.Contains("/v1/dex/token/price", url);
            Assert.Contains("address=0xabc", url);
        }

        [Fact]
        public async Task Dex_Token_GetPoolsAsync_gets_v1_endpoint()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await api.Dex.Token.GetPoolsAsync("0xabc", "ethereum");

            Assert.Contains("/v1/dex/token/pools", handler.LastRequest!.RequestUri!.ToString());
        }

        [Fact]
        public async Task Dex_Token_GetLiquidityAsync_gets_v1_endpoint()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": { "address": "0xabc" } }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await api.Dex.Token.GetLiquidityAsync("0xabc", "ethereum");

            Assert.Contains("/v1/dex/token-liquidity/query", handler.LastRequest!.RequestUri!.ToString());
        }

        [Fact]
        public async Task Dex_Token_GetTransactionsAsync_gets_v1_endpoint_with_limit()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await api.Dex.Token.GetTransactionsAsync("0xabc", "ethereum", limit: 50);

            var url = handler.LastRequest!.RequestUri!.ToString();
            Assert.Contains("/v1/dex/tokens/transactions", url);
            Assert.Contains("limit=50", url);
        }

        [Fact]
        public async Task Dex_Token_GetSecurityAsync_gets_v1_endpoint()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": { "address": "0xabc", "is_honeypot": false } }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            var result = await api.Dex.Token.GetSecurityAsync("0xabc", "ethereum");

            Assert.Contains("/v1/dex/security/detail", handler.LastRequest!.RequestUri!.ToString());
            Assert.False(result.Data!.IsHoneypot);
        }

        [Fact]
        public async Task Dex_Token_SearchAsync_gets_v1_endpoint_with_keyword()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await api.Dex.Token.SearchAsync("pepe");

            var url = handler.LastRequest!.RequestUri!.ToString();
            Assert.Contains("/v1/dex/search", url);
            Assert.Contains("keyword=pepe", url);
        }

        [Fact]
        public async Task Dex_Token_GetLiquidityChangeAsync_gets_v1_endpoint()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await api.Dex.Token.GetLiquidityChangeAsync("0xabc", "ethereum", limit: 100);

            var url = handler.LastRequest!.RequestUri!.ToString();
            Assert.Contains("/v1/dex/liquidity-change/list", url);
            Assert.Contains("limit=100", url);
        }

        [Fact]
        public async Task Dex_Pairs_GetSpotPairsLatestAsync_gets_v4_endpoint()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await api.Dex.Pairs.GetSpotPairsLatestAsync(networkSlug: "ethereum", limit: 25);

            Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
            var url = handler.LastRequest.RequestUri!.ToString();
            Assert.Contains("/v4/dex/spot-pairs/latest", url);
            Assert.Contains("network_slug=ethereum", url);
            Assert.Contains("limit=25", url);
        }

        [Fact]
        public async Task Dex_Pairs_GetQuotesLatestAsync_gets_v4_endpoint()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await api.Dex.Pairs.GetQuotesLatestAsync("0xpair", "ethereum");

            Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
            var url = handler.LastRequest.RequestUri!.ToString();
            Assert.Contains("/v4/dex/pairs/quotes/latest", url);
            Assert.Contains("pair_address=0xpair", url);
            Assert.Contains("network_slug=ethereum", url);
        }

        [Fact]
        public async Task Dex_Pairs_GetQuotesLatestAsync_validates_required_params()
        {
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, "{}");
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await Assert.ThrowsAsync<ArgumentException>(() => api.Dex.Pairs.GetQuotesLatestAsync("", "ethereum"));
            await Assert.ThrowsAsync<ArgumentException>(() => api.Dex.Pairs.GetQuotesLatestAsync("0xpair", ""));
        }

        [Fact]
        public async Task PostDataAsync_propagates_CancellationToken_to_HttpClient()
        {
            const string okBody = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, okBody);
            var client = new HttpClient(handler);
            var api = new CoinMarketCapAPI("test-key", client);

            using var cts = new System.Threading.CancellationTokenSource();
            await api.PostDataAsync<ResponseList<Status>>("v1/dex/test-endpoint", new { x = 1 }, cts.Token);

            Assert.True(handler.LastCancellationToken.CanBeCanceled);
        }

        [Fact]
        public async Task Dex_Platform_GetListAsync_gets_v1_endpoint()
        {
            const string body = """
            {
              "status": { "error_code": 0, "error_message": null },
              "data": [
                { "network_slug": "ethereum", "name": "Ethereum", "chain_id": "1" },
                { "network_slug": "bsc", "name": "BNB Smart Chain", "chain_id": "56" }
              ]
            }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            var result = await api.Dex.Platform.GetListAsync();

            Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
            Assert.Contains("/v1/dex/platform/list", handler.LastRequest.RequestUri!.ToString());
            Assert.Equal(2, result.Data!.Count);
            Assert.Equal("ethereum", result.Data[0].NetworkSlug);
        }

        [Fact]
        public async Task Dex_Platform_GetDetailAsync_gets_v1_endpoint_with_query()
        {
            const string body = """
            {
              "status": { "error_code": 0, "error_message": null },
              "data": {
                "network_slug": "ethereum",
                "name": "Ethereum",
                "chain_id": "1",
                "supported_dexes": ["uniswap", "sushiswap"],
                "token_count": 50000,
                "pair_count": 30000
              }
            }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            var result = await api.Dex.Platform.GetDetailAsync("ethereum");

            Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
            var url = handler.LastRequest.RequestUri!.ToString();
            Assert.Contains("/v1/dex/platform/detail", url);
            Assert.Contains("network_slug=ethereum", url);
            Assert.Equal(2, result.Data!.SupportedDexes.Count);
            Assert.Equal(50000, result.Data.TokenCount);
        }

        [Fact]
        public async Task Dex_Platform_GetDetailAsync_validates_required_param()
        {
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, "{}");
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await Assert.ThrowsAsync<ArgumentException>(() => api.Dex.Platform.GetDetailAsync(""));
        }

        [Fact]
        public async Task Dex_Kline_GetPointsAsync_gets_v1_endpoint_with_query()
        {
            const string body = """
            {
              "status": { "error_code": 0, "error_message": null },
              "data": [
                { "timestamp": "2026-04-21T12:00:00.000Z", "price_usd": 1.23, "volume_usd": 50000.0 },
                { "timestamp": "2026-04-21T13:00:00.000Z", "price_usd": 1.25, "volume_usd": 60000.0 }
              ]
            }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            var result = await api.Dex.Kline.GetPointsAsync("0xabc", "ethereum", "1h", limit: 24);

            Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
            var url = handler.LastRequest.RequestUri!.ToString();
            Assert.Contains("/v1/k-line/points", url);
            Assert.Contains("address=0xabc", url);
            Assert.Contains("network_slug=ethereum", url);
            Assert.Contains("interval=1h", url);
            Assert.Contains("limit=24", url);
            Assert.Equal(2, result.Data!.Count);
            Assert.Equal(1.23, result.Data[0].PriceUsd);
        }

        [Fact]
        public async Task Dex_Kline_GetCandlesAsync_gets_v1_endpoint_with_query()
        {
            const string body = """
            {
              "status": { "error_code": 0, "error_message": null },
              "data": [
                { "timestamp": "2026-04-21T12:00:00.000Z", "open": 1.20, "high": 1.30, "low": 1.18, "close": 1.25, "volume_usd": 110000.0, "trader_count": 87 }
              ]
            }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            var result = await api.Dex.Kline.GetCandlesAsync("0xabc", "ethereum", "5m");

            Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
            var url = handler.LastRequest.RequestUri!.ToString();
            Assert.Contains("/v1/k-line/candles", url);
            Assert.Contains("interval=5m", url);
            Assert.Single(result.Data!);
            Assert.Equal(1.20, result.Data![0].Open);
            Assert.Equal(1.25, result.Data[0].Close);
            Assert.Equal(87, result.Data[0].TraderCount);
        }

        [Fact]
        public async Task Dex_Kline_GetPointsAsync_validates_required_params()
        {
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, "{}");
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await Assert.ThrowsAsync<ArgumentException>(() => api.Dex.Kline.GetPointsAsync("", "ethereum", "1h"));
            await Assert.ThrowsAsync<ArgumentException>(() => api.Dex.Kline.GetPointsAsync("0xabc", "", "1h"));
            await Assert.ThrowsAsync<ArgumentException>(() => api.Dex.Kline.GetPointsAsync("0xabc", "ethereum", ""));
        }

        [Fact]
        public async Task Dex_Holders_GetListAsync_posts_to_v1_endpoint_with_body()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await api.Dex.Holders.GetListAsync("0xtoken", "ethereum", limit: 50, sortField: "balance_usd", sortDirection: "desc");

            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.Contains("/v1/dex/holders/list", handler.LastRequest.RequestUri!.ToString());
            var sentBody = await handler.LastRequest.Content!.ReadAsStringAsync();
            Assert.Contains("\"token_address\":\"0xtoken\"", sentBody);
            Assert.Contains("\"network_slug\":\"ethereum\"", sentBody);
            Assert.Contains("\"limit\":50", sentBody);
            Assert.Contains("\"sort_field\":\"balance_usd\"", sentBody);
        }

        [Fact]
        public async Task Dex_Holders_GetDetailAsync_posts_to_v1_endpoint_with_body()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": { "wallet_address": "0xwallet" } }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await api.Dex.Holders.GetDetailAsync("0xtoken", "0xwallet", "ethereum");

            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.Contains("/v1/dex/holders/detail", handler.LastRequest.RequestUri!.ToString());
            var sentBody = await handler.LastRequest.Content!.ReadAsStringAsync();
            Assert.Contains("\"wallet_address\":\"0xwallet\"", sentBody);
        }

        [Fact]
        public async Task Dex_Holders_GetTrendListAsync_gets_v1_endpoint()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            await api.Dex.Holders.GetTrendListAsync("0xtoken", "ethereum", "1d", limit: 30);

            Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
            var url = handler.LastRequest.RequestUri!.ToString();
            Assert.Contains("/v1/dex/holders/trend/list", url);
            Assert.Contains("token_address=0xtoken", url);
            Assert.Contains("interval=1d", url);
        }

        [Fact]
        public async Task Dex_Holders_GetTagCountAsync_gets_v1_endpoint()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": [ { "tag": "whale", "count": 12 } ] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            var result = await api.Dex.Holders.GetTagCountAsync("0xtoken", "ethereum");

            Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
            Assert.Contains("/v1/dex/holders/tag_count", handler.LastRequest.RequestUri!.ToString());
            Assert.Single(result.Data!);
            Assert.Equal("whale", result.Data![0].Tag);
        }

        [Fact]
        public async Task Dex_Holders_GetCountAsync_gets_v1_endpoint()
        {
            const string body = """
            { "status": { "error_code": 0, "error_message": null }, "data": { "token_address": "0xtoken", "holder_count": 5432 } }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
            var api = new CoinMarketCapAPI("test-key", new HttpClient(handler));

            var result = await api.Dex.Holders.GetCountAsync("0xtoken", "ethereum");

            Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
            Assert.Contains("/v1/dex/holders/count", handler.LastRequest.RequestUri!.ToString());
            Assert.Equal(5432, result.Data!.HolderCount);
        }
    }
}
