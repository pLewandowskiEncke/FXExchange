using FXExchange.Core.Interfaces;
using FXExchange.Core.Services;
using FXExchange.Core.Models;
using Moq;
using Moq.AutoMock;
using Xunit;
using FluentAssertions;

namespace FXExchange.Tests.Services
{
    public class FXHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly FXHandler _fxHandler;

        public FXHandlerTests()
        {
            _mocker = new AutoMocker();
            _fxHandler = _mocker.CreateInstance<FXHandler>();
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenValidationFails()
        {
            // Arrange
            var args = new string[] { "invalid", "input" };
            _mocker.GetMock<IFXValidationService>()
                .Setup(x => x.TryParse(args, out It.Ref<FXRequest>.IsAny))
                .Returns(new FXValidationResult { IsValid = false, ErrorMessage = "Invalid input" });

            // Act
            var result = await _fxHandler.Handle(args);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Invalid input");
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenExceptionIsThrown()
        {
            // Arrange
            var args = new string[] { "EUR/USD", "100" };
            var fxRequest = new FXRequest { MainCurrency = "EUR", MoneyCurrency = "USD", Amount = 100 };
            _mocker.GetMock<IFXValidationService>()
                .Setup(x => x.TryParse(args, out fxRequest))
                .Returns(new FXValidationResult { IsValid = true });
            _mocker.GetMock<IFXRatesRetrievalService>()
                .Setup(x => x.GetRatesAsync())
                .ThrowsAsync(new Exception("Service error"));

            // Act
            var result = await _fxHandler.Handle(args);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("An error occurred while processing the request.");
        }

        [Fact]
        public async Task Handle_ShouldLogReturnExchangedAmount_WhenValidationSucceeds()
        {
            // Arrange
            var args = new string[] { "EUR/USD", "100" };
            var fxRequest = new FXRequest { MainCurrency = "EUR", MoneyCurrency = "USD", Amount = 100 };
            var exchangeRates = new Dictionary<string, double>
            {
                { "EUR", 743.94 },
                { "USD", 663.11 }
            };
            _mocker.GetMock<IFXValidationService>()
                .Setup(x => x.TryParse(args, out fxRequest))
                .Returns(new FXValidationResult { IsValid = true });
            _mocker.GetMock<IFXRatesRetrievalService>()
                .Setup(x => x.GetRatesAsync())
                .ReturnsAsync(exchangeRates);
            _mocker.GetMock<IFXCalculationService>()
                .Setup(x => x.Calculate("EUR", "USD", 100, exchangeRates))
                .Returns(112.18);

            // Act
            var result = await _fxHandler.Handle(args);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.ExchangedAmount.Should().Be(112.18);
        }
    }
}
