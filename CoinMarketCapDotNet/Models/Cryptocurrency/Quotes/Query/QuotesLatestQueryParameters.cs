using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Quotes.Query
{
    public class QuotesLatestQueryParameters : QueryParameters
    {
        public void AddId(string id)
        {
            Add("id", id);
        }

        public void AddSymbol(string symbol)
        {
            Add("symbol", symbol);
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

        public void AddSkipInvalid(bool skipInvalid)
        {
            Add("skip_invalid", skipInvalid.ToString());
        }
    }
}
