using FXExchange.Core.Interfaces;
using FXExchange.Core.Models;

namespace FXExchange.Core.Services
{
    public class FXValidationService : IFXValidationService
    {
        ///<inheritdoc />
        public FXValidationResult TryParse(string[] input, out FXRequest output)
        {
            output = new FXRequest();
            if (input.Length != 2)
            {
                return new FXValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Usage: Exchange <currency pair> <amount to exchange>"
                };
            }
            string currencyPair = input[0];

            string[] currencies = currencyPair.Split('/');
            if (currencies.Length != 2)
            {
                return new FXValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Invalid currency pair format."
                };
            }

            string mainCurrency = currencies[0];
            string moneyCurrency = currencies[1];

            if (mainCurrency.Length != 3 || moneyCurrency.Length != 3)
            {
                return new FXValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Invalid currency ISO format."
                };
            }

            if (!double.TryParse(input[1], out double amount))
            {
                return new FXValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Invalid amount"
                };
            };

            output = new FXRequest
            {
                MainCurrency = mainCurrency,
                MoneyCurrency = moneyCurrency,
                Amount = amount
            };
            return new FXValidationResult { IsValid = true };
        }
    }
}
