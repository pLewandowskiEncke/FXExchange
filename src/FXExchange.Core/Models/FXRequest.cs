using System.Diagnostics.CodeAnalysis;

namespace FXExchange.Core.Models
{
    [ExcludeFromCodeCoverage]
    public record FXRequest
    {
        public string MainCurrency { get; set; }
        public string MoneyCurrency { get; set; }
        public double Amount { get; set; }
    }
}
