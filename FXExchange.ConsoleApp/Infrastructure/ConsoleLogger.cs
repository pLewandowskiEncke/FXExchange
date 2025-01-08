using FXExchange.Core.Interfaces;

namespace FXExchange.ConsoleApp.Infrastructure
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
