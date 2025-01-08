using FXExchange.Core.Interfaces;

namespace FXExchange.Core.Services
{
    ///<inheritdoc />
    public class FXCalculationService : IFXCalculationService
    {
        ///<inheritdoc />
        public double Calculate(string mainCurrency, string moneyCurrency, double amount, Dictionary<string, double> exchangeRates)
        {
            if (!exchangeRates.Keys.Contains(mainCurrency))
            {
                throw new Exception($"Unsupported main currency in pair: {mainCurrency}");
            }

            if (!exchangeRates.Keys.Contains(moneyCurrency))
            {
                throw new Exception($"Unsupported money currency in pair: {moneyCurrency}");
            }

            if (mainCurrency == moneyCurrency)
            {
                return amount;
            }

            double mainToBase = exchangeRates[mainCurrency] / 100;
            double moneyToBase = exchangeRates[moneyCurrency] / 100;

            double amountInDKK = amount * mainToBase;
            double exchangedAmount = amountInDKK / moneyToBase;

            return Math.Round(exchangedAmount, 4);
        }
    }
}
