using FXExchange.Core.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace FXExchange.ConsoleApp.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
