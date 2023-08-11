using System;
using System.Collections.Generic;
using System.Linq;

namespace CoinMarketCapDotNet.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var value) ? value : default;
        }
        public static TValue TryGetValue<TKey, TValue>(
    this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(
    this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
        {
            foreach (var pair in second)
            {
                first.Add(pair.Key, pair.Value);
            }
            return first;
        }
        public static IDictionary<TKey, TValue> FilterByKeys<TKey, TValue>(
    this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys)
        {
            return keys.Where(key => dictionary.ContainsKey(key))
                       .ToDictionary(key => key, key => dictionary[key]);
        }
        public static IDictionary<TKey, TValue> FilterByValueCondition<TKey, TValue>(
    this IDictionary<TKey, TValue> dictionary, Func<TValue, bool> predicate)
        {
            return dictionary.Where(pair => predicate(pair.Value))
                             .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
