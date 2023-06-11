using CoinMarketCapDotNet.Extensions;
using CoinMarketCapDotNet.Models.Enums;
using CoinMarketCapDotNet.Models.General;
using System;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Listing.Query
{
    public class HistoricalQueryParameters : QueryParameters
    {
        public void AddDate(DateTime date)
        {
            var formattedDate = date.ToString("yyyy-MM-dd");
            Add("date", formattedDate);
        }

        public void AddDate(string iso8601Date)
        {
            var date = DateTime.Parse(iso8601Date);
            var formattedDate = date.ToString("yyyy-MM-dd");
            Add("date", formattedDate);
        }

        public void AddStart(int start)
        {
            Add("start", start.ToString());
        }

        public void AddLimit(int limit)
        {
            Add("limit", limit.ToString());
        }

        public void AddConvert(string convert)
        {
            Add("convert", convert);
        }

        public void AddConvertId(string convertId)
        {
            Add("convert_id", convertId);
        }

        public void AddSort(SortListingHistoricalEnum sort)
        {
            Add("sort", sort.GetEnumMemberValue());
        }

        public void AddSortDir(SortDirectionEnum sortDir)
        {
            Add("sort_dir", sortDir.GetEnumMemberValue());
        }

        public void AddCryptocurrencyType(CryptocurrencyTypeEnum cryptocurrencyType)
        {
            Add("cryptocurrency_type", cryptocurrencyType.GetEnumMemberValue());
        }

        public void AddAux(string aux)
        {
            Add("aux", aux);
        }
    }

}
