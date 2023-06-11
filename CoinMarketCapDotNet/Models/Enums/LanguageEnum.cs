using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum LanguageEnum
    {
        [EnumMember(Value = "en")]
        English,
        [EnumMember(Value = "zh")]
        Chinese,
        [EnumMember(Value = "zh-tw")]
        ChineseTraditional,
        [EnumMember(Value = "de")]
        German,
        [EnumMember(Value = "id")]
        Indonesian,
        [EnumMember(Value = "ja")]
        Japanese,
        [EnumMember(Value = "ko")]
        Korean,
        [EnumMember(Value = "es")]
        Spanish,
        [EnumMember(Value = "th")]
        Thai,
        [EnumMember(Value = "tr")]
        Turkish,
        [EnumMember(Value = "vi")]
        Vietnamese,
        [EnumMember(Value = "ru")]
        Russian,
        [EnumMember(Value = "fr")]
        French,
        [EnumMember(Value = "nl")]
        Dutch,
        [EnumMember(Value = "ar")]
        Arabic,
        [EnumMember(Value = "pt-br")]
        PortugueseBrazilian,
        [EnumMember(Value = "hi")]
        Hindi,
        [EnumMember(Value = "pl")]
        Polish,
        [EnumMember(Value = "uk")]
        Ukrainian,
        [EnumMember(Value = "fil-rph")]
        Filipino,
        [EnumMember(Value = "it")]
        Italian
    }
}
