using CoinMarketCapDotNet.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoinMarketCapDotNet.Extensions
{
    public static class StringExtensions
    {
        public static List<int> GetCurrencyIds(this string symbols)
        {
            List<int> ids = new List<int>();

            string[] symbolArray = symbols.Split(',');

            foreach (string symbol in symbolArray)
            {
                CurrencyEnum currency = Enum.GetValues(typeof(CurrencyEnum))
                                                .Cast<CurrencyEnum>()
                                                .FirstOrDefault(c => c.GetSymbol() == symbol);

                if (currency != default)
                {
                    int id = (int)currency;
                    ids.Add(id);
                }
            }

            return ids;
        }

    }
}
