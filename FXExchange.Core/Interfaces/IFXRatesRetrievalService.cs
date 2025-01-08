namespace FXExchange.Core.Interfaces
{

    /// <summary>
    /// Represents a service for retrieving foreign exchange rates.
    /// </summary>
    public interface IFXRatesRetrievalService
    {
        /// <summary>
        /// Retrieves the exchange rates for the specified base currency.
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, double>> GetRatesAsync();
    }
}
