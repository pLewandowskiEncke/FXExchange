using FXExchange.Core.Interfaces;
using FXExchange.Core.Models;
using FXExchange.Core.Services;
using Moq;
using Moq.AutoMock;
using System.Globalization;
using Xunit;

namespace FXExchange.Tests.Services
{
    public class FXHandlerTests : IDisposable
    {
        private readonly CultureInfo _originalCulture;
        private readonly AutoMocker _mocker;
        private readonly FXHandler _fxHandler;

        public FXHandlerTests()
        {
            _originalCulture = CultureInfo.CurrentCulture;
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
            await _fxHandler.Handle(args);

            // Assert
            _mocker.GetMock<ILogger>().Verify(x => x.Log("Invalid input"), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenExceptionIsThrown()
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
            await _fxHandler.Handle(args);

            // Assert
            _mocker.GetMock<ILogger>().Verify(x => x.Log("Error: Service error"), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldLogExchangedAmount_WhenValidationSucceeds()
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
            CultureInfo.CurrentCulture = new CultureInfo("en-EN");

            // Act
            await _fxHandler.Handle(args);

            // Assert
            _mocker.GetMock<ILogger>().Verify(x => x.Log("112.18"), Times.Once);
        }

        public void Dispose()
        {
            // Restore the original culture after the test
            CultureInfo.CurrentCulture = _originalCulture;
        }
    }
}
