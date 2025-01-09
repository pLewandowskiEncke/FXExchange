namespace FXExchange.Core.Interfaces
{


    /// <summary>
    /// Represents a service for calculating exchanged amounts between two currencies based on exchange rates.
    /// </summary>
    public interface IFXCalculationService
    {
        /// <summary>
        /// Calculates the exchanged amount between two currencies based on the exchange rates.
        /// </summary>
        /// <param name="mainCurrency">The main currency in the pair.</param>
        /// <param name="moneyCurrency">The money currency in the pair.</param>
        /// <param name="amount">The amount to be exchanged.</param>
        /// <param name="exchangeRates">The dictionary of exchange rates.</param>
        /// <returns>The exchanged amount.</returns>
        double Calculate(string mainCurrency, string moneyCurrency, double amount, Dictionary<string, double> exchangeRates);
    }
}
