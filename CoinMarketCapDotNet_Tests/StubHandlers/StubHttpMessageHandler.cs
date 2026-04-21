using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoinMarketCapDotNet_Tests.StubHandlers
{
    internal sealed class StubHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpStatusCode _statusCode;
        private readonly string _body;
        public HttpRequestMessage? LastRequest { get; private set; }
        public CancellationToken LastCancellationToken { get; private set; }

        public StubHttpMessageHandler(HttpStatusCode statusCode, string body)
        {
            _statusCode = statusCode;
            _body = body;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequest = request;
            LastCancellationToken = cancellationToken;
            var response = new HttpResponseMessage(_statusCode)
            {
                Content = new StringContent(_body)
            };
            return Task.FromResult(response);
        }
    }
}
