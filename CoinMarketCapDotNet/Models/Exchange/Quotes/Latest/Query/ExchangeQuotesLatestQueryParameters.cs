using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Exchange.Quotes.Latest.Query
{
    public class ExchangeQuotesLatestQueryParameters : QueryParameters
    {
        public void AddId(string id)
        {
            Add("id", id);
        }

        public void AddSlug(string slug)
        {
            Add("slug", slug);
        }
        public void AddConvert(string convert)
        {
            Add("convert", convert);
        }

        public void AddConvertId(string convertId)
        {
            Add("convert_id", convertId);
        }
        public void AddAux(string aux)
        {
            Add("aux", aux);
        }


    }
}
