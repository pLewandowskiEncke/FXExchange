using FluentAssertions;
using FXExchange.Core.Models;
using FXExchange.Core.Services;
using Moq.AutoMock;
using Xunit;

namespace FXExchange.Tests.Services
{
    public class InputValidationServiceTests
    {
        private readonly FXValidationService _service;
        private readonly AutoMocker _mocker;

        public InputValidationServiceTests()
        {
            _mocker = new AutoMocker();
            _service = _mocker.CreateInstance<FXValidationService>();
        }

        [Fact]
        public void TryParse_ShouldReturnFalse_WhenInputLengthIsNotTwo()
        {
            // Arrange
            var input = new string[] { "EUR/USD" };

            // Act
            var result = _service.TryParse(input, out FXRequest output);

            // Assert
            result.IsValid.Should().BeFalse();
            result.ErrorMessage.Should().Be("Usage: Exchange <currency pair> <amount to exchange>");
        }

        [Fact]
        public void TryParse_ShouldReturnFalse_WhenCurrencyPairFormatIsInvalid()
        {
            // Arrange
            var input = new string[] { "EURUSD", "100" };

            // Act
            var result = _service.TryParse(input, out FXRequest output);

            // Assert
            result.IsValid.Should().BeFalse();
            result.ErrorMessage.Should().Contain("Invalid currency pair format");
        }

        [Fact]
        public void TryParse_ShouldReturnFalse_WhenAmountIsInvalid()
        {
            // Arrange
            var input = new string[] { "EUR/USD", "invalid" };

            // Act
            var result = _service.TryParse(input, out FXRequest output);

            // Assert
            result.IsValid.Should().BeFalse();
            result.ErrorMessage.Should().Contain("Invalid amount");
        }

        [Fact]
        public void TryParse_ShouldReturnFalse_WhenMainCurrencyIsNotISO()
        {
            // Arrange
            var input = new string[] { "AA/EUR", "100" };

            // Act
            var result = _service.TryParse(input, out FXRequest output);

            // Assert
            result.IsValid.Should().BeFalse();
            result.ErrorMessage.Should().Contain("Invalid currency ISO format");
        }

        [Fact]
        public void TryParse_ShouldReturnFalse_WhenMoneyCurrencyIsNotISO()
        {
            // Arrange
            var input = new string[] { "EUR/BB", "100" };

            // Act
            var result = _service.TryParse(input, out FXRequest output);

            // Assert
            result.IsValid.Should().BeFalse();
            result.ErrorMessage.Should().Contain("Invalid currency ISO format");
        }

        [Fact]
        public void TryParse_ShouldReturnTrue_WhenInputIsValid()
        {
            // Arrange
            var input = new string[] { "EUR/USD", "100" };

            // Act
            var result = _service.TryParse(input, out FXRequest output);

            // Assert
            result.IsValid.Should().BeTrue();
            result.ErrorMessage.Should().BeNull();
        }

        [Fact]
        public void TryParse_ShouldConvertStringToFxRequest_WhenInputIsValid()
        {
            // Arrange
            var input = new string[] { "EUR/USD", "100" };
            // Act
            var result = _service.TryParse(input, out FXRequest output);

            // Assert
            result.IsValid.Should().BeTrue();
            output.Should().BeEquivalentTo(new FXRequest
            {
                MainCurrency = "EUR",
                MoneyCurrency = "USD",
                Amount = 100
            });
        }
    }
}
