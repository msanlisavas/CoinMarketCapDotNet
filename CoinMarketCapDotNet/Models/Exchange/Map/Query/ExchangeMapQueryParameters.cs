using CoinMarketCapDotNet.Extensions;
using CoinMarketCapDotNet.Models.Enums;
using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Exchange.Map.Query
{
    public class ExchangeMapQueryParameters : QueryParameters
    {
        public void AddListingStatus(string listingStatus)
        {
            Add("listing_status", listingStatus);
        }
        public void AddSlug(string slug)
        {
            Add("slug", slug);
        }
        public void AddStart(int start)
        {
            Add("start", start);
        }

        public void AddLimit(int limit)
        {
            Add("limit", limit);
        }
        public void AddSort(ExchangeMapSortEnum sort)
        {
            Add("sort", sort.GetEnumMemberValue());
        }

        public void AddAux(string aux)
        {
            Add("aux", aux);
        }
        public void AddCryptoId(string cryptoId)
        {
            Add("crypto_id", cryptoId);
        }


    }
}
