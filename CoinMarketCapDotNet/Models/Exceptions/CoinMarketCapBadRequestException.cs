using System.Net;

namespace CoinMarketCapDotNet.Models.Exceptions
{
    public sealed class CoinMarketCapBadRequestException : CoinMarketCapException
    {
        public CoinMarketCapBadRequestException(int? errorCode, string? cmcErrorMessage, string message)
            : base(HttpStatusCode.BadRequest, errorCode, cmcErrorMessage, message) { }
    }
}
