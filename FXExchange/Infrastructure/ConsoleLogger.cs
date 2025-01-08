using System.Diagnostics.CodeAnalysis;

namespace FXExchange.Infrastructure
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
