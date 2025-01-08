using FXExchange.Infrastructure;
using FXExchange.Interfaces;
using FXExchange.Models;

namespace FXExchange.Services
{

    ///<inheritdoc />
    public class FXHandler : IFXHandler
    {
        const string BaseCurrency = "DKK";
        private readonly IFXValidationService _fxValidationService;
        private readonly IFXCalculationService _fxCalculationService;
        private readonly IFXRatesRetrievalService _fxRatesRetrievalService;
        private readonly ILogger _logger;

        public FXHandler(
            IFXValidationService fxValidationService,
            IFXCalculationService fxCalculationService,
            IFXRatesRetrievalService fxRatesRetrievalService,
            ILogger logger)
        {
            _fxValidationService = fxValidationService;
            _fxCalculationService = fxCalculationService;
            _fxRatesRetrievalService = fxRatesRetrievalService;
            _logger = logger;
        }

        ///<inheritdoc />
        public async Task Handle(string[] args)
        {
            var validationResults = _fxValidationService.TryParse(args, out FXInput fxInput);
            if (!validationResults.IsValid)
            {
                _logger.Log(validationResults.ErrorMessage);
                return;
            }

            try
            {
                var exchangeRates = await _fxRatesRetrievalService.GetRatesAsync(BaseCurrency);
                double exchangedAmount = _fxCalculationService.Calculate(
                    fxInput.MainCurrency,
                    fxInput.MoneyCurrency,
                    fxInput.Amount,
                    exchangeRates);
                _logger.Log($"Exchanged amount: {exchangedAmount}");
            }
            catch (Exception ex)
            {
                _logger.Log($"Error: {ex.Message}");
            }
        }
    }
}
