using CoinMarketCapDotNet.Models.General;
using System;

namespace CoinMarketCapDotNet.Models.Tools.Query
{
    public class PriceConvertionQueryParameters : QueryParameters
    {
        public void AddAmount(double amount)
        {
            Add("amount", amount.ToString());
        }
        public void AddId(string id)
        {
            Add("id", id);
        }
        public void AddSymbol(string symbol)
        {
            Add("symbol", symbol);
        }
        public void AddConvert(string convert)
        {
            Add("convert", convert);
        }
        public void AddConvertId(string convertId)
        {
            Add("convert_id", convertId);
        }
        public void AddTime(DateTime? timeStart)
        {
            if (timeStart.HasValue)
            {
                var timestamp = timeStart.Value.ToString("yyyy-MM-dd");
                Add("time", timestamp);
            }
        }

        public void AddTime(long? unixTimestamp)
        {
            if (unixTimestamp.HasValue)
            {
                var timestamp = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp.Value).ToString("yyyy-MM-dd");
                Add("time", timestamp);
            }
        }

        public void AddTime(string iso8601Timestamp)
        {
            if (!string.IsNullOrEmpty(iso8601Timestamp))
            {
                var date = DateTime.Parse(iso8601Timestamp).ToString("yyyy-MM-dd");
                Add("time", date);
            }
        }

    }
}
