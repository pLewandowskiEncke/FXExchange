using FXExchange.Core.Interfaces;
using FXExchange.Core.Models;

namespace FXExchange.Core.Services
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
        public async Task<Result<FXResponse>> Handle(string[] args)
        {
            var validationResults = _fxValidationService.TryParse(args, out FXRequest fxRequest);
            if (!validationResults.IsValid)
            {
                return Result<FXResponse>.Failure(validationResults.ErrorMessage);
            }

            try
            {
                var exchangeRates = await _fxRatesRetrievalService.GetRatesAsync(BaseCurrency);
                double exchangedAmount = _fxCalculationService.Calculate(
                    fxRequest.MainCurrency,
                    fxRequest.MoneyCurrency,
                    fxRequest.Amount,
                    exchangeRates);
                return Result<FXResponse>.Success(new FXResponse { ExchangedAmount = exchangedAmount });
            }
            catch (Exception ex)
            {
                return Result<FXResponse>.Failure("An error occurred while processing the request.");
            }
        }
    }
}
