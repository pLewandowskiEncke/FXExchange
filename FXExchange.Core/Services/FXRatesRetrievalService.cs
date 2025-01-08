using FXExchange.Core.Interfaces;

namespace FXExchange.Core.Services
{
    public class FXRatesRetrievalService : IFXRatesRetrievalService
    {
        ///<inheritdoc />
        public Task<Dictionary<string, double>> GetRatesAsync()
        {
            // The actual call to the external service, to get the exchange rates for the specified base currency,
            // would be implemented here.
            // For the purpose of this example, we will return some hardcoded values for DKK
            var exchangeRates = new Dictionary<string, double>
            {
                { "EUR", 743.94 },
                { "USD", 663.11 },
                { "GBP", 852.85 },
                { "SEK", 76.10 },
                { "NOK", 78.40 },
                { "CHF", 683.58 },
                { "JPY", 5.9740 },
                { "DKK", 100.0 }
            };
            return Task.FromResult(exchangeRates);
        }
    }
}
