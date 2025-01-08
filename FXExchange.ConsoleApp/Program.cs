using FXExchange.ConsoleApp.Infrastructure;
using FXExchange.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        // Create a new instance of the service collection
        var services = new ServiceCollection();
        services.ConfigureServices();
        var serviceProvider = services.BuildServiceProvider();

        var fxHandler = serviceProvider.GetRequiredService<IFXHandler>();
        var logger = serviceProvider.GetRequiredService<ILogger>();

        // Handle the operation
        var result = await fxHandler.Handle(args);
        if (result.IsSuccess)
        {
            logger.Log(result.Value.ExchangedAmount.ToString());
        }
        else
        {
            logger.Log(result.ErrorMessage);
        }
    }
}
