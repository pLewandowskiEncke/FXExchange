using FXExchange.Infrastructure;
using FXExchange.Interfaces;
using FXExchange.Models;
using FXExchange.Services;
using Moq;
using Moq.AutoMock;
using Xunit;

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
                .Setup(x => x.TryParse(args, out It.Ref<FXInput>.IsAny))
                .Returns(new FXValidationResult { IsValid = false, ErrorMessage = "Invalid input" });

            // Act
            await _fxHandler.Handle(args);

            // Assert
            _mocker.GetMock<ILogger>().Verify(x => x.Log("Invalid input"), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var args = new string[] { "EUR/USD", "100" };
            var fxInput = new FXInput { MainCurrency = "EUR", MoneyCurrency = "USD", Amount = 100 };
            _mocker.GetMock<IFXValidationService>()
                .Setup(x => x.TryParse(args, out fxInput))
                .Returns(new FXValidationResult { IsValid = true });
            _mocker.GetMock<IFXRatesRetrievalService>()
                .Setup(x => x.GetRatesAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Service error"));

            // Act
            await _fxHandler.Handle(args);

            // Assert
            _mocker.GetMock<ILogger>().Verify(x => x.Log("Error: Service error"), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldLogExchangedAmount_WhenValidationSucceeds()
        {
            // Arrange
            var args = new string[] { "EUR/USD", "100" };
            var fxInput = new FXInput { MainCurrency = "EUR", MoneyCurrency = "USD", Amount = 100 };
            var exchangeRates = new Dictionary<string, double>
            {
                { "EUR", 743.94 },
                { "USD", 663.11 }
            };
            _mocker.GetMock<IFXValidationService>()
                .Setup(x => x.TryParse(args, out fxInput))
                .Returns(new FXValidationResult { IsValid = true });
            _mocker.GetMock<IFXRatesRetrievalService>()
                .Setup(x => x.GetRatesAsync(It.IsAny<string>()))
                .ReturnsAsync(exchangeRates);
            _mocker.GetMock<IFXCalculationService>()
                .Setup(x => x.Calculate("EUR", "USD", 100, exchangeRates))
                .Returns(112.18);

            // Act
            await _fxHandler.Handle(args);

            // Assert
            _mocker.GetMock<ILogger>().Verify(x => x.Log("Exchanged amount: 112.18"), Times.Once);
        }
    }
}
