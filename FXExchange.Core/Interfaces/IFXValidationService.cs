using FXExchange.Core.Models;

namespace FXExchange.Core.Interfaces
{

    /// <summary>
    /// Represents a validation service for FXExchange.
    /// </summary>
    public interface IFXValidationService
    {
        /// <summary>
        /// Tries to convert an array of strings containing the string representation of currency pair and the amount.
        /// </summary>
        /// <param name="input">An array of strings to be converted</param>
        /// <param name="result">An object representing the converted result</param>
        /// <returns>A validation result object with IsValid set to true when converted successfully; otherwise, false.</returns>
        FXValidationResult TryParse(string[] input, out FXRequest result);
    }
}
