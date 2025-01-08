namespace FXExchange.Interfaces
{

    /// <summary>
    /// Represents a service for retrieving foreign exchange rates.
    /// </summary>
    public interface IFXRatesRetrievalService
    {
        Task<Dictionary<string, double>> GetRatesAsync(string baseCurrency);
    }
}
