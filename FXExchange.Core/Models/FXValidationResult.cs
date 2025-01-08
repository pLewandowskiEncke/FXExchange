using System.Diagnostics.CodeAnalysis;

namespace FXExchange.Core.Models
{
    [ExcludeFromCodeCoverage]
    public class FXValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
    }
}
