using FXExchange.Core.Interfaces;
using FXExchange.Core.Services;
using FXExchange.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

public static class ServicesConfiguration
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddTransient<IFXHandler, FXHandler>();
        services.AddTransient<IFXValidationService, FXValidationService>();
        services.AddTransient<IFXCalculationService, FXCalculationService>();
        services.AddTransient<IFXRatesRetrievalService, FXRatesRetrievalService>();
        services.AddTransient<ILogger, ConsoleLogger>();
    }
}
