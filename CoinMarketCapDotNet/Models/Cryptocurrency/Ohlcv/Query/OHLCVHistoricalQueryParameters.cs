using CoinMarketCapDotNet.Extensions;
using CoinMarketCapDotNet.Models.Enums;
using CoinMarketCapDotNet.Models.General;
using System;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Ohlcv.Query
{
    public class OHLCVHistoricalQueryParameters : QueryParameters
    {
        public void AddId(string id)
        {
            Add("id", id);
        }

        public void AddSlug(string slug)
        {
            Add("slug", slug);
        }

        public void AddSymbol(string symbol)
        {
            Add("symbol", symbol);
        }

        public void AddTimePeriod(TimePeriodOHLCVEnum timePeriod)
        {
            Add("time_period", timePeriod.GetEnumMemberValue());
        }

        public void AddTimeStart(DateTime? timeStart)
        {
            if (timeStart.HasValue)
            {
                var timestamp = timeStart.Value.ToString("yyyy-MM-dd");
                Add("time_start", timestamp);
            }
        }

        public void AddTimeStart(long? unixTimestamp)
        {
            if (unixTimestamp.HasValue)
            {
                var timestamp = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp.Value).ToString("yyyy-MM-dd");
                Add("time_start", timestamp);
            }
        }

        public void AddTimeStart(string iso8601Timestamp)
        {
            if (!string.IsNullOrEmpty(iso8601Timestamp))
            {
                var date = DateTime.Parse(iso8601Timestamp).ToString("yyyy-MM-dd");
                Add("time_start", date);
            }
        }

        public void AddTimeEnd(DateTime? timeEnd)
        {
            if (timeEnd.HasValue)
            {
                var timestamp = timeEnd.Value.ToString("yyyy-MM-dd");
                Add("time_end", timestamp);
            }
        }

        public void AddTimeEnd(long? unixTimestamp)
        {
            if (unixTimestamp.HasValue)
            {
                var timestamp = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp.Value).ToString("yyyy-MM-dd");
                Add("time_end", timestamp);
            }
        }

        public void AddTimeEnd(string iso8601Timestamp)
        {
            if (!string.IsNullOrEmpty(iso8601Timestamp))
            {
                var date = DateTime.Parse(iso8601Timestamp).ToString("yyyy-MM-dd");
                Add("time_end", date);
            }
        }

        public void AddCount(int count)
        {
            Add("count", count.ToString());
        }

        public void AddInterval(IntervalEnum interval)
        {
            Add("interval", interval.GetEnumMemberValue());
        }

        public void AddConvert(string convert)
        {
            Add("convert", convert);
        }

        public void AddConvertId(string convertId)
        {
            Add("convert_id", convertId);
        }

        public void AddSkipInvalid(bool skipInvalid)
        {
            Add("skip_invalid", skipInvalid.ToString().ToLower());
        }
    }
}
