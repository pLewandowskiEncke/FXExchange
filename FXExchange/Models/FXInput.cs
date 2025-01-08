using System.Diagnostics.CodeAnalysis;

namespace FXExchange.Models
{
    [ExcludeFromCodeCoverage]
    public record FXInput
    {
        public string MainCurrency { get; set; }
        public string MoneyCurrency { get; set; }
        public double Amount { get; set; }
    }
}
