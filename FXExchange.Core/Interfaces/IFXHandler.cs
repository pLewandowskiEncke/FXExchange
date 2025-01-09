namespace FXExchange.Core.Interfaces
{

    /// <summary>
    /// Represents a service for handling foreign exchange operations.
    /// </summary>
    public interface IFXHandler
    {
        /// <summary>
        /// Handles the foreign exchange operation for the specified currency pair and amount.
        /// </summary>
        /// <param name="args">The arguments for the operation containing currency pair and the amount</param>
        Task Handle(string[] args);
    }
}
