using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Ohlcv.Query
{
    public class OHLCVLatestQueryParameters : QueryParameters
    {
        public void AddIds(string ids)
        {
            Add("id", ids);
        }

        public void AddSymbols(string symbols)
        {
            Add("symbol", symbols);
        }

        public void AddConvert(string convert)
        {
            Add("convert", convert);
        }

        public void AddConvertIds(string convertIds)
        {
            Add("convert_id", convertIds);
        }
        public void AddSkipInvalid(bool skipInvalid)
        {
            Add("skip_invalid", skipInvalid.ToString().ToLower());
        }
    }

}
