using FXExchange.Interfaces;
using FXExchange.Services;
using FluentAssertions;
using Xunit;

namespace FXExchange.Tests.Services
{
    public class FXCalculationServiceTests
    {
        private readonly IFXCalculationService _service;

        public FXCalculationServiceTests()
        {
            _service = new FXCalculationService();
        }

        [Fact]
        public void Calculate_ShouldThrowException_WhenMainCurrencyIsUnsupported()
        {
            // Arrange
            var exchangeRates = new Dictionary<string, double>
            {
                { "USD", 723.92 }
            };

            // Act
            Action act = () => _service.Calculate("EUR", "USD", 100, exchangeRates);

            // Assert
            act.Should().Throw<Exception>().WithMessage("Unsupported main currency in pair: EUR");
        }

        [Fact]
        public void Calculate_ShouldThrowException_WhenMoneyCurrencyIsUnsupported()
        {
            // Arrange
            var exchangeRates = new Dictionary<string, double>
            {
                { "EUR", 746.08 }
            };

            // Act
            Action act = () => _service.Calculate("EUR", "USD", 100, exchangeRates);

            // Assert
            act.Should().Throw<Exception>().WithMessage("Unsupported money currency in pair: USD");
        }

        [Fact]
        public void Calculate_ShouldReturnSameAmount_WhenCurrenciesAreIdentical()
        {
            // Arrange
            var amount = 100;
            var exchangeRates = new Dictionary<string, double>
            {
                { "EUR", 746.08 }
            };

            // Act
            var result = _service.Calculate("EUR", "EUR", amount, exchangeRates);

            // Assert
            result.Should().Be(amount);
        }

        [Fact]
        public void Calculate_ShouldReturnMainCurrencyAmount_WhenMoneyCurrencyIsBaseCurrency()
        {
            // Arrange
            var amount = 1;
            var exchangeRates = new Dictionary<string, double>
            {
                { "EUR", 746.08 },
                { "DKK", 100 },
            };

            // Act
            var result = _service.Calculate("EUR", "DKK", amount, exchangeRates);

            // Assert
            result.Should().BeApproximately(7.4608, 0.0001);
        }

        [Fact]
        public void Calculate_ShouldReturnExchangedAmountRoundedTo4DecimalDigits()
        {
            // Arrange
            var amount = 1;
            var exchangeRates = new Dictionary<string, double>
            {
                { "EUR", 746.666666666 },
                { "DKK", 100 },
            };

            // Act
            var result = _service.Calculate("EUR", "DKK", amount, exchangeRates);

            // Assert
            result.Should().Be(7.4667);
        }

        [Theory]
        [InlineData(1, 1, 100, 100)]
        [InlineData(1, 1, 1, 1)]
        [InlineData(2.22, 2.22, 1, 1)]
        [InlineData(1, 100, 100, 1)]
        [InlineData(100, 1, 1, 100)]
        [InlineData(1, 100, 10, 0.1)]
        [InlineData(5, 1, 10, 50)]
        [InlineData(746.08, 723.92, 100, 103.06)] 
        [InlineData(723.92, 746.08, 100, 97.03)]
        public void Calculate_ShouldReturnCorrectExchangedAmount(double mainCurrencyRate, double moneyCurrencyRate, double amount, double expected)
        {
            // Arrange
            var exchangeRates = new Dictionary<string, double>
            {
                { "EUR", mainCurrencyRate },
                { "USD", moneyCurrencyRate }
            };

            // Act
            var result = _service.Calculate("EUR", "USD", amount, exchangeRates);

            // Assert
            result.Should().BeApproximately(expected, 0.01);
        }
    }
}
