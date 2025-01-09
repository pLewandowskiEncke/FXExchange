using FXExchange.Core.Interfaces;
using FXExchange.Core.Models;

namespace FXExchange.Core.Services
{

    ///<inheritdoc />
    public class FXHandler : IFXHandler
    {
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
            var validationResults = _fxValidationService.TryParse(args, out FXRequest fxRequest);
            if (!validationResults.IsValid)
            {
                _logger.Log(validationResults.ErrorMessage);
                return;
            }

            try
            {
                var exchangeRates = await _fxRatesRetrievalService.GetRatesAsync();
                double exchangedAmount = _fxCalculationService.Calculate(
                    fxRequest.MainCurrency,
                    fxRequest.MoneyCurrency,
                    fxRequest.Amount,
                    exchangeRates);
                _logger.Log(exchangedAmount.ToString());
            }
            catch (Exception ex)
            {
                _logger.Log($"Error: {ex.Message}");
            }
        }
    }
}
