using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumMemberValue(this Enum enumValue)
        {
            var memberInfo = enumValue.GetType().GetMember(enumValue.ToString());
            var attribute = (EnumMemberAttribute)Attribute.GetCustomAttribute(memberInfo[0], typeof(EnumMemberAttribute));
            return attribute?.Value;
        }
        public static int GetId<TEnum>(this TEnum value) where TEnum : Enum
        {
            return (int)Convert.ChangeType(value, typeof(int));
        }
        public static string GetSymbol<TEnum>(this TEnum value) where TEnum : Enum
        {
            var enumType = typeof(TEnum);
            var name = Enum.GetName(enumType, value);

            var fieldInfo = enumType.GetField(name);
            var enumMemberAttribute = fieldInfo.GetCustomAttributes(false)
                                                .OfType<EnumMemberAttribute>()
                                                .FirstOrDefault();

            return enumMemberAttribute?.Value;
        }
        public static List<int> GetAllIds<TEnum>() where TEnum : Enum
        {
            List<int> ids = new List<int>();

            foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
            {
                ids.Add((int)(object)value);
            }

            return ids;
        }
        public static List<string> GetAllSymbols<TEnum>() where TEnum : Enum
        {
            List<string> enumMemberValues = new List<string>();

            foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
            {
                string enumMemberValue = value.GetEnumMemberValue();
                enumMemberValues.Add(enumMemberValue);
            }

            return enumMemberValues;
        }

    }
}
