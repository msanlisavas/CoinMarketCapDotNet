using CoinMarketCapDotNet.Extensions;
using CoinMarketCapDotNet.Models.Enums;
using System.Collections.Generic;
using Xunit;

namespace CoinMarketCapDotNet_Tests
{
    public class EnumExtensionsTests
    {
        [Fact]
        public void GetEnumMemberValue_ReturnsCorrectValue()
        {
            // Arrange
            CurrencyEnum currency = CurrencyEnum.USD;

            // Act
            string enumMemberValue = currency.GetEnumMemberValue();

            // Assert
            Assert.Equal("USD", enumMemberValue);
        }

        [Fact]
        public void GetId_ReturnsCorrectId()
        {
            // Arrange
            CurrencyEnum currency = CurrencyEnum.EUR;

            // Act
            int id = currency.GetId();

            // Assert
            Assert.Equal(2790, id);
        }

        [Fact]
        public void GetSymbol_ReturnsCorrectSymbol()
        {
            // Arrange
            CurrencyEnum currency = CurrencyEnum.TRY;

            // Act
            string symbol = currency.GetSymbol();

            // Assert
            Assert.Equal("TRY", symbol);
        }
        [Fact]
        public void GetCurrencyIds_ReturnsCorrectIds()
        {
            // Arrange
            string symbols = "USD,EUR,TRY";

            // Act
            List<int> ids = symbols.GetCurrencyIds();

            // Assert
            Assert.Equal(new List<int> { 2781, 2790, 2810 }, ids);
        }
        [Fact]
        public void GetAllIds_ReturnsAllIds()
        {
            // Arrange

            // Act
            List<int> allIds = EnumExtensions.GetAllIds<CategoryEnum>();

            // Assert
            Assert.True(allIds.Count > 0);
        }
        [Fact]
        public void GetEnumMemberValues_ReturnsEnumMemberValues()
        {
            // Arrange

            // Act
            List<string> enumMemberValues = EnumExtensions.GetAllSymbols<CurrencyEnum>();

            // Assert
            Assert.True(enumMemberValues.Count > 0);
        }
    }
}