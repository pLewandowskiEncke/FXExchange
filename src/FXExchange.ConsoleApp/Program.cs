using FXExchange.ConsoleApp.Infrastructure;
using FXExchange.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.ConfigureServices();
        var serviceProvider = services.BuildServiceProvider();

        var fxHandler = serviceProvider.GetRequiredService<IFXHandler>();        

        // Handle the operation
        await fxHandler.Handle(args);
    }
}
