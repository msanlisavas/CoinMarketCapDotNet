using CoinMarketCapDotNet.Extensions;
using CoinMarketCapDotNet.Models.Enums;
using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Listing.Query
{
    public class LatestQueryParameters : QueryParameters
    {
        public void AddStart(int start)
        {
            Add("start", start.ToString());
        }

        public void AddLimit(int limit)
        {
            Add("limit", limit.ToString());
        }

        public void AddPriceMin(double? priceMin)
        {
            if (priceMin.HasValue)
            {
                Add("price_min", priceMin.Value.ToString());
            }
        }

        public void AddPriceMax(double? priceMax)
        {
            if (priceMax.HasValue)
            {
                Add("price_max", priceMax.Value.ToString());
            }
        }

        public void AddMarketCapMin(double? marketCapMin)
        {
            if (marketCapMin.HasValue)
            {
                Add("market_cap_min", marketCapMin.Value.ToString());
            }
        }

        public void AddMarketCapMax(double? marketCapMax)
        {
            if (marketCapMax.HasValue)
            {
                Add("market_cap_max", marketCapMax.Value.ToString());
            }
        }

        public void AddVolume24hMin(double? volume24hMin)
        {
            if (volume24hMin.HasValue)
            {
                Add("volume_24h_min", volume24hMin.Value.ToString());
            }
        }

        public void AddVolume24hMax(double? volume24hMax)
        {
            if (volume24hMax.HasValue)
            {
                Add("volume_24h_max", volume24hMax.Value.ToString());
            }
        }

        public void AddCirculatingSupplyMin(double? circulatingSupplyMin)
        {
            if (circulatingSupplyMin.HasValue)
            {
                Add("circulating_supply_min", circulatingSupplyMin.Value.ToString());
            }
        }

        public void AddCirculatingSupplyMax(double? circulatingSupplyMax)
        {
            if (circulatingSupplyMax.HasValue)
            {
                Add("circulating_supply_max", circulatingSupplyMax.Value.ToString());
            }
        }

        public void AddPercentChange24hMin(double? percentChange24hMin)
        {
            if (percentChange24hMin.HasValue)
            {
                Add("percent_change_24h_min", percentChange24hMin.Value.ToString());
            }
        }

        public void AddPercentChange24hMax(double? percentChange24hMax)
        {
            if (percentChange24hMax.HasValue)
            {
                Add("percent_change_24h_max", percentChange24hMax.Value.ToString());
            }
        }

        public void AddConvert(string convert)
        {
            Add("convert", convert);
        }

        public void AddConvertId(string convertId)
        {
            Add("convert_id", convertId);
        }

        public void AddSort(SortListingLatestEnum sort)
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

        public void AddTag(TagEnum tag)
        {
            Add("tag", tag.GetEnumMemberValue());
        }

        public void AddAux(string aux)
        {
            Add("aux", aux);
        }
    }

}
